// Importamos los espacios de nombres necesarios para el controlador, modelos y envío de correos
using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using System.Net;
using System.Net.Mail;

namespace PortfolioBackend.Controllers
{
    // Indicamos que esta clase es un controlador de API
    [ApiController]
    // Ruta base del controlador: api/contact
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        // Ruta GET: api/contact/ping
        // Método simple para probar si la API está activa y funcionando correctamente
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API funcionando correctamente");
        }

        // Ruta POST: api/contact
        // Método que recibe una solicitud con nombre, correo y mensaje para enviar un correo electrónico
        [HttpPost]
        public IActionResult SendEmail(ContactRequest request)
        {
            try
            {
                // Definimos el correo del remitente (desde dónde se envía el correo)
                var fromAddress = new MailAddress("tucorreo@gmail.com", "Portafolio Web");

                // Definimos el correo del destinatario (a quién llega el mensaje)
                var toAddress = new MailAddress("tucorreo@gmail.com", "Daniel");

                // Contraseña de aplicación de Gmail (debe generarse desde la cuenta de Google)
                const string fromPassword = "mi_app_password"; 

                // Asunto del correo
                string subject = $"Nuevo mensaje de {request.Name}";

                // Cuerpo del correo con los datos que vienen del formulario de contacto
                string body = $"Nombre: {request.Name}\nCorreo: {request.Email}\n\nMensaje:\n{request.Message}";

                // Configuramos el cliente SMTP (para enviar correos usando Gmail)
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",         // Servidor SMTP de Gmail
                    Port = 587,                      // Puerto usado para TLS
                    EnableSsl = true,                // Activamos la encriptación SSL
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword) // Autenticación
                };

                // Creamos el mensaje de correo
                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                // Enviamos el correo
                smtp.Send(message);

                // Si es correcto, devolvemos un mensaje de éxito
                return Ok(new { message = "Correo enviado correctamente." });
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos un mensaje con el código 500 y el detalle del error
                return StatusCode(500, new { message = "Error al enviar el correo.", error = ex.Message });
            }
        }
    }
}
