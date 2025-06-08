using GreenAlert.Core.Models;
using GreenAlert.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace GreenAlert.Service.Services;

public class EstacaoService
{
    private readonly ApplicationDbContext _context;

    public EstacaoService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todas as estações com seus sensores.
    /// </summary>
    public IEnumerable<Estacao> ObterTodas()
    {
        return _context.Estacoes
            .Include(e => e.Sensores)
            .AsNoTracking()
            .ToList();
    }

    /// <summary>
    /// Retorna uma estação por ID.
    /// </summary>
    public Estacao? ObterPorId(Guid id)
    {
        return _context.Estacoes
            .Include(e => e.Sensores)
            .AsNoTracking()
            .FirstOrDefault(e => e.EstacaoId == id);
    }

    /// <summary>
    /// Cria uma nova estação.
    /// </summary>
    public Estacao Criar(Estacao estacao)
    {
        if (estacao == null) throw new ArgumentNullException(nameof(estacao));

        estacao.EstacaoId = Guid.NewGuid(); // garante ID novo
        _context.Estacoes.Add(estacao);
        _context.SaveChanges();
        return estacao;
    }

    /// <summary>
    /// Atualiza uma estação existente.
    /// </summary>
    public bool Atualizar(Guid id, Estacao estacaoAtualizada)
    {
        if (estacaoAtualizada == null || id != estacaoAtualizada.EstacaoId)
            return false;

        var existente = _context.Estacoes.Find(id);
        if (existente == null) return false;

        existente.Nome = estacaoAtualizada.Nome;
        existente.Localizacao = estacaoAtualizada.Localizacao;

        _context.Estacoes.Update(existente);
        _context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Remove uma estação pelo ID.
    /// </summary>
    public bool Remover(Guid id)
    {
        var estacao = _context.Estacoes.Find(id);
        if (estacao == null) return false;

        _context.Estacoes.Remove(estacao);
        _context.SaveChanges();
        return true;
    }
}
