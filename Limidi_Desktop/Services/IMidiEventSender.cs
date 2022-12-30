public interface IMidiEventSender{
    IEnumerable<String> GetAllInputDeviceNames();
    String GetCurrentInputDeviceName();
    bool SetCurrentInputDevice(int deviceID);
    bool SendMidiInput(bool isNoteOn, int noteNumber, int velocity);
}