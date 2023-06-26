using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Limidi_Desktop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MidiControlChangeController : ControllerBase
    {
        private IMidiEventSender _midiEventSender;
        public MidiControlChangeController(IMidiEventSender midiEventSender)
        {
            this._midiEventSender = midiEventSender;
        }

        [HttpGet(Name = "GetControlChangeInput")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetControlChangeInput(
            [FromQuery(Name = "controlIndex")] int controlIndex,
            [FromQuery(Name = "level")] int level
        )
        {
            return this._midiEventSender.SendMidiControlChange(controlIndex, level) ? Ok() : BadRequest("Invalid input");
        }
    }
}
