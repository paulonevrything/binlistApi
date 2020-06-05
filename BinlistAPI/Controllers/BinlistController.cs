using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinlistAPI.Models;
using BinlistAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BinlistAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BinlistController : ControllerBase
    {
        private readonly IBinlistService _binlistService;
        public BinlistController(IBinlistService binlistService)
        {
            _binlistService = binlistService;
        }

        [HttpGet]
        [Route("GetBinDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBinDetails([FromQuery] BinlistRequestModel binlistRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _binlistService.GetBinDetails(binlistRequest.cardIin));
        }
    }
}
