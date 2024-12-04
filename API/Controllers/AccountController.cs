using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController(DataContext context) : BaseApiController
    {
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<AppUser>> Register(string username, string password) // ep33
        // di default i parametri passati come stringhe vengono recuperati dalla query string
        // oppure lo si puo specificare: Register([FromQuery]string username, string password)
        // oppure si può indicare che i parametri sono da cercare nel body: Register([FromBody]string username, string password)
        // il modo migliore è comunque passare un oggetto (DTO) in questo modo di default l'oggetto verrà cercato nel Body ep34
        {
            using var hmac = new HMACSHA512(); // usare lo using permette ri risparmiare memoria in quanto
            //appena si avrà finito di usare l'istanza della classe istanziata: new HMACSHA512() il metodo dispose
            //verrà chiamato e si occuperà di chiudere immediatamente lo scope e quindi non consumerà più memoria

            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

    }
}
