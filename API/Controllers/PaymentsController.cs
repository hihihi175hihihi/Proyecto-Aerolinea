using System.Text;
using API.Models;
using API.Models.ViewModelSP;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;
        private readonly EmailService _emailService;

        public PaymentsController(Aerolinea_DesarrolloContext context,EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [Route("pay")]
        [HttpPost]
        public async Task<ActionResult>Pay(PaymentRequest payment)
        {
            
            Pagos pago;
            //si se mando una tarjeta seleccionada
            if (payment.IdTarjeta != null)
            {
                var tarjeta = _context.Tarjetas.Where(x => x.idTarjeta == payment.IdTarjeta).FirstOrDefault();
                if (tarjeta != null)
                {
                    // Realizar el pago con Stripe

                    // Crear el token de la tarjeta con Stripe
                    var tokenOptions = new TokenCreateOptions
                    {
                        Card = new TokenCardOptions
                        {
                            Name = payment.NombreTarjeta,
                            Number = tarjeta.TokenCard,
                            ExpMonth = tarjeta.ExpMonth,
                            ExpYear = tarjeta.ExpYear,
                            Cvc = tarjeta.Csv.ToString()
                        }
                    };

                    var tokenService = new TokenService();
                    Token stripeToken;
                    try
                    {
                        stripeToken = await tokenService.CreateAsync(tokenOptions);
                    }
                    catch (StripeException ex)
                    {
                        return BadRequest(ex.Message);
                    }

                    // Realizar el pago con Stripe
                    var chargeService = new ChargeService();
                    var chargeOptions = new ChargeCreateOptions
                    {
                        Amount = Convert.ToInt32(payment.MontoPago * 100), // Monto en centavos
                        Currency = "usd", // Asume que la moneda es dólares estadounidenses
                        Description = $"Pago para el cliente {payment.IdCliente}",
                        Source = stripeToken.Id
                    };

                    var charge = chargeService.Create(chargeOptions);
                    if (charge.StripeResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return BadRequest("No se pudo realizar el pago.");
                    }
                    // Almacenar la información del pago en la tabla Pagos
                    pago = new Pagos
                    {
                        idCompra = payment.IdCompra,
                        FechaPago = DateTime.Now,
                        MontoPago = payment.MontoPago
                    };

                    _context.Pagos.Add(pago);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound("Tarjeta no encontrada.");
                }
            }
            else
            {
                //Si no se mando una tarjeta asociada
                // Crear el token de la tarjeta con Stripe
                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Name=payment.NombreTarjeta,
                        Number = payment.TokenCard,
                        ExpMonth = payment.ExpMonth,
                        ExpYear = payment.ExpYear,
                        Cvc = payment.Cvs.ToString()
                    }
                };

                var tokenService = new TokenService();
                Token stripeToken;
                try
                {
                    stripeToken = await tokenService.CreateAsync(tokenOptions);
                }
                catch (StripeException ex)
                {
                    return BadRequest(ex.Message);
                }

                var tarjeta = new Tarjetas()
                {
                    idCliente=payment.IdCliente,
                    TokenCard= payment.TokenCard,
                    Last4 = int.Parse(stripeToken.Card.Last4),
                    ExpMonth =payment.ExpMonth,
                    ExpYear=payment.ExpYear,
                    Csv=payment.Cvs,
                    Brand = stripeToken.Card.Brand
                };
                _context.Tarjetas.Add(tarjeta);
                await _context.SaveChangesAsync();

                // Realizar el pago con Stripe
                var chargeService = new ChargeService();
                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(payment.MontoPago * 100), // Monto en centavos
                    Currency = "usd", // Asume que la moneda es dólares estadounidenses
                    Description = $"Pago para el cliente {payment.IdCliente}",
                    Source = stripeToken.Id
                };

                var charge = chargeService.Create(chargeOptions);
                if (charge.StripeResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return BadRequest("No se pudo realizar el pago.");
                }
                // Almacenar la información del pago en la tabla Pagos
                pago = new Pagos
                {
                    idCompra = payment.IdCompra,
                    FechaPago = DateTime.Now,
                    MontoPago = payment.MontoPago
                };

                _context.Pagos.Add(pago);
                await _context.SaveChangesAsync();
            }

            // Generar el itinerario
            var itinerario = await generateItinerary(payment.IdCompra);
            var itineraryTableRows = new StringBuilder();

            foreach (var itinerary in itinerario)
            {
                string escalasTableRows = (itinerary.Escalas == null || !itinerary.Escalas.Any())
                                            ? "<tr><td colspan=\"3\">Sin Escalas</td></tr>"
                                            : string.Join("", itinerary.Escalas.Select(escala => $@"
                                            <tr>
                                                <td>{escala.CIUDAD_ESCALA}</td>
                                                <td>{escala.DuracionEscala}</td>
                                                <td>{escala.DuracionLlegada}</td>
                                            </tr>"));
                string dayName = GetDayNameFromNumber(itinerary.DiaSemana);
                itineraryTableRows.Append($@"
                <tr>
                    <td>{dayName}</td>
                    <td>{itinerary.HoraSalida}</td>
                    <td>{itinerary.HoraLlegada}</td>
                    <td>{itinerary.CIUDAD_ORIGEN}</td>
                    <td>{itinerary.CIUDAD_DESTINO}</td>
                    <td>{itinerary.PAIS_ORIGEN}</td>
                    <td>{itinerary.PAIS_DESTINO}</td>
                    <td>
                        <table border=""1"">
                            <tr>
                                <th>Ciudad de escala</th>
                                <th>Duración de escala</th>
                                <th>Duración de llegada</th>
                            </tr>
                            {escalasTableRows}
                        </table>
                    </td>
                </tr>");
            }

            string body = EmailTemplates.ItineraryEmail.Replace("{0}", itineraryTableRows.ToString());
            
            await _emailService.SendEmailAsync("Itinerario de Vuelo",body);
            return Ok(pago);
        }
        public static string GetDayNameFromNumber(string dayNumber)
        {
            return dayNumber switch
            {
                "1" => "Lunes",
                "2" => "Martes",
                "3" => "Miércoles",
                "4" => "Jueves",
                "5" => "Viernes",
                "6" => "Sábado",
                "7" => "Domingo"
            };
        }
        private async Task<List<GenerateItinerary>> generateItinerary(int? idCompra)
        {
            var parameters = SqlParameterWrapper.Create(("@Compra", idCompra));
            var itinerario = await _context.RunSpAsync<GenerateItinerary>("GenerateItinerary", parameters);
            itinerario.DeserializeEscalasJson();
            return itinerario;

        }
    }
}
