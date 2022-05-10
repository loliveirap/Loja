using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.Handlers;
using App.Paladinos.Domain.StoreContext.Commands.ProdutoCommands.Input;

namespace App.Paladinos.Api.Controllers
{
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _repository;
        private readonly ProdutoHandler _produtoHandler;

        public ProdutoController(IProdutoRepository repository, ProdutoHandler produtoHandler)
        {
            _repository = repository;
            _produtoHandler = produtoHandler;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.Get(id));
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] CreateProdutoCommand produto)
        {
            var result = await _produtoHandler.Handle(produto);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] AlterProdutoCommand produto, Guid id)
        {
            var result = await _produtoHandler.Handle(produto, id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _produtoHandler.Handle(id);
            if (result.Sucesso)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}