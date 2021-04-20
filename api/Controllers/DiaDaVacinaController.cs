using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiaDaVacinaController : ControllerBase
    {

        private readonly ILogger<DiaDaVacinaController> _logger;

        public DiaDaVacinaController(ILogger<DiaDaVacinaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAsync([FromQuery] Pessoa pessoa)
        {
            return Ok("Hello World");
        }
    }

    public class Pessoa
    {
        public string Telefone;
        public string Estado;
        public DateTime DataNascimento;
    }
}
