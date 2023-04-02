using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    //[Route("v1")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]//localhost:PORT/v1/categories
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {

            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE4 - Não foi possível BUSCAR TODAS as categorias."));
            }

            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05X10 - Falha interna do servidor"));

            }



        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
                [FromRoute] int id,
                    [FromServices] BlogDataContext context)
        {

            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>($"Categoria de Id '{id}' não encontrada."));
                //retorna um erro 404

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE5 - Não foi possível BUSCAR a categoria por ID."));
            }

            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05X11 - Falha interna do servidor"));

            }

        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
                [FromBody] EditorCategoryViewModel model,
                   [FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower()
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}",
                        new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE6 - Não foi possível INSERIR a categoria."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05X12 - Falha interna do servidor"));

            }

        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
                [FromBody] EditorCategoryViewModel model,
                   [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>($"Categoria de Id '{id}' não encontrada."));
                //retorna um erro 404

                category.Name = model.Name;
                category.Slug = model.Slug.ToLower();

                context.Categories.Update(category);
                await context.SaveChangesAsync();


                return Ok(new ResultViewModel<Category>(category));
            }

            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE7 - Não foi possível ATUALIZAR a categoria."));
            }

            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05X13 - Falha interna do servidor"));

            }

        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
                [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                                .Categories
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>($"Categoria de Id '{id}' não encontrada."));




                context.Categories.Remove(category);
                await context.SaveChangesAsync();


                return Ok(new ResultViewModel<Category>(category));
            }


            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE8 - Não foi possível EXCLUIR a categoria."));
            }

            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05X14 - Falha interna do servidor"));

            }


        }

    }
}
