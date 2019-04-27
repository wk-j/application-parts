using Microsoft.AspNetCore.Mvc;

namespace MyLibrary.Controllers {

    [Route("api/[controller]/[action]")]
    public class HelloController {

        [HttpGet]
        public string Hi() => "Hello";
    }
}