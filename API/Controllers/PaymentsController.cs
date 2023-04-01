using API.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;

        public PaymentsController(Aerolinea_DesarrolloContext context)
        {
            _context = context;
        }

        [Route("pay")]
        [HttpPost]
        public async Task<ActionResult>Pay(PaymentRequest payment)
        {
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
                            Cvc = tarjeta.Cvs.ToString()
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

                    // Almacenar la información del pago en la tabla Pagos
                    var pago = new Pagos
                    {
                        idCompra = payment.IdCompra,
                        FechaPago = DateTime.Now,
                        MontoPago = payment.MontoPago
                    };

                    _context.Pagos.Add(pago);
                    await _context.SaveChangesAsync();
                    return Ok(pago);
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
                    Cvs=payment.Cvs,
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

                // Almacenar la información del pago en la tabla Pagos
                var pago = new Pagos
                {
                    idCompra = payment.IdCompra,
                    FechaPago = DateTime.Now,
                    MontoPago = payment.MontoPago
                };

                _context.Pagos.Add(pago);
                await _context.SaveChangesAsync();
                return Ok(pago);
            }
        }
    }
}
