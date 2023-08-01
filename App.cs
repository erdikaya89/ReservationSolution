using Microsoft.Extensions.Configuration;
using ReservationProject.Services;
using ReservationProject.Validators;
using System;

namespace ReservationProject.Models
{
    public class App
    {
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly IReservationService _reservationService;

        public App(IMailService mailService,
            IReservationService reservationService,
            IConfiguration configuration)
        {
            _mailService = mailService;
            _configuration = configuration;
            _reservationService = reservationService;
        }

        public void Run(string[] args)
        {
            do
            {
                string name;
                DateTime reservationDate;
                int guests;
                try
                {
                    GetReservationDetails(out name, out reservationDate, out guests);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rezervasyon bilgileri alınırken bir hata oluştu: " + ex.Message);
                    continue;
                }

                try
                {
                    _reservationService.MakeReservation(name, reservationDate, guests);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rezervasyon oluşturulurken bir hata oluştu: " + ex.Message);
                }

                Console.Write("Başka rezervasyon yapmak istiyor musunuz? (Hayır için 'H' veya 'hayır' girin): ");
                string? answer = Console.ReadLine()?.Trim().ToLower();
                if (answer == null)
                {
                    Console.WriteLine("Giriş alınamadı. Lütfen tekrar deneyin.");
                    continue;
                }
                if (answer == "hayır" || answer == "h")
                {
                    break; // Döngüden çık ve uygulamayı tamamla
                }

            } while (true);
        }

        private static void GetReservationDetails(out string name, out DateTime date, out int guests)
        {
            Intro();
            name = GetInput("Müşteri Adı: ", ReservationInputValidator.ValidateName, "Geçersiz müşteri adı. En az 3 harften oluşmalıdır. Tekrar deneyin.");
            date = GetInput("Tarih (dd/MM/yyyy): ", ReservationInputValidator.ValidateDate, "Geçersiz tarih. Tarih bugünden önce olmamalıdır. Tekrar deneyin.");
            guests = GetInput("Misafir Sayısı: ", ReservationInputValidator.ValidateGuests, "Geçersiz misafir sayısı. Tekrar deneyin.");
        }

        private static void Intro()
        {
            Console.WriteLine("********* Rezervasyon Sistemine Hoşgeldiniz **********");
            Console.WriteLine("\nLütfen Rezervasyona ait bilgileri giriniz\n");
        }

        private static T GetInput<T>(string message, Func<string, (bool, T)> validator, string errorMessage)
        {
            while (true)
            {
                Console.Write(message); 
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Giriş alınamadı. Lütfen tekrar deneyin.");
                    continue;
                }
                var (isValid, value) = validator(input);
                if (isValid)
                {
                    return value;
                }
                Console.WriteLine(errorMessage);
            }
        }
    }
}
