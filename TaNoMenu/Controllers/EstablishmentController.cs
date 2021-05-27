using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TaNoMenu.Models.Establishments;
using TaNoMenu.Repositories;

namespace TaNoMenu.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class EstablishmentController : ControllerBase
    {
        private readonly EstablishmentRepository _establishmentRepository;

        public EstablishmentController(IConfiguration configuration)
        {
            _establishmentRepository = new EstablishmentRepository(configuration);
        }
        
        [HttpGet]
        public List<Establishment> Get()
        {
            return _establishmentRepository.FindAll().ToList();
        }
        
        [Route("{establishmentId}")]
        [HttpGet]
        public ActionResult Show(int establishmentId)
        {
            return Ok(_establishmentRepository.FindById(establishmentId));
        }
        
        [HttpDelete]
        [Route("{establishmentId}")]
        public IActionResult Delete(int establishmentId)
        {
            _establishmentRepository.Remove(establishmentId);
            return Ok();
        }
        
        [HttpPut]
        [Route("{establishmentId}")]
        public IActionResult Put(int establishmentId, [FromBody] Establishment establishment)
        {
            establishment.Id = establishmentId;
            _establishmentRepository.Update(establishment);
            return Ok(establishment);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Establishment establishment)
        {
            _establishmentRepository.Add(establishment);
            return Ok(establishment);
        }
    }
}