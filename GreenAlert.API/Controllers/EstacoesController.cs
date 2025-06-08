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
    /// Retorna todas as estações cadastradas.
    /// </summary>
    /// <remarks>Lista de estações com seus respectivos sensores.</remarks>
    /// <response code="200">Estações retornadas com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(_service.ObterTodas());

    /// <summary>
    /// Retorna uma estação por ID.
    /// </summary>
    /// <param name="id">ID da estação.</param>
    /// <response code="200">Estação encontrada.</response>
    /// <response code="404">Estação não encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var estacao = _service.ObterPorId(id);
        return estacao is null ? NotFound() : Ok(estacao);
    }

    /// <summary>
    /// Cadastra uma nova estação.
    /// </summary>
    /// <param name="estacao">Dados da estação.</param>
    /// <response code="201">Estação criada.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Estacao estacao)
    {
        if (estacao == null || string.IsNullOrEmpty(estacao.Nome))
            return BadRequest("Dados inválidos para criação.");

        var nova = _service.Criar(estacao);
        return CreatedAtAction(nameof(GetById), new { id = nova.EstacaoId }, nova);
    }

    /// <summary>
    /// Atualiza uma estação existente.
    /// </summary>
    /// <param name="id">ID da estação.</param>
    /// <param name="estacao">Dados atualizados.</param>
    /// <response code="200">Estação atualizada.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="404">Estação não encontrada.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(Guid id, [FromBody] Estacao estacao)
    {
        if (estacao == null || id != estacao.EstacaoId)
            return BadRequest("Dados inválidos para atualização.");

        var atualizado = _service.Atualizar(id, estacao);
        return atualizado ? Ok(estacao) : NotFound();
    }

    /// <summary>
    /// Remove uma estação.
    /// </summary>
    /// <param name="id">ID da estação.</param>
    /// <response code="204">Remoção bem-sucedida.</response>
    /// <response code="400">ID inválido.</response>
    /// <response code="404">Estação não encontrada.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Id inválido.");

        var removido = _service.Remover(id);
        return removido ? NoContent() : NotFound();
    }
}