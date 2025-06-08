namespace GreenAlert.MVC.Models
{
    public class AlertaViewModel
    {
        public Guid AlertaId { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }

        public Guid SensorId { get; set; }
        public string? TipoSensor { get; set; } 
    }
}
