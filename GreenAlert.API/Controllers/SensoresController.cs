using Microsoft.AspNetCore.Mvc;
using GreenAlert.Core.Models;
using GreenAlert.Service.Services;
using System;

namespace GreenAlert.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensoresController : ControllerBase
{
    private readonly SensorService _service;

    public SensoresController(SensorService service) => _service = service;

    /// <summary>
    /// Retorna todos os sensores cadastrados.
    /// </summary>
    /// <remarks>Lista sensores com informa��es da esta��o e alertas relacionados.</remarks>
    /// <response code="200">Sensores retornados com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(_service.ObterTodos());

    /// <summary>
    /// Retorna um sensor espec�fico por ID.
    /// </summary>
    /// <param name="id">ID do sensor.</param>
    /// <response code="200">Sensor encontrado.</response>
    /// <response code="404">Sensor n�o encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var sensor = _service.ObterPorId(id);
        return sensor is null ? NotFound() : Ok(sensor);
    }

    /// <summary>
    /// Cadastra um novo sensor.
    /// </summary>
    /// <param name="sensor">Dados do sensor.</param>
    /// <response code="201">Sensor criado com sucesso.</response>
    /// <response code="400">Dados inv�lidos.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Sensor sensor)
    {
        if (sensor == null || string.IsNullOrEmpty(sensor.Tipo))
            return BadRequest("Dados inv�lidos para cria��o.");

        var novo = _service.Criar(sensor);
        return CreatedAtAction(nameof(GetById), new { id = novo.SensorId }, novo);
    }

    /// <summary>
    /// Atualiza um sensor existente.
    /// </summary>
    /// <param name="id">ID do sensor.</param>
    /// <param name="sensor">Dados atualizados do sensor.</param>
    /// <response code="204">Atualiza��o realizada com sucesso.</response>
    /// <response code="400">Dados inv�lidos ou inconsistentes.</response>
    /// <response code="404">Sensor n�o encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(Guid id, [FromBody] Sensor sensor)
    {
        if (sensor == null || id != sensor.SensorId)
            return BadRequest("Dados inv�lidos ou ID inconsistente.");

        var atualizado = _service.Atualizar(id, sensor);
        return atualizado ? NoContent() : NotFound();
    }

    /// <summary>
    /// Remove um sensor.
    /// </summary>
    /// <param name="id">ID do sensor.</param>
    /// <response code="204">Remo��o bem-sucedida.</response>
    /// <response code="400">ID inv�lido.</response>
    /// <response code="404">Sensor n�o encontrado.</response>
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
