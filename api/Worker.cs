using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime today = DateTime.Now;

            TimeSpan ts = new TimeSpan(int.Parse(_configuration.GetSection("TimeToAct").Value), 30, 0);

            DateTime tomorrow = today.Date + ts;

            while (!stoppingToken.IsCancellationRequested)
            {
                today = DateTime.Now;
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                if (today.Date == tomorrow.Date && today.Hour == tomorrow.Hour)
                {
                    tomorrow.AddDays(1);
                    var pessoas = new List<Pessoa>();

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _contextDb = scope.ServiceProvider.GetRequiredService<DiaDaVacinaContext>();
                        var dataVacina = _contextDb.DataVacina.Where(dv => dv.DataInicio.Date >= today.Date).ToList();

                        if (dataVacina.Count > 0)
                        {
                            foreach (var v in dataVacina)
                            {
                                //Vacinados de hoje
                                var NascimentoMinimo = v.DataInicio.AddDays(-v.idade);
                                var pessoasParaNotificarHoje = pessoas.Select(p => p.DataNascimento.Date == NascimentoMinimo.Date && p.Estado == v.Estado && p.Sexo == v.Sexo);
                                foreach (var p in pessoasParaNotificarHoje)
                                {
                                    Console.WriteLine("Hoje é dia de vacinar");
                                }

                                //Vacinados de Amanha
                                NascimentoMinimo = v.DataInicio.AddDays(-1);
                                var pessoasParaNotificarAmanha = pessoas.Select(p => p.DataNascimento.Date == NascimentoMinimo.Date && p.Estado == v.Estado && p.Sexo == v.Sexo);
                                foreach (var p in pessoasParaNotificarHoje)
                                {
                                    Console.WriteLine("Amanha é dia de vacinar");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
