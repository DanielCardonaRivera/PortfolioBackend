using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using System.Net;
using System.Net.Mail;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API funcionando correctamente");
        }

        [HttpPost]
        public IActionResult SendEmail(ContactRequest request)
        {
            try
            {
                var fromAddress = new MailAddress("tucorreo@gmail.com", "Portafolio Web");
                var toAddress = new MailAddress("tucorreo@gmail.com", "Daniel");
                const string fromPassword = "mi_app_password"; //Contraseña de aplicación de Gmail

                string subject = $"Nuevo mensaje de {request.Name}";
                string body = $"Nombre: {request.Name}\nCorreo: {request.Email}\n\nMensaje:\n{request.Message}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                smtp.Send(message);

                return Ok(new { message = "Correo enviado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el correo.", error = ex.Message });
            }
        }
    }
}
