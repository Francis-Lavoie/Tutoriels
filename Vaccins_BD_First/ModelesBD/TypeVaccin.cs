using System;
using System.Collections.Generic;

#nullable disable

namespace Vaccins_BD_First.ModelesBD
{
    public partial class TypeVaccin
    {
        public TypeVaccin()
        {
            Vaccins = new HashSet<Vaccin>();
        }

        public int TypeVaccinId { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Vaccin> Vaccins { get; set; }
    }
}
