namespace Limidi_Desktop.Services
{
    public interface IVirtualMidi
    {
        bool SendMidiNoteInput(bool isNoteOn, int noteNumber, int velocity);
    }
}
