using Microsoft.AspNetCore.Mvc;

namespace Limidi_Desktop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MidiInputController : ControllerBase
    {
        private IMidiEventSender _midiEventSender;
        public MidiInputController(IMidiEventSender midiEventSender)
        {
            this._midiEventSender = midiEventSender;
        }

        [HttpGet(Name = "GetMidiInput")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMidiInput(
            [FromQuery(Name = "isNoteOn")] bool isNoteOn,
            [FromQuery(Name = "noteNumber")] int noteNumber,
            [FromQuery(Name = "velocity")] int velocity // Must by between 1 & 127 inclusive
        )
        {
            return this._midiEventSender.SendMidiNoteInput(isNoteOn, noteNumber, velocity) ? Ok() : BadRequest("Invalid input");
        }
    }
}
