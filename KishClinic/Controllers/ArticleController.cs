using KishClinic.Entities;
using KishClinic.Models;
using KishClinic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace KishClinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetAll()
        {
            var articles = await _articleService.GetAllAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetById(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create([FromBody] ArticleDto articleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = new Article
            {
                Title = articleDto.Title,
                Content = articleDto.Content,
                Description = articleDto.Description,
                AuthorName = articleDto.AuthorName
            };

            var createdArticle = await _articleService.CreateAsync(article);
            return CreatedAtAction(nameof(GetById), new { id = createdArticle.Id }, createdArticle);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<Article>> Update(int id, [FromBody] ArticleDto articleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = new Article
            {
                Title = articleDto.Title,
                Content = articleDto.Content,
                Description = articleDto.Description,
                AuthorName = articleDto.AuthorName
            };

            var updatedArticle = await _articleService.UpdateAsync(id, article);
            if (updatedArticle == null)
            {
                return NotFound();
            }
            return Ok(updatedArticle);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<Article>> Update(int id, [FromBody] JsonPatchDocument<Article> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var article = await _articleService.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(article, (error) => ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedArticle = await _articleService.UpdateAsync(id, article);
            if (updatedArticle == null)
            {
                return NotFound();
            }

            return Ok(updatedArticle);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _articleService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
