namespace GreenAlert.MVC.Models
{
    public class SensorViewModel
    {
        public Guid SensorId { get; set; }
        public string Tipo { get; set; }
        public string Unidade { get; set; }

        public Guid EstacaoId { get; set; }
        public string? NomeEstacao { get; set; } 
    }
}
