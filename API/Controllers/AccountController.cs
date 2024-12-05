using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController(DataContext context) : BaseApiController
    {
        //In the API Controllers we have an[ApiController] attribute.One of the features of this attribute
        //is automatic property binding to the parameters of the endpoints.The basic rules are if the argument
        //is a string property then it will look in the query string params, and if it is an object then
        //it will look in the body of the request to match this.  This can be overridden if needed
        //by supplying the [FromQuery] or[FromBody] attributes
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto) // ep33
        // di default i parametri passati come stringhe vengono recuperati dalla query string
        // oppure lo si puo specificare: Register([FromQuery]string username, string password)
        // oppure si può indicare che i parametri sono da cercare nel body: Register([FromBody]string username, string password)
        // il modo migliore è comunque passare un oggetto (DTO) in questo modo di default l'oggetto verrà cercato nel Body ep34
        {
            if(await UserExist(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }

            using var hmac = new HMACSHA512(); // usare lo using permette ri risparmiare memoria in quanto
            //appena si avrà finito di usare l'istanza della classe istanziata: new HMACSHA512() il metodo dispose
            //verrà chiamato e si occuperà di chiudere immediatamente lo scope e quindi non consumerà più memoria

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
        private async Task<bool> UserExist(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower()); //Bob != bob
        }
    }
}
