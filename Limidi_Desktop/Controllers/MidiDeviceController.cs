using Microsoft.AspNetCore.Mvc;

namespace Limidi_Desktop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MidiDeviceController : ControllerBase
    {
        private IMidiEventSender _midiEventSender;
        public MidiDeviceController(IMidiEventSender midiEventSender)
        {
            this._midiEventSender = midiEventSender;
        }

        [HttpGet(Name = "GetMidiDevices")]
        public IEnumerable<String> GetMidiDevices()
        {
            return this._midiEventSender.GetAllInputDeviceNames();
        }

        [HttpPut(Name = "PutMidiDevices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutMidiDevices([FromQuery(Name = "deviceID")] int deviceID)
        {
            if (this._midiEventSender.SetCurrentInputDevice(deviceID))
            {
                return Ok("Current Device: " + this._midiEventSender.GetCurrentInputDeviceName());
            }
            return BadRequest("Invalid device ID");
        }

    }
}
