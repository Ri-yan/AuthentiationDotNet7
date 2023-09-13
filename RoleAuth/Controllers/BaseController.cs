using Microsoft.AspNetCore.Mvc;

namespace RoleAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        public class ApiResponse
        {
            public object Data { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public List<ErroInfo> Errors { get; set; }
        }
        public class ErroInfo
        {
            public int Code { get; set; }
            public string Error { get; set; }
        }
        public class Response
        {
            public string? Status { get; set; }
            public string? Message { get; set; }
        }
    }
}
