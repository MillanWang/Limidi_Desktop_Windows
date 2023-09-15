using Limidi_Desktop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Emit;

namespace Limidi_Desktop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        private IVirtualMidi _virtualMidi;
        public HeartbeatController(IVirtualMidi virtualMidi)
        {
            this._virtualMidi = virtualMidi;
            Console.WriteLine("HeartbeatConstructor");

        }


        /**
         * QUICK PROOF OF CONCEPT
         * 
         * The TE-SDK is really cool, and it works easily enough on windows. Virtual inputs work as expected.
         * Gotta try out how this managed-midi stuff works on MacOS and if those v-midis work out the box, it'll just be different implementations of the iMidiEventSender 
         * Gotta love dep-injects
         */

        [HttpGet(Name = "GetHeartbeat")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetHeartbeat()
        {
            _virtualMidi.SendMidiNoteInput(true, 123, 123);
            return Ok("NICE");
        }
    }
}
