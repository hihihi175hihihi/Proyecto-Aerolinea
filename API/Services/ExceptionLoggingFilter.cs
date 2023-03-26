using System.Data;
using System.Text.RegularExpressions;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Services
{
    public class ExceptionLoggingFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionLoggingFilter> _logger;
        private readonly Aerolinea_DesarrolloContext _context;

        public ExceptionLoggingFilter(ILogger<ExceptionLoggingFilter> logger, Aerolinea_DesarrolloContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void OnException(ExceptionContext context)
        {
            // Log exception
            _logger.LogError(context.Exception, context.Exception.Message);

            try
            {


                // Save exception to Bitacora table
                var bitacora = new Bitacoras()
                {
                    Action = context.ActionDescriptor.DisplayName,
                    Error = context.Exception.Message,
                    Request = String.Empty,
                    Response = String.Empty,
                    Tipo = "Exception", // Save the type as "Exception"
                    Fecha = DateTime.UtcNow
                };

                _context.Bitacoras.Add(bitacora);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex, ex.Message);
            }

            // Customize response
            context.Result = new ObjectResult(new
            {
                message = "Ha ocurrido un error en el servidor.",
                error = context.Exception.Message
            })
            {
                StatusCode = 500 // Internal Server Error
            };

            context.ExceptionHandled = true;
        }
    }
}

