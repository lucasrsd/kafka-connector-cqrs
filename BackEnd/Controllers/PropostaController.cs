using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.WebApi.Dao.NoSql.Proposta;
using BackEnd.WebApi.Dao.Transacional.Proposta;
using BackEnd.WebApi.Models.Proposta;
using BackEnd.WebApi.Topics.Proposta;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEnd.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropostaController : ControllerBase
    {
        private readonly IPropostaTopic propostaTopicClient;
        private readonly IPropostaDao propostaTransacionalDao;
        private readonly IPropostaRepository propostaNoSqlDao;

        public PropostaController(IPropostaTopic transferenciaTopic, IPropostaDao propostaTransacionalDao, IPropostaRepository propostaNoSqlDao)
        {
            this.propostaTopicClient = transferenciaTopic;
            this.propostaTransacionalDao = propostaTransacionalDao;
            this.propostaNoSqlDao = propostaNoSqlDao;
        }

        [HttpGet("todas")]
        public async Task<IActionResult> GetTodas()
        {
            Console.WriteLine("Obtendo todas as propostas");
            var todasPropostas = this.propostaNoSqlDao.ObterTodas();
            return Ok(todasPropostas);
        }

        [HttpGet("porIdProposta/{idProposta:int}")]
        public async Task<IActionResult> GetPorId([FromRoute] int idProposta)
        {
            var propostaResult = this.propostaNoSqlDao.ObterPorProposta(idProposta);
            return Ok(propostaResult);
        }

        [HttpGet("porCliente/{cliente}")]
        public async Task<IActionResult> GetPorCliente([FromRoute] string cliente)
        {
            var propostaResult = this.propostaNoSqlDao.ObterPorCliente(cliente);
            return Ok(propostaResult);
        }

        [HttpGet("porVendedor/{vendedor}")]
        public async Task<IActionResult> GetPorVendedor([FromRoute] string vendedor)
        {
            var propostaResult = this.propostaNoSqlDao.ObterPorVendedor(vendedor);
            return Ok(propostaResult);
        }

        [HttpPut("EventoEmProposta")]
        public async Task<IActionResult> Put([FromBody] PropostaDTO dtoRequest)
        {
            Console.WriteLine($"Recebendo request de evento para atualizar base MongoDB!! Payload: {JsonConvert.SerializeObject(dtoRequest)}");
            if (dtoRequest.IdProposta <= 0) return BadRequest("Informe uma proposta");

            this.propostaNoSqlDao.InserirOuAtualizar(dtoRequest);
            return Ok();
        }

        [HttpPost("Transacional")]
        public async Task<IActionResult> PostTransacional([FromBody] PropostaRequest request)
        {
            this.propostaTransacionalDao.CadastrarNovaProposta(request);
            return Ok();
        }
    }
}