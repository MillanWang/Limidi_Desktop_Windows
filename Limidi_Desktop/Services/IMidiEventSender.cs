public interface IMidiEventSender
{
    bool SendMidiNoteInput(bool isNoteOn, int noteNumber, int velocity);
    bool SendMidiControlChange(int controlIndex, int level);
}