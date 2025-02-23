using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers;

//[ApiController]
//[Route("api/[controller]")] // /api/users
[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController //remove DataContext and use IUserRepository ep 87
{
    //[AllowAnonymous]// di defaulte è allow anonymous
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    //[Authorize]
    //[HttpGet("{id:int}")] // /api/users/1
    [HttpGet("{username}")] // /api/users/1
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        //var user = await context.Users.FindAsync(id);

        var user = await userRepository.GetMemberByUsernameAsync(username);

        if (user == null) return NotFound();

        return user;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (username == null)
        {
            return BadRequest("No username found in token");
        }

        var user = await userRepository.GetUserByUsernameAsync(username);

        if (user == null)
        {
            return BadRequest("Could not find user");
        }

        //la prop user è tracciata da EF, se dopo il mapper, il quale aggiorna l'oggetto user con le modifiche ricevute dal dto
        //si chiama save chacnges allora si aggiornano le info nel db
        mapper.Map(memberUpdateDto, user);

        if (await userRepository.SaveAllAsync())
        {
            return NoContent();//è come ritornare Ok, 200 ma dato che siamo in PUT si usa questo
        }

        return BadRequest("Failed to update the user");
    }
}
