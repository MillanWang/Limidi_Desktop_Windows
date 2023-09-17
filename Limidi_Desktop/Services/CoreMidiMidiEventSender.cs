using Commons.Music.Midi;
using Commons.Music.Midi.CoreMidiApi;
using CoreMidi;

namespace Limidi_Desktop.Services
{
    public class CoreMidiMidiEventSender : IMidiEventSender
    {
        public CoreMidiMidiEventSender()
        {
            // This looks promising on mac. Gotta take a look & see

            var extensionManager = new CoreMidiAccess().ExtensionManager.GetInstance<MidiPortCreatorExtension>();
            var portCreatorContext1 = new MidiPortCreatorExtension.PortCreatorContext
            {
                ApplicationName = "sample ApplicationName FromMill",
                PortName = "sample PortName FromMill",
                Manufacturer = "sample Manufacturer FromMill",
                Version = "sample Version FromMill",
            };

            var portCreatorContext2 = new MidiPortCreatorExtension.PortCreatorContext
            {
                ApplicationName = "sample ApplicationName FromMill 1256",
                PortName = "sample PortName FromMill 1256",
                Manufacturer = "sample Manufacturer FromMill 1256",
                Version = "sample Version FromMill 1256"
            };


            var x = extensionManager.CreateVirtualInputSender(portCreatorContext1);
            var y = extensionManager.CreateVirtualOutputReceiver(portCreatorContext2);


        }
        public IEnumerable<string> GetAllInputDeviceNames()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentInputDeviceName()
        {
            throw new NotImplementedException();
        }

        public bool SendMidiControlChange(int controlIndex, int level)
        {
            throw new NotImplementedException();
        }

        public bool SendMidiNoteInput(bool isNoteOn, int noteNumber, int velocity)
        {
            throw new NotImplementedException();
        }

        public bool SetCurrentInputDevice(int deviceID)
        {
            throw new NotImplementedException();
        }
    }
}
