using GreenAlert.Core.Models;
using GreenAlert.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace GreenAlert.Service.Services;

public class AlertaService
{
    private readonly ApplicationDbContext _context;

    public AlertaService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os alertas com seus sensores associados.
    /// </summary>
    public IEnumerable<Alerta> ObterTodos()
    {
        return _context.Alertas
            .Include(a => a.Sensor)
            .AsNoTracking()
            .ToList();
    }

    /// <summary>
    /// Retorna um alerta específico por ID.
    /// </summary>
    public Alerta? ObterPorId(Guid id)
    {
        return _context.Alertas
            .Include(a => a.Sensor)
            .AsNoTracking()
            .FirstOrDefault(a => a.AlertaId == id);
    }

    /// <summary>
    /// Cria um novo alerta com data/hora atual.
    /// </summary>
    public Alerta Criar(Alerta alerta)
    {
        if (alerta == null) throw new ArgumentNullException(nameof(alerta));

        alerta.AlertaId = Guid.NewGuid();
        alerta.DataHora = DateTime.UtcNow;
        _context.Alertas.Add(alerta);
        _context.SaveChanges();
        return alerta;
    }

    /// <summary>
    /// Atualiza os dados de um alerta existente.
    /// </summary>
    public bool Atualizar(Guid id, Alerta alertaAtualizado)
    {
        if (alertaAtualizado == null || id != alertaAtualizado.AlertaId)
            return false;

        var existente = _context.Alertas.Find(id);
        if (existente == null) return false;

        existente.Tipo = alertaAtualizado.Tipo;
        existente.Valor = alertaAtualizado.Valor;
        existente.Descricao = alertaAtualizado.Descricao;
        existente.SensorId = alertaAtualizado.SensorId;

        _context.Alertas.Update(existente);
        _context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Remove um alerta por ID.
    /// </summary>
    public bool Remover(Guid id)
    {
        var alerta = _context.Alertas.Find(id);
        if (alerta == null) return false;

        _context.Alertas.Remove(alerta);
        _context.SaveChanges();
        return true;
    }
}
