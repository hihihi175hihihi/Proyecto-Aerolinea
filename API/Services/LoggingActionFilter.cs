using System;
using API.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Services
{
    public class LoggingActionFilter : IAsyncActionFilter, IAsyncExceptionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Aerolinea_DesarrolloContext _context;
        private readonly TelemetryClient _telemetryClient;

        public LoggingActionFilter(
       ILogger<LoggingActionFilter> logger,
       IHttpContextAccessor httpContextAccessor,
       Aerolinea_DesarrolloContext context,
       TelemetryClient telemetryClient)  // <-- Inject TelemetryClient
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _telemetryClient = telemetryClient;  // <-- Initialize TelemetryClient
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var request = httpContext.Request;

            // Registra la solicitud HTTP
            _logger.LogInformation($"Solicitud HTTP: URL {request.Path}, Método {request.Method}");
            // Log to Application Insights
            _telemetryClient.TrackTrace($"Request: {request.Path}, Method: {request.Method}");

            // Ejecuta la acción del controlador
            var startTime = DateTimeOffset.UtcNow;
            var resultContext = await next();
            var endTime = DateTimeOffset.UtcNow;

            // Registra el tiempo de ejecución
            _logger.LogInformation($"Acción ejecutada en {endTime - startTime}");

            // Registra la respuesta HTTP
            var response = resultContext.HttpContext.Response;
            _logger.LogInformation($"Respuesta HTTP: StatusCode {response.StatusCode}");

            // Convierte el resultado del controlador en una cadena JSON
            var responseJson = "";
            if (resultContext.Result is ObjectResult objectResult)
            {
                responseJson = JsonConvert.SerializeObject(objectResult.Value);
            }


            // Crea el objeto Bitacora
            var bitacora = new Bitacoras
            {
                Action = $"Acción del controlador: {context.ActionDescriptor.DisplayName}",
                Request = $"URL: {request.Path}, Método: {request.Method}",
                Response = $"StatusCode: {response.StatusCode}, Content: {responseJson}",
                Tipo ="Log",
                // Aquí puedes agregar más campos, como la tabla afectada (necesitarás analizar el contexto de la acción)
                Fecha = DateTime.UtcNow
            };

            try
            {
                // Guarda el objeto Bitacora en la tabla Bitacora
                _context.Bitacoras.Add(bitacora);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Si hay algún error al guardar en la tabla Bitacora, registra el error y continúa
                _logger.LogError(ex, "Error al guardar en la tabla Bitacora");
            }
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            // Log the exception details
            _logger.LogError(context.Exception, "An exception occurred in the action filter");
            // Log to Application Insights
            _telemetryClient.TrackException(context.Exception);
            // Don't handle the exception further, let other filters or middleware handle it
            return Task.CompletedTask;
        }
    }
}