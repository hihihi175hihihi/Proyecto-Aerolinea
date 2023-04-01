namespace API.Models
{
    public class PaymentRequest
    {
        public string? NombreTarjeta { get; set; }
        public int IdCliente { get; set; }
        public int? IdTarjeta { get; set; }
        public decimal MontoPago { get; set; }
        public int? IdCompra { get; set; }
        public string? TokenCard { get; set; }

        public int? Last4 { get; set; }

        public string? ExpMonth { get; set; }

        public string? ExpYear { get; set; }
        
        public string? Brand { get; set; }

        public int? Cvs { get; set; }
    }
}
