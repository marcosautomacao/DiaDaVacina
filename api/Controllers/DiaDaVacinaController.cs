using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            _logger.Log(LogLevel.Information, "GetAsync called {0} ", JsonConvert.SerializeObject(pessoa));
            List<Pessoa> result = new List<Pessoa>();

            if (pessoa.Telefone != 0) result.Add(_contextDb.Pessoa.Find(pessoa.Telefone));
            else result = result = _contextDb.Pessoa.ToList();

            return Ok(result);
        } 
        [HttpPost]
        public ActionResult PostAsync([FromQuery] Pessoa pessoa)
        {
            _logger.Log(LogLevel.Information, "PostAsync called {0} ", JsonConvert.SerializeObject(pessoa));
            _contextDb.Pessoa.Add(pessoa);
            _contextDb.SaveChanges();
            return Ok();
        }
      
        [HttpPut]
        public ActionResult PutAsync([FromQuery] Pessoa pessoa)
        {
            _logger.Log(LogLevel.Information, "PutAsync called {0} ", JsonConvert.SerializeObject(pessoa));
            Pessoa result = _contextDb.Pessoa.Find(pessoa.Telefone);
            if (result != null)
            {
                result = pessoa;
                _contextDb.SaveChanges();
            }
            return Ok();
        }        
        [HttpDelete]
        public ActionResult DeleteAsync([FromQuery] Pessoa pessoa)
        {
            _logger.Log(LogLevel.Information, "DeleteAsync called {0} ", JsonConvert.SerializeObject(pessoa));
            _contextDb.Pessoa.Remove(pessoa);
            _contextDb.SaveChanges();
            return Ok();
        }
    }
}
