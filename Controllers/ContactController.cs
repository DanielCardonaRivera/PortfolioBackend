using Microsoft.AspNetCore.Mvc;

namespace PortfolioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public IActionResult Send([FromBody] ContactForm form)
        {
            // Aquí podrías enviar un correo o guardar en una base de datos
            Console.WriteLine($"Mensaje de: {form.Name} - {form.Email} - {form.Message}");
            return Ok(new { message = "Mensaje recibido correctamente." });
        }
    }

    public class ContactForm
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
