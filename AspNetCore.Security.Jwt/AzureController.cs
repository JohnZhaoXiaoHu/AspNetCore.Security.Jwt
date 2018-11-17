﻿using AspNetCore.Security.Jwt.AzureAD;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspNetCore.Security.Jwt
{
    /// <summary>
    /// Azure Contoller for authentication
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AzureController : Controller
    {
        private readonly IAuthentication<AzureADAuthModel, AzureADResponseModel> authentication;

        public AzureController(IAuthentication<AzureADAuthModel, AzureADResponseModel> authentication)
        {
            this.authentication = authentication;
        }

        [Route("/azure")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var response = await this.authentication.IsValidUser(null);

            if (!string.IsNullOrEmpty(response.AccessToken))
                return new ObjectResult(response.AccessToken);

            return BadRequest();
        }
    }
}
