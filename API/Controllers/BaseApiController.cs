using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        //DRY dont repeat yourself - uso questo controller per non ripetere la route e apiController
    }
}
