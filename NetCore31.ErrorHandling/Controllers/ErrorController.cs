using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetCore31.ErrorHandling.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("Argument")]
        public IEnumerable<IActionResult> Argument()
        {
            throw new ArgumentException("The argument exception");
        }

        [HttpGet("FileNotFound")]
        public IEnumerable<IActionResult> FileNotFound()
        {
            throw new FileNotFoundException("The file not found exception");
        }
    }
}
