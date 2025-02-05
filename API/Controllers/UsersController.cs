using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//[ApiController]
//[Route("api/[controller]")] // /api/users
[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController //remove DataContext an use IUserRepository ep 87
{
    //[AllowAnonymous]// di defaulte è allow anonymous
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        //var users = await context.Users.ToListAsync();
        var users = await userRepository.GetUsersAsync();

        return Ok(users);
    }

    //[Authorize]
    //[HttpGet("{id:int}")] // /api/users/1
    [HttpGet("{username}")] // /api/users/1
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
        //var user = await context.Users.FindAsync(id);

        var user = await userRepository.GetUserByUsernameAsync(username);

        if (user == null) return NotFound();

        return user;
    }
}
