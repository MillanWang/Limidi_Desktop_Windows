using Commons.Music.Midi;

//Needs to be a singleton dependency that is passed into the MIDI API endpoint
public class MidiEventSender : IMidiEventSender
{

    private int _currentDeviceID;
    private object _lock;

    public MidiEventSender()
    {
        _currentDeviceID = 1;
        _lock = new object();
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
        return MidiAccessManager.Default.Outputs.ToList<IMidiPortDetails>()[_currentDeviceID].Name;
    }

    public bool SetCurrentInputDevice(int deviceID)
    {
        if (deviceID < 0 || deviceID >= MidiAccessManager.Default.Outputs.ToList<IMidiPortDetails>().Count())
        {
            return false;
        }
        this._currentDeviceID = deviceID;
        return true;
    }

    public bool SendMidiInput(bool isNoteOn, int noteNumber, int velocity)
    {
        // Illegal value filtration
        if (noteNumber < 0 || noteNumber > 120 || velocity < 1 || velocity > 127) return false;

        /**
            PERFORMANCE CHECK: If errors persist, consider the side-thread closure idea. 
        */
        lock (_lock)
        {
            var output = MidiAccessManager.Default.OpenOutputAsync(this._currentDeviceID.ToString()).Result;

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
    }
}

