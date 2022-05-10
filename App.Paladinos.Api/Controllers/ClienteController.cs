
using App.Paladinos.Domain.StoreContext.Commands.ClienteCommands.Input;
using App.Paladinos.Domain.StoreContext.Commands.Handlers;
using App.Paladinos.Domain.StoreContext.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Paladinos.Api.Controllers
{
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _repository;
        private readonly ClienteHandler _clientHandler;

        public ClienteController(IClienteRepository repository, ClienteHandler clienteHandler)
        {
            _repository = repository;
            _clientHandler = clienteHandler;
        }

        [HttpGet]
        [Route("clientes")]
        // Cache no Cliente
        //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        //Cache no servidor
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.Get());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.Get(id));
        }

        [HttpGet]
        [Route("{id}/pedidos")]
        public async Task<IActionResult> GetPedidos(Guid id)
        {
            return Ok(await _repository.GetPedido(id));
        }

        [HttpGet]
        [Route("{cnpj}/pedidos")]
        public async Task<IActionResult> GetPedidos(string cnpj)
        {
            return Ok(await _repository.GetByCnpj(cnpj));
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] CreateClienteCommand cliente)
        {
            var result = await _clientHandler.Handle(cliente);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] AlterClienteCommand cliente, Guid id)
        {
            var result = await _clientHandler.Handle(cliente, id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _clientHandler.Handle(id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
