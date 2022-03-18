using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes_API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Notes_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static List<Category> categories = new List<Category>()
        {
            new Category(){ Name="To do", Color = "#f0f0f0", Id = 1},
            new Category(){ Name="Doing", Color = "#F1ED95", Id = 2},
            new Category(){ Name="Done", Color = "#A0D582", Id = 3},

        };

        /// <summary>
        /// Returneaza toate categoriile
        /// </summary>
        /// <response code="200">Daca totul e ok</response>
        /// <returns>Returneaza toate categoriile</returns>
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(categories);
        }

        /// <summary>
        /// Returneaza 1 categorie dupa Id
        /// </summary>
        /// <param name="id">Id-ul categoriei</param>
        /// <returns>Categoria gasita</returns>
        /// <response code="200">Daca a gasit categoria</response>
        /// <response code="404">Daca nu a gasit categoria</response>
        [HttpGet("{id}", Name = "GetCategoryById")]
        public IActionResult GetCategoryById([FromRoute] long id)
        {
            var found = categories.FirstOrDefault(c => c.Id == id);
            if (found == null)
                return NotFound();
            return Ok(found);
        }

        /// <summary>
        /// Adauga o categorie noua
        /// </summary>
        /// <param name="category">
        /// Din body
        /// Categoria care trebuie adaugate
        /// </param>
        /// <returns>Categoria adaugata</returns>
        /// <response code="201">Daca categoria a fost adaugata</response>
        [HttpPost]
        public IActionResult PostCategory([FromBody] Category category)
        {
            var lastid = categories.Last().Id;
            category.Id = ++lastid;
            categories.Add(category);
            return CreatedAtRoute("GetCategoryById", new { id = category.Id }, category);
        }

        /// <summary>
        /// Actualizaza 1 categorie dupa Id
        /// </summary>
        /// <param name="id">
        /// Id-ul categoriei care trebuie actualizata
        /// </param>
        /// <param name="category">
        /// Din body
        /// Categoria cu proprietatile modificate
        /// </param>
        /// <returns>Categoria actualizata</returns>
        /// <response code="200">Daca actualizare este cu succes</response>
        /// <response code="404">Daca nu a fost gasita categoria care trebuie actualizata</response>
        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] long id, [FromBody] Category category)
        {
            var index = categories.FindIndex(c => c.Id == id);
            if (index == -1)
                return NotFound();
            categories[index].Name = category.Name;
            categories[index].Color = category.Color;
            return Ok(categories[index]);

        }

        /// <summary>
        /// Sterge 1 categorie dupa id
        /// </summary>
        /// <param name="id">
        /// Id-ul categoriei care trebuie stearsa
        /// </param>
        /// <returns>Categoria stearsa</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromRoute] long id)
        {
            var deletedCategory = categories.FirstOrDefault(c => c.Id == id);
            if (deletedCategory == null)
                return NotFound();
            categories = categories.Where(c => c.Id != id).ToList();
            return Ok(deletedCategory);
        }
    }
}
