using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.Handlers;
using App.Paladinos.Domain.StoreContext.Commands.PedidoCommands.Input;

namespace App.Paladinos.Api.Controllers
{
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _repository;
        private readonly PedidoHandler _pedidoHandler;
        public PedidoController(IPedidoRepository repository, PedidoHandler pedidoHandler)
        {
            _repository = repository;
            _pedidoHandler = pedidoHandler;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] CreatePedidoCommand pedido)
        {
            var result = await _pedidoHandler.Handle(pedido);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] AlterPedidoCommand pedido, Guid id)
        {
            var result = await _pedidoHandler.Handle(pedido, id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _pedidoHandler.Handle(id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}