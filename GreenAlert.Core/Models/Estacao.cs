using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenAlert.Core.Models;

public class Estacao
{
    [Key]
    public Guid EstacaoId { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Localizacao { get; set; } = string.Empty;

    public ICollection<Sensor>? Sensores { get; set; }
}