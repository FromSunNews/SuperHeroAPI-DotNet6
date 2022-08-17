using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero is not found");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superhero)
        {
            context.SuperHeroes.Add(superhero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await context.SuperHeroes.FindAsync(request.Id);

            if (hero == null)
            {
                return BadRequest("Hero is not found");
            }
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero is not found");
            }
             context.SuperHeroes.Remove(hero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToArrayAsync());
        }
    }
}
