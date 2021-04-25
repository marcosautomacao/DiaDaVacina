using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Comtele.Sdk.Services;
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
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _contextDb = scope.ServiceProvider.GetRequiredService<DiaDaVacinaContext>();
                        var dataVacina = _contextDb.DataVacina.Where(dv => dv.DataInicio.Date >= today.Date).ToList();

                        if (dataVacina.Count > 0)
                        {
                            foreach (var v in dataVacina)
                            {
                                var telefoneList = new List<string>();
                                //Vacinados de hoje
                                var NascimentoMinimo = v.DataInicio.AddYears(-v.idade);
                                List<Pessoa> pessoasParaNotificarHoje = _contextDb.Pessoa.Where(p => p.DataNascimento.Date == NascimentoMinimo.Date && p.Estado == v.Estado && p.Sexo == v.Sexo).ToList();
                                foreach (var p in pessoasParaNotificarHoje)
                                {
                                    telefoneList.Add(p.Telefone.ToString());                                    
                                }
                                if (telefoneList.Count > 0)
                                    sendSms("Hoje é dia de vacinar", telefoneList.ToArray());

                                telefoneList = new List<string>();
                                //Vacinados de Amanha
                                NascimentoMinimo = v.DataInicio.AddDays(-1);
                                var pessoasParaNotificarAmanha = _contextDb.Pessoa.ToList().Where(p => p.DataNascimento.Date == NascimentoMinimo.Date && p.Estado == v.Estado && p.Sexo == v.Sexo);
                                foreach (var p in pessoasParaNotificarAmanha)
                                {
                                    telefoneList.Add(p.Telefone.ToString());
                                }
                                if (telefoneList.Count > 0)
                                    sendSms("Amanha é dia de vacinar", telefoneList.ToArray());
                            }
                        }
                    }
                }
            }
        }

        private void sendSms(string mensagem, string[] telefones )
        {
            var textMessageService = new TextMessageService(_configuration.GetSection("ApiKeySms").Value);
            var result = textMessageService.Send(
             "appVacinas",             // Sender: Id de requisicao da sua aplicacao para ser retornado no relatorio, pode ser passado em branco.
             mensagem,                 // Content: Conteudo da mensagem a ser enviada.
             telefones                 // Receivers: Numero de telefone que vai ser enviado o SMS.
            );

            if (result.Success)
            {
                _logger.LogInformation("A mensagem foi enviada com sucesso.");
            }
            else
            {
                _logger.LogInformation("A mensagem não pode ser enviada. Detalhes: " + result.Message);
            }
        }
    }
}
