namespace ReservationProject.Services;

public class MailService : IMailService
{
    public void SendEmail(string recipient, string subject, string message)
    {
        Console.WriteLine($"E-posta gönderiliyor: \nAlıcı: {recipient}\nKonu: {subject}\nMesaj: {message}");
    }
}