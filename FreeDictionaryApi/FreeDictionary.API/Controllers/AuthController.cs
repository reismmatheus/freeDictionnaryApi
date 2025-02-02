﻿using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.CrossCutting.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using static FreeDictionary.Application.Model.AuthModel;

namespace FreeDictionary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        [HttpPost("Singup")]
        public async Task<IActionResult> Singup(SingupModel model)
        {
            var (result, cache) = await _authBusiness.Singup(model);
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");

            if (result == null)
                return BadRequest(new { message = "Error message" });

            return Ok(result);
        }

        [HttpPost("Singin")]
        public async Task<IActionResult> Singin(SinginModel model)
        {
            var (result, cache) = await _authBusiness.Singin(model);
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
