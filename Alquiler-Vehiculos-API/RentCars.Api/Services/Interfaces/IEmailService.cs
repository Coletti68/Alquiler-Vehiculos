namespace RentCars.Api.Services.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string destinatario, string asunto, string cuerpoHtml);
        Task EnviarEmailRecuperacionAsync(string nombreUsuario, string destinatario, string link);
    }
}