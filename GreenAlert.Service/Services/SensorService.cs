using GreenAlert.Core.Models;
using GreenAlert.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace GreenAlert.Service.Services;

public class SensorService
{
    private readonly ApplicationDbContext _context;

    public SensorService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os sensores com suas estações e alertas.
    /// </summary>
    public IEnumerable<Sensor> ObterTodos()
    {
        return _context.Sensores
            .Include(s => s.Estacao)
            .Include(s => s.Alertas)
            .AsNoTracking()
            .ToList();
    }

    /// <summary>
    /// Retorna um sensor por ID, incluindo estação e alertas.
    /// </summary>
    public Sensor? ObterPorId(Guid id)
    {
        return _context.Sensores
            .Include(s => s.Estacao)
            .Include(s => s.Alertas)
            .AsNoTracking()
            .FirstOrDefault(s => s.SensorId == id);
    }

    /// <summary>
    /// Cria um novo sensor.
    /// </summary>
    public Sensor Criar(Sensor sensor)
    {
        if (sensor == null) throw new ArgumentNullException(nameof(sensor));

        sensor.SensorId = Guid.NewGuid();
        _context.Sensores.Add(sensor);
        _context.SaveChanges();
        return sensor;
    }

    /// <summary>
    /// Atualiza os dados de um sensor existente.
    /// </summary>
    public bool Atualizar(Guid id, Sensor sensorAtualizado)
    {
        if (sensorAtualizado == null || id != sensorAtualizado.SensorId)
            return false;

        var existente = _context.Sensores.Find(id);
        if (existente == null) return false;

        existente.Tipo = sensorAtualizado.Tipo;
        existente.Unidade = sensorAtualizado.Unidade;
        existente.EstacaoId = sensorAtualizado.EstacaoId;

        _context.Sensores.Update(existente);
        _context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Remove um sensor pelo ID.
    /// </summary>
    public bool Remover(Guid id)
    {
        var sensor = _context.Sensores.Find(id);
        if (sensor == null) return false;

        _context.Sensores.Remove(sensor);
        _context.SaveChanges();
        return true;
    }
}
