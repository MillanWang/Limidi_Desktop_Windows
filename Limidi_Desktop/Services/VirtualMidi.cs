namespace Limidi_Desktop.Services
{
    public class VirtualMidi : IVirtualMidi
    {

        TeVirtualMIDI _virtualMidiInput;
        public VirtualMidi()
        {
            _virtualMidiInput = new TeVirtualMIDI("virtualMilliMidi");
        }

        ~VirtualMidi()
        {
            _virtualMidiInput.shutdown();
        }

        public bool SendMidiNoteInput(bool isNoteOn, int noteNumber, int velocity)
        {
            this._virtualMidiInput.sendCommand(new byte[] { 144, 60, 100 });
            return true;
        }
    }
}
