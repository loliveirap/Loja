using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.Handlers;
using App.Paladinos.Domain.StoreContext.Commands.CondicaoPagamentoCommands.Input;

namespace App.Paladinos.Api.Controllers
{
    [Route("[controller]")]
    public class CondicaoPagamentoController : Controller
    {
        private readonly ICondicaoPagamentoRepository _repository;
        private readonly CondicaoPagamentoHandler _condicaoPagamentoHandler;

        public CondicaoPagamentoController(ICondicaoPagamentoRepository repository, CondicaoPagamentoHandler condicaoPagamentoHandler)
        {
            _repository = repository;
            _condicaoPagamentoHandler = condicaoPagamentoHandler;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] CreateCondicaoPagamentoCommand condicaoPagamento)
        {
            var result = await _condicaoPagamentoHandler.Handle(condicaoPagamento);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] AlterCondicaoPagamentoCommand condicaoPagamento, Guid id)
        {
            var result = await _condicaoPagamentoHandler.Handle(condicaoPagamento, id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _condicaoPagamentoHandler.Handle(id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}