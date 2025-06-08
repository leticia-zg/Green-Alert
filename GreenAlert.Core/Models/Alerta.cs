using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenAlert.Core.Models;

public class Alerta
{
    [Key]
    public Guid AlertaId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SensorId { get; set; }

    [ForeignKey("SensorId")]
    public Sensor? Sensor { get; set; }

    [Required]
    [StringLength(50)]
    public string Tipo { get; set; } = string.Empty;

    [Required]
    public double Valor { get; set; }

    [Required]
    public DateTime DataHora { get; set; } = DateTime.UtcNow;

    [StringLength(200)]
    public string Descricao { get; set; } = string.Empty;
}