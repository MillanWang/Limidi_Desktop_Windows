using Commons.Music.Midi;
using System.Xml.Serialization;

//Needs to be a singleton dependency that is passed into the MIDI API endpoint
public class MidiEventSender : IMidiEventSender
{

    private int _selectedDeviceID;
    private bool _isCloserThreadRunning;
    private IMidiOutput? _currentOutput;

    public MidiEventSender()
    {
        _selectedDeviceID = 1;
        _isCloserThreadRunning = false;
        _currentOutput = null;
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

        IMidiOutput output = MidiAccessManager.Default.OpenOutputAsync(this._selectedDeviceID.ToString()).Result;

        output.Send(
            new byte[] {
                    isNoteOn ? MidiEvent.NoteOn : MidiEvent.NoteOff,
                    (byte) noteNumber, //Note number, i.e C5==60
                    (byte) velocity // AKA Volume
            },
            0, // Offset
            3, // Length. Always 3 bytes
            0 // Timestamp
        );

        //Cleanup and respond
        output.CloseAsync();
        return true;
    }

    public bool SendMidiControlChange(int controlIndex, int level)
    {
        // Illegal value filtration
        if (controlIndex < 0 || controlIndex > 127 || level < 0 || level > 127) return false;

        IMidiOutput output = MidiAccessManager.Default.OpenOutputAsync(this._selectedDeviceID.ToString()).Result;

        output.Send(
            new byte[] {
                    MidiEvent.CC,
                    (byte) controlIndex,
                    (byte) level
            },
            0, // Offset
            3, // Length. Always 3 bytes
            0 // Timestamp
        );

        //Cleanup and respond
        output.CloseAsync();
        return true;
    }

    private void OpenOutputDevice()
    {
        if (this._isCloserThreadRunning) return;
        this._currentOutput = MidiAccessManager.Default.OpenOutputAsync(this._selectedDeviceID.ToString()).Result;

        Thread t = new Thread(() => { OutputCloserSideThread(this); });
        t.Start();
    }


    public void CloseOutput()
    {
        //TODO - Track the most recent output open time and relaunch thread if it's too recent 
        this._isCloserThreadRunning = false;
        this._currentOutput?.CloseAsync();
        this._currentOutput = null;
    }

    static void OutputCloserSideThread(MidiEventSender midiEventSender)
    {
        //Sleep for 10 seconds
        Thread.Sleep(10000);
        midiEventSender.CloseOutput();
    }
}

