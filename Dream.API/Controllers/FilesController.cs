using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{
    [Route("api/Files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            string path = "webapiBanner.rar";

            if (!System.IO.File.Exists(path))
            {
                return NotFound();  
            }

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "text/plain" , Path.GetFileName(path));

        }
    }
}
