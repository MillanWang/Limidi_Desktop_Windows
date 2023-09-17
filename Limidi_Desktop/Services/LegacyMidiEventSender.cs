using Commons.Music.Midi;

/*OLD loopMidi approach. Need the licesnse to get away from this ol thing*/
public class LegacyMidiEventSender : IMidiEventSender
{
    private int _selectedDeviceID;
    private IMidiOutput? _currentOutput;

    public LegacyMidiEventSender()
    {
        _selectedDeviceID = 1;
        _currentOutput = null;
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
        if (_currentOutput == null)
        {
            this._currentOutput = MidiAccessManager.Default.OpenOutputAsync(this._selectedDeviceID.ToString()).Result;
        }
        _currentOutput.Send(
                new byte[] { eventType, index, amount },
                0, // Offset
                3, // Length. Always 3 bytes
                0 // Timestamp
            );
    }


    public void CloseOutput()
    {
        if (this._currentOutput == null) return; // Do nothing if already closed
        this._currentOutput.CloseAsync();
        this._currentOutput = null;
    }
}

