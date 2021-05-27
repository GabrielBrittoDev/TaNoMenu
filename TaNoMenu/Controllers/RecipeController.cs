using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Recipe> Get()
        {
            return _recipeRepository.FindAll().ToList();
        }
        
        [Route("{recipeId}")]
        [HttpGet]
        public IActionResult Show(int recipeId)
        {
            return Ok(_recipeRepository.FindById(recipeId));
        }
        
        [HttpDelete]
        [Route("{recipeId}")]
        public IActionResult Delete(int recipeId)
        {
            _recipeRepository.Remove(recipeId);
            return Ok();
        }
        
        [HttpPut]
        [Route("{recipeId}")]
        public IActionResult Put(int recipeId, [FromBody] Recipe recipe)
        {
            recipe.Id = recipeId;
            _recipeRepository.Update(recipe);
            return Ok(recipe);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Recipe recipe)
        {
            _recipeRepository.Add(recipe);
            return Ok(recipe);
        }
    }
}