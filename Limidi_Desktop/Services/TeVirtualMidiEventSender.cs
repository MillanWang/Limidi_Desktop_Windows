
using Commons.Music.Midi;
using TobiasErichsen.teVirtualMIDI;

namespace Limidi_Desktop.Services
{
    public class TeVirtualMidiEventSender : IMidiEventSender
    {
        TeVirtualMIDI _virtualMidiInput;

        public TeVirtualMidiEventSender()
        {

            try
            {
                _virtualMidiInput = new TeVirtualMIDI("virtualMilliMidi");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        ~TeVirtualMidiEventSender()
        {
            _virtualMidiInput.shutdown();
        }

        public bool SendMidiControlChange(int controlIndex, int level)
        {
            if (controlIndex < 0 || controlIndex > 127 || level < 0 || level > 127) return false;

            _sendMidiSignal(MidiEvent.CC, (byte)controlIndex, (byte)level);
            return true;
        }

        public bool SendMidiNoteInput(bool isNoteOn, int noteNumber, int velocity)
        {
            if (noteNumber < 0 || noteNumber > 120 || velocity < 0 || velocity > 127) return false;

            _sendMidiSignal(isNoteOn ? MidiEvent.NoteOn : MidiEvent.NoteOff, (byte)noteNumber, (byte)velocity);
            return true;
        }

        private void _sendMidiSignal(byte eventType, byte index, byte amount)
        {
            _virtualMidiInput.sendCommand(new byte[] { eventType, index, amount });

        }
    }
}
