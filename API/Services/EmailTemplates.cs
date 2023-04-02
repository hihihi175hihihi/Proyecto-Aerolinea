using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public static class EmailTemplates
    {
        public const string WelcomeEmail = @"
        <html>
            <head>
                <title>Bienvenido</title>
            </head>
            <body>
                <h1>Bienvenido a nuestra plataforma</h1>
                <p>Estamos encantados de tenerte con nosotros. ¡Comienza a explorar nuestras funciones ahora!</p>
            </body>
        </html>";

        public const string PasswordResetEmail = @"
        <html>
            <head>
                <title>Restablecimiento de contraseña</title>
            </head>
            <body>
                <h1>Restablece tu contraseña</h1>
                <p>Para restablecer tu contraseña, haz clic en el siguiente enlace:</p>
                <a href=""{0}"">Restablecer contraseña</a>
            </body>
        </html>";
        public const string ItineraryEmail = @"
        <html>
            <head>
                <title>Itinerario de vuelo</title>
            </head>
            <body>
                <h1>Itinerario de vuelo</h1>
                <table border=""1"">
                    <tr>
                        <th>Día de la semana</th>
                        <th>Hora de salida</th>
                        <th>Hora de llegada</th>
                        <th>Ciudad de origen</th>
                        <th>Ciudad de destino</th>
                        <th>País de origen</th>
                        <th>País de destino</th>
                        <th>Escalas</th>
                    </tr>
                    {0}
                </table>
            </body>
        </html>";
    }
}
