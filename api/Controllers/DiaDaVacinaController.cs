using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiaDaVacinaController : ControllerBase
    {

        private readonly ILogger<DiaDaVacinaController> _logger;
        private readonly DiaDaVacinaContext _contextDb;

        public DiaDaVacinaController(ILogger<DiaDaVacinaController> logger, DiaDaVacinaContext contextDb)
        {
            _logger = logger;
            _contextDb = contextDb;
        }

        [HttpGet]
        public ActionResult GetAsync([FromQuery] Pessoa pessoa)
        {
            _contextDb.Add(pessoa);
            DbSet<Pessoa> result = _contextDb.Pessoa;
            result.Add(pessoa);
            _contextDb.SaveChanges();
            return Ok();
        }
    }
}
