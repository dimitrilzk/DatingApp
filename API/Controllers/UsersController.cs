using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

//[ApiController]
//[Route("api/[controller]")] // /api/users
[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController //remove DataContext and use IUserRepository ep 87
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
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

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

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if(user == null)
        {
            return BadRequest("Cannot update user");
        }

        var result = await photoService.AddPhotoAsync(file);

        if(result.Error != null)
        {
            return BadRequest(result.Error.Message);
        }

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (user.Photos.Count == 0)
        {
            photo.IsMain = true;
        }

        user.Photos.Add(photo);

        if(await userRepository.SaveAllAsync())
        {
            //spiegazione ep 124
            return CreatedAtAction(nameof(GetUser),
                new { username = user.UserName },
                mapper.Map<PhotoDto>(photo));
        }

        return BadRequest("Problem adding photo");
    }

    [HttpPut("set-main-photo/{photoId:int}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null)
        {
            return BadRequest("Could not find user");
        }

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null || photo.IsMain)
        {
            return BadRequest("Cannot use this as main photo");
        }

        var currenMain = user.Photos.FirstOrDefault(x => x.IsMain);

        if (currenMain != null)
        {
            currenMain.IsMain = false;
        }

        photo.IsMain = true;

        if (await userRepository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem setting main photo");
    }

    [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null)
        {
            return BadRequest("User not found");
        }

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null || photo.IsMain)
        {
            return BadRequest("This photo cannot be deleted");
        }
        
        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }
        }

        user.Photos.Remove(photo);

        if (await userRepository.SaveAllAsync())
        {
            return Ok();
        }

        return BadRequest("Problem deleting photo");
    }
}
