using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//[ApiController]
//[Route("api/[controller]")] // /api/users
[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController //remove DataContext an use IUserRepository ep 87
{
    //[AllowAnonymous]// di defaulte è allow anonymous
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        //var users = await context.Users.ToListAsync();
        var users = await userRepository.GetUsersAsync();

        var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

        return Ok(usersToReturn);
    }

    //[Authorize]
    //[HttpGet("{id:int}")] // /api/users/1
    [HttpGet("{username}")] // /api/users/1
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        //var user = await context.Users.FindAsync(id);

        var user = await userRepository.GetUserByUsernameAsync(username);

        if (user == null) return NotFound();

        return mapper.Map<MemberDto>(user);
    }
}
