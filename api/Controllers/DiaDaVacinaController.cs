using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
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

        public DiaDaVacinaController(ILogger<DiaDaVacinaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAsync([FromQuery] Pessoa pessoa)
        {

            _contextDb.Add(pessoa);
            var result = _contextDb.Find<Pessoa>();
            return Ok("Hello World");
        }
    }

    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public string Telefone;
        public string Estado;
        public DateTime DataNascimento;
    }

    public class DiaDaVacinaContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }

        public DiaDaVacinaContext(DbContextOptions<DiaDaVacinaContext> options) :
            base(options)
        {
        }
    }

}
