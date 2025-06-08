using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenAlert.Core.Models;

public class Sensor
{
    [Key]
    public Guid SensorId { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public string Tipo { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Unidade { get; set; } = string.Empty;

    [Required]
    public Guid EstacaoId { get; set; }

    [ForeignKey("EstacaoId")]
    public Estacao? Estacao { get; set; }

    public ICollection<Alerta>? Alertas { get; set; }
}