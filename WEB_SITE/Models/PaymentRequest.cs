namespace WEB_SITE.Models
{
    public class PaymentRequest
    {
        public bool? saveCard { get; set; }
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
    public class PaymentRequestVM
    {
        public int? Total { get; set; }
        public bool? saveCard { get; set; }
        public string? NombreTarjeta { get; set; }
        public int idCliente { get; set; }
        public int idVuelo { get; set; }
        public int? IdCompra { get; set; }
        public string? TokenCard { get; set; }

        public string? ExpMonth { get; set; }

        public string? ExpYear { get; set; }

        public int? Cvs { get; set; }

        public DateTime? FechaCompra { get; set; } = DateTime.Now;
        
        public int? Cantidad { get; set; }
    }
}
