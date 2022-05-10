using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.Handlers;
using App.Paladinos.Domain.StoreContext.Commands.EnderecoCommands.Input;

namespace App.Paladinos.Api.Controllers
{
    [Route("[controller]")]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoRepository _repository;
        private readonly EnderecoHandler _enderecoHandler;

        public EnderecoController(IEnderecoRepository repository, EnderecoHandler enderecoHandler)
        {
            _repository = repository;
            _enderecoHandler = enderecoHandler;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.Get(id));
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] CreateEnderecoCommand endereco)
        {
            var result = await _enderecoHandler.Handle(endereco);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] AlterEnderecoCommand endereco, Guid id)
        {
            var result = await _enderecoHandler.Handle(endereco, id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _enderecoHandler.Handle(id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}