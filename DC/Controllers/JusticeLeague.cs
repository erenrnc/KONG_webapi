using Microsoft.AspNetCore.Mvc;

namespace DC.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JusticeLeague : ControllerBase
    {
        [HttpGet]
        public string Strongest()
        {
            return "Batman!";
        }
    }
}
