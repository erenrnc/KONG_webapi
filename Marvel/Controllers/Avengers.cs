using Microsoft.AspNetCore.Mvc;

namespace Marvel.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Avengers : ControllerBase
    {
        [HttpGet]
        public string Strongest()
        {
            return "Iron Man!";
        }
    }
}
