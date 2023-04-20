﻿using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;

    public SuperHeroController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> Get()

    {
        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> Get(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero == null)
            return new OkObjectResult(BadRequest("Hero not found."));
        return Ok(hero);
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)

    {
        _context.SuperHeroes.Add(hero);
        await _context.SaveChangesAsync();
        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
    {
        var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
        if (dbHero == null)
            return new OkObjectResult(BadRequest("Hero not found."));
        dbHero.Name = request.Name;
        dbHero.FirstName = request.FirstName;
        dbHero.LastName = request.LastName;
        dbHero.Place = request.Place;
        await _context.SaveChangesAsync();
        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<SuperHero>> Delete(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero == null)
            return new OkObjectResult(BadRequest("Hero not found."));
        return Ok(hero);
    }
}