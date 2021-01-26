using System;
using System.Collections.Generic;
using System.Text;

namespace Vaccins
{
    public class Vaccin
    {
        public int VaccinId { get; set; }
        public DateTime Date { get; set; }
        public string Nom { get; set; }
        public string NAMPatient { get; set; }

        public override string ToString()
        {
            return $" Vaccin #{VaccinId} ({Nom}), adiminstré le {Date} à {NAMPatient}";
        }
    }
}
