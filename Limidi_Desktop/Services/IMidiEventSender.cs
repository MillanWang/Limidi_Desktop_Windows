public interface IMidiEventSender
{
    IEnumerable<String> GetAllInputDeviceNames();
    String GetCurrentInputDeviceName();
    bool SetCurrentInputDevice(int deviceID);
    bool SendMidiNoteInput(bool isNoteOn, int noteNumber, int velocity);
    bool SendMidiControlChange(int controlIndex, int level);
}