using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaNoMenu.Models.Establishments;
using TaNoMenu.Repositories;

namespace TaNoMenu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly EstablishmentRepository _establishmentRepository;

        public AuthenticationController(IConfiguration configuration)
        {
            _establishmentRepository = new EstablishmentRepository(configuration);
        }
        
        [HttpPost]
        [Route("login")]
        public ActionResult Authenticate([FromBody]Establishment model)
        {
            String token = TokenRepository.GenerateToken(model);

            if (!_establishmentRepository.Authenticable(model))
            {
                return NotFound(new
                {
                    message = "Usuário ou senha inválidos!"
                });
            }

                return Ok(new {
                    token
            });
        }
    }
}