namespace ReservationProject.Services;

public interface IMailService
{
    public void SendEmail(string recipient, string subject, string message);

}