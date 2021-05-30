using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaNoMenu.Models;
using TaNoMenu.Repositories;

namespace TaNoMenu.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeRepository _recipeRepository;

        public RecipeController(IConfiguration configuration)
        {
            _recipeRepository = new RecipeRepository(configuration);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public List<Recipe> Get()
        {
            return _recipeRepository.FindAll().ToList();
        }
        
        [Route("{recipeId}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Show(int recipeId)
        {
            return Ok(_recipeRepository.FindById(recipeId));
        }
        
        [HttpDelete]
        [Route("{recipeId}")]
        [Authorize]
        public IActionResult Delete(int recipeId)
        {
            _recipeRepository.Remove(recipeId);
            return Ok();
        }
        
        [HttpPut]
        [Route("{recipeId}")]
        [Authorize]
        public IActionResult Put(int recipeId, [FromBody] Recipe recipe)
        {
            recipe.Id = recipeId;
            _recipeRepository.Update(recipe);
            return Ok(recipe);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Recipe recipe)
        {
            _recipeRepository.Add(recipe);
            return Ok(recipe);
        }
    }
}