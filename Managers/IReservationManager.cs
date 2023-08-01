using ReservationProject.Models;

namespace ReservationProject.Managers
{
    public interface IReservationManager
    {
        public List<Table> GetTables();
        public List<Table> GetTables(int count);
        public List<Table> GetAvailableTables(int count);
        public void MakeReservation(Reservation reservation);
        public void CancelReservation(Reservation reservation);

    }
}