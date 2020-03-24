using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public class Person
    {
        private string _name;
        private string _surname;
        private Pesel _pesel;
        private DateTime _birthday;

        public Person(string name, string surname, string pesel, DateTime birthday) // sprawdzić datę urodzenia
        {
            if (Utilities.IsNull(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (Utilities.IsNull(surname))
            {
                throw new ArgumentNullException(nameof(surname));
            }

            if ((DateTime.UtcNow - birthday).Days < 0 || GetDifferenceInYears(birthday, DateTime.UtcNow) > 116)
            {
                throw new ArgumentException(nameof(birthday));
            }

            _name = name;
            _surname = surname;
            _pesel = new Pesel(pesel);
            _birthday = birthday;
        }

        public void Vote(Party party, ElectoralCommission commission)
        {
            if (Utilities.IsNull(party))
            {
                throw new ArgumentNullException(nameof(party));
            }
            if (Utilities.IsNull(commission))
            {
                throw new ArgumentNullException(nameof(commission));
            }

            commission.PlaceVote(this, new Vote(party));
        }

        public bool IsAdult()
        {
            if (GetDifferenceInYears(_birthday, DateTime.UtcNow) < 18)
            {
                return false;
            }
            return true;
        }

        public string GetPesel() => _pesel.Value;

        public string GetFullName()
        {
            return $"{_name} {_surname}";
        }

        private int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            //Excel documentation says "COMPLETE calendar years in between dates"
            int years = endDate.Year - startDate.Year;

            if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
                endDate.Day < startDate.Day// AND the end day is less than the start day
                || endDate.Month < startDate.Month)// OR if the end month is less than the start month
            {
                years--;
            }

            return years;
        }
    }
}
