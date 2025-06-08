using Microsoft.AspNetCore.Mvc;
using GreenAlert.Core.Models;
using GreenAlert.Service.Services;
using System;

namespace GreenAlert.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstacoesController : ControllerBase
{
    private readonly EstacaoService _service;

    public EstacoesController(EstacaoService service) => _service = service;

    /// <summary>
    /// Retorna todas as esta��es cadastradas.
    /// </summary>
    /// <remarks>Lista de esta��es com seus respectivos sensores.</remarks>
    /// <response code="200">Esta��es retornadas com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(_service.ObterTodas());

    /// <summary>
    /// Retorna uma esta��o por ID.
    /// </summary>
    /// <param name="id">ID da esta��o.</param>
    /// <response code="200">Esta��o encontrada.</response>
    /// <response code="404">Esta��o n�o encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var estacao = _service.ObterPorId(id);
        return estacao is null ? NotFound() : Ok(estacao);
    }

    /// <summary>
    /// Cadastra uma nova esta��o.
    /// </summary>
    /// <param name="estacao">Dados da esta��o.</param>
    /// <response code="201">Esta��o criada.</response>
    /// <response code="400">Dados inv�lidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Estacao estacao)
    {
        if (estacao == null || string.IsNullOrEmpty(estacao.Nome))
            return BadRequest("Dados inv�lidos para cria��o.");

        var nova = _service.Criar(estacao);
        return CreatedAtAction(nameof(GetById), new { id = nova.EstacaoId }, nova);
    }

    /// <summary>
    /// Atualiza uma esta��o existente.
    /// </summary>
    /// <param name="id">ID da esta��o.</param>
    /// <param name="estacao">Dados atualizados.</param>
    /// <response code="200">Esta��o atualizada.</response>
    /// <response code="400">Requisi��o inv�lida.</response>
    /// <response code="404">Esta��o n�o encontrada.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(Guid id, [FromBody] Estacao estacao)
    {
        if (estacao == null || id != estacao.EstacaoId)
            return BadRequest("Dados inv�lidos para atualiza��o.");

        var atualizado = _service.Atualizar(id, estacao);
        return atualizado ? Ok(estacao) : NotFound();
    }

    /// <summary>
    /// Remove uma esta��o.
    /// </summary>
    /// <param name="id">ID da esta��o.</param>
    /// <response code="204">Remo��o bem-sucedida.</response>
    /// <response code="400">ID inv�lido.</response>
    /// <response code="404">Esta��o n�o encontrada.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Id inv�lido.");

        var removido = _service.Remover(id);
        return removido ? NoContent() : NotFound();
    }
}