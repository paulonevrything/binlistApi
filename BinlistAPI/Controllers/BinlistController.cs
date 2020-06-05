using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetBinDetails([FromBody] string cardIin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _binlistService.GetBinDetails(cardIin));
        }
    }
}
