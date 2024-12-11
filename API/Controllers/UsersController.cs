using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//[ApiController]
//[Route("api/[controller]")] // /api/users
public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous]//di defaulte è allow anonymous
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();

        return users; // it's like return Ok(users) because the method return is ActionResult
        // so i can return Ok(), NotFound(), BadRequest()
    }

    [Authorize]
    [HttpGet("{id:int}")] // /api/users/1
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }
}
