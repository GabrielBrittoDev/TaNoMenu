using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaNoMenu.Exceptions;
using TaNoMenu.Models.Establishments;
using TaNoMenu.Repositories;

namespace TaNoMenu.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class EstablishmentController : Controller
    {
        private readonly EstablishmentRepository _establishmentRepository;

        public EstablishmentController(IConfiguration configuration)
        {
            _establishmentRepository = new EstablishmentRepository(configuration);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Establishment>> Get()
        {
            try
            {
                return Ok(_establishmentRepository.FindAll().ToList());
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }
        
        [Route("{establishmentId}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Show(int establishmentId)
        {
            try
            {
                return Ok(_establishmentRepository.FindById(establishmentId));
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }
        
        [HttpDelete]
        [Route("{establishmentId}")]
        [Authorize]
        public IActionResult Delete(int establishmentId)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId != establishmentId)
                    return Unauthorized("Sem autorização para realizar esta ação");
                
                _establishmentRepository.Remove(establishmentId);
                return Ok("Estabelecimento deletado com sucesso");
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }
        
        [HttpPut]
        [Route("{establishmentId}")]
        [Authorize]
        public IActionResult Put(int establishmentId, [FromBody] Establishment establishment)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId != establishmentId)
                    return Unauthorized("Sem autorização para realizar esta ação");
                
                establishment.Id = establishmentId;
                _establishmentRepository.Update(establishment);
                return Ok(establishment);
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Establishment establishment)
        {
            try
            {
                _establishmentRepository.Add(establishment);
                return Ok(establishment);
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }
    }
}