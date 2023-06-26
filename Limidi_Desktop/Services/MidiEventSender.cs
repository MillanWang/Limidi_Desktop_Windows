using Commons.Music.Midi;

public class MidiEventSender : IMidiEventSender
{
    private static int OUTPUT_OPEN_DURATION_MILLISECONDS = 10000;


    private int _selectedDeviceID;
    private IMidiOutput? _currentOutput;
    private DateTime _mostRecentRequestTime;


    public MidiEventSender()
    {
        _selectedDeviceID = 1;
        _currentOutput = null;
        _mostRecentRequestTime = DateTime.Now;
    }

    public IEnumerable<String> GetAllInputDeviceNames()
    {
        var rawOutputs = MidiAccessManager.Default.Outputs;
        var outputNames = new List<string>();
        foreach (var output in rawOutputs)
        {
            outputNames.Add(output.Name);
        }
        return outputNames;
    }

    public string GetCurrentInputDeviceName()
    {
        return MidiAccessManager.Default.Outputs.ToList<IMidiPortDetails>()[_selectedDeviceID].Name;
    }

    public bool SetCurrentInputDevice(int deviceID)
    {
        if (deviceID < 0 || deviceID >= MidiAccessManager.Default.Outputs.ToList<IMidiPortDetails>().Count())
        {
            return false;
        }
        this.CloseOutput();
        this._selectedDeviceID = deviceID;
        return true;
    }

    public bool SendMidiNoteInput(bool isNoteOn, int noteNumber, int velocity)
    {
        // Illegal value filtration
        if (noteNumber < 0 || noteNumber > 120 || velocity < 0 || velocity > 127) return false;

        _sendMidiSignal(isNoteOn ? MidiEvent.NoteOn : MidiEvent.NoteOff, (byte)noteNumber, (byte)velocity);
        return true;
    }

    public bool SendMidiControlChange(int controlIndex, int level)
    {
        // Illegal value filtration
        if (controlIndex < 0 || controlIndex > 127 || level < 0 || level > 127) return false;

        _sendMidiSignal(MidiEvent.CC, (byte)controlIndex, (byte)level);
        return true;
    }

    private void _sendMidiSignal(byte eventType, byte index, byte amount)
    {
        _mostRecentRequestTime = DateTime.Now;
        if (_currentOutput == null)
        {
            this._currentOutput = MidiAccessManager.Default.OpenOutputAsync(this._selectedDeviceID.ToString()).Result;
            _createOutputCloserThread();
        }
        _currentOutput.Send(
            new byte[] { eventType, index, amount },
            0, // Offset
            3, // Length. Always 3 bytes
            0 // Timestamp
        );
    }

    private void _createOutputCloserThread()
    {
        (new Thread(() => { OutputCloserSideThread(this); })).Start();
    }

    public void CloseOutput()
    {
        if (this._currentOutput == null) return; // Do nothing if already closed
        if (DateTime.Now.Subtract(this._mostRecentRequestTime).TotalMilliseconds <= OUTPUT_OPEN_DURATION_MILLISECONDS)
        { // The time until close was reset
            Console.WriteLine("Previous close thread jumped. Make new one");
            _createOutputCloserThread();
            return;
        }
        // TODO - Validate the close process and put locks around usage of _current output
        Console.WriteLine("Closed output @ " + DateTime.Now);
        this._currentOutput?.CloseAsync();
        this._currentOutput = null;
    }

    static void OutputCloserSideThread(MidiEventSender midiEventSender)
    {
        //Sleep expects milliseconds
        Thread.Sleep(OUTPUT_OPEN_DURATION_MILLISECONDS);
        midiEventSender.CloseOutput();
    }
}

