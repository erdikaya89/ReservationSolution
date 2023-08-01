namespace ReservationProject.Services;

public interface IReservationService
{
    public void MakeReservation(string name, DateTime date, int guests);

}