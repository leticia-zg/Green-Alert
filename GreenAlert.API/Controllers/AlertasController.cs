using Microsoft.AspNetCore.Mvc;
using GreenAlert.Core.Models;
using GreenAlert.Service.Services;
using System;

namespace GreenAlert.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertasController : ControllerBase
{
    private readonly AlertaService _service;

    public AlertasController(AlertaService service) => _service = service;

    /// <summary>
    /// Retorna todos os alertas registrados.
    /// </summary>
    /// <remarks>Inclui os dados do sensor associado.</remarks>
    /// <response code="200">Alertas retornados com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(_service.ObterTodos());

    /// <summary>
    /// Retorna um alerta específico por ID.
    /// </summary>
    /// <param name="id">ID do alerta.</param>
    /// <response code="200">Alerta encontrado.</response>
    /// <response code="404">Alerta não encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var alerta = _service.ObterPorId(id);
        return alerta is null ? NotFound() : Ok(alerta);
    }

    /// <summary>
    /// Cadastra um novo alerta.
    /// </summary>
    /// <param name="alerta">Dados do alerta.</param>
    /// <response code="201">Alerta criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Alerta alerta)
    {
        if (alerta == null || string.IsNullOrEmpty(alerta.Tipo))
            return BadRequest("Dados inválidos para criação.");

        var novo = _service.Criar(alerta);
        return CreatedAtAction(nameof(GetById), new { id = novo.AlertaId }, novo);
    }

    /// <summary>
    /// Atualiza um alerta existente.
    /// </summary>
    /// <param name="id">ID do alerta.</param>
    /// <param name="alerta">Dados atualizados do alerta.</param>
    /// <response code="204">Atualização realizada com sucesso.</response>
    /// <response code="400">Dados inválidos ou inconsistentes.</response>
    /// <response code="404">Alerta não encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(Guid id, [FromBody] Alerta alerta)
    {
        if (alerta == null || id != alerta.AlertaId)
            return BadRequest("Dados inválidos ou ID inconsistente.");

        var atualizado = _service.Atualizar(id, alerta);
        return atualizado ? NoContent() : NotFound();
    }

    /// <summary>
    /// Remove um alerta.
    /// </summary>
    /// <param name="id">ID do alerta.</param>
    /// <response code="204">Remoção bem-sucedida.</response>
    /// <response code="400">ID inválido.</response>
    /// <response code="404">Alerta não encontrado.</response>
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
