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
            try
            {
                String token = TokenRepository.GenerateToken(_establishmentRepository.Authenticable(model));
                return Ok(new {
                    token
                });
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    message = "Usuário ou senha inválidos!"
                });
            }
        }
    }
}