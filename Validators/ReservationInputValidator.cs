using System.Text.RegularExpressions;

namespace ReservationProject.Validators
{
    public static class ReservationInputValidator
    {
        public static (bool, string) ValidateName(string input)
        {
            if (input.Length >= 3 && IsName(input))
            {
                return (true, input);
            }
            return (false, null);
        }

        public static (bool, DateTime) ValidateDate(string input)
        {
            if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                if (date >= DateTime.Now.Date)
                {
                    return (true, date);
                }
            }
            return (false, default);
        }

        public static (bool, int) ValidateGuests(string input)
        {
            if (int.TryParse(input, out int guests) && guests > 0)
            {
                return (true, guests);
            }
            return (false, default);
        }

        public static bool IsName(string value) { Regex regex = new Regex(@"^[a-zA-ZığüşöçĞÜŞİÖÇ]+$"); return regex.IsMatch(value); }
    }

}
