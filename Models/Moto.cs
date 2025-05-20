namespace MottuApi.Models
{
    public class Moto
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Status { get; set; } = "Disponível"; // Ex: Disponível, Em Uso, Em Manutenção
        public string Patio { get; set; } = string.Empty;
        public DateTime DataEntrada { get; set; } = DateTime.Now;
        public DateTime? DataSaida { get; set; }
    }
}
