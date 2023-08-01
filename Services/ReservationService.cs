using ReservationProject.Managers;
using ReservationProject.Models;

namespace ReservationProject.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationManager _reservationManager;
    private readonly IMailService _mailService;
    public ReservationService(IReservationManager reservationManager, IMailService mailService)
    {
        _reservationManager = reservationManager;
        _mailService = mailService;
    }

    public void MakeReservation(string name, DateTime date, int guests)
    {
        var tables = _reservationManager.GetAvailableTables(guests);

        if (tables == null || tables.Count <= 0)
        {
            Console.WriteLine("Üzgünüz, uygun masa bulunamadı.");
            return;
        }

        var table = tables[0];
        var reservation = new Models.Reservation
        {
            CustomerName = name,
            ReservationDate = date,
            NumberOfGuests = guests,
            TableNumber = table.Number
        };

        _reservationManager.MakeReservation(reservation);

        Console.WriteLine($"Rezervasyon Kaydedildi: {reservation.CustomerName}, {reservation.ReservationDate}, {reservation.NumberOfGuests}, {reservation.TableNumber}");

        // Rezervasyon onay e-postası gönderme
        var email = $"Sayın {name}, rezervasyonunuz başarıyla alındı. Masa No: {table.Number}, Tarih: {date}, Kişi Sayısı: {guests}";
        _mailService.SendEmail(name, "Rezervasyon Onayı", email);

        Console.WriteLine("Rezervasyon başarıyla yapıldı.");
    }

}