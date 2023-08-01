using ReservationProject.Models;
using ReservationProject.Services;

namespace ReservationProject.Managers
{
    public class ReservationManager : IReservationManager
    {
        private readonly IMailService _mailService;
        private List<Table> _tables;
        private List<Reservation> _reservations;

        public ReservationManager(IMailService mailService)
        {
            _mailService = mailService;
            OpenRestaurant();
        }
        private void OpenRestaurant()
        {
            PrepareTables();
            _reservations = new List<Reservation>();
        }

        private void PrepareTables()
        {
            var rnd = new Random();
            _tables = new List<Table>();
            var tableCount = rnd.Next(10, 20);

            for (int i = 1; i <= tableCount; i++)
            {
                var table = new Table();
                table.Number = i;
                table.Capacity = rnd.Next(2, 8);
                _tables.Add(table);
            }
        }
        public List<Table> GetTables() => _tables;
        public List<Table> GetTables(int count) => _tables.FindAll(t => t.Capacity >= count);
        public List<Table> GetAvailableTables(int count) => _tables.FindAll(t => t.Capacity >= count && !_reservations.Any(r => r.TableNumber == t.Number));

        public void MakeReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
        }
        public void CancelReservation(Reservation reservation)
        {
            _reservations.Remove(reservation);
        }

    }
}