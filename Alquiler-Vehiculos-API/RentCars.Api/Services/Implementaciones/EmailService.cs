using RentCars.Api.Services.Interfaces;
using RentCars.Api.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace RentCars.Api.Services
{
    public class EmailService : IEmailService
    {
        public async Task EnviarEmailRecuperacionAsync(string nombreUsuario, string destinatario, string link)
        {
            var asunto = "Restablecer tu contraseña";
            var cuerpoHtml = GenerarCuerpoHtml(nombreUsuario, link);

            await EnviarEmailAsync(destinatario, asunto, cuerpoHtml);
        }

        public async Task EnviarEmailAsync(string destinatario, string asunto, string cuerpoHtml)
        {
            try
            {
                var smtp = new SmtpClient("sandbox.smtp.mailtrap.io")
                {
                    Port = 2525,
                    Credentials = new NetworkCredential("7161877c48fa2f", "05b44ebe135c09"),
                    EnableSsl = true
                };

                var mensaje = new MailMessage("ResetPassword@rentcars.com", destinatario, asunto, cuerpoHtml)
                {
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(mensaje);
                Console.WriteLine($"📧 Email enviado a {destinatario} con asunto '{asunto}'");
            }
            catch (SmtpException smtpEx)
            {
                throw new ApplicationException("Error al enviar el correo SMTP", smtpEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al enviar el correo", ex);
            }
        }

        private string GenerarCuerpoHtml(string nombreUsuario, string link)
        {
            return $@"
            <html>
            <body style='font-family: Arial, sans-serif; color: #333;'>
                <h2>Solicitud de restablecimiento de contraseña</h2>
                <p>Hola <strong>{nombreUsuario}</strong>,</p>
                <p>Recibimos una solicitud para restablecer tu contraseña. Si no fuiste vos, podés ignorar este mensaje.</p>
                <p>Para continuar, hacé clic en el siguiente botón:</p>
                <p style='margin: 20px 0;'>
                    <a href='{link}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Restablecer contraseña</a>
                </p>
                <p>Este enlace expirará en 1 hora.</p>
                <p>Gracias,<br/>El equipo de RentCars</p>
            </body>
            </html>";
        }
    }
}