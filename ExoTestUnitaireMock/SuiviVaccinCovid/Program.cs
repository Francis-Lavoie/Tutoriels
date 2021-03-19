using SuiviVaccinCovid.Modele;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuiviVaccinCovid
{
    public interface IFournisseurDeDate
    {
        public DateTime Now { get; }
    }

    public class FournisseurDeMaintenant : IFournisseurDeDate
    {
        public DateTime Now { get { return DateTime.Now; } }
    }

    public interface IDaoVaccin
    {
        public IEnumerable<Vaccin> ObtenirVaccins();
        public void AjouterVaccin(Vaccin v);
        public void Sauvegarder();
    }

    public class DBVaccinContext : IDaoVaccin
    {
        private VaccinContext contexte = new VaccinContext();
        public IEnumerable<Vaccin> ObtenirVaccins()
        {
            return contexte.Vaccins;
        }
        public void AjouterVaccin(Vaccin v)
        {
            contexte.Vaccins.Add(v);
        }
        public void Sauvegarder()
        {
            contexte.SaveChanges();
        }
    }

    public class Program
    {
        public IFournisseurDeDate FournisseurDeDate { get; set; }
        public IDaoVaccin DaoVaccin { get; set; }

        static void Main(string[] args)
        {
            Program p = new Program
            {
                DaoVaccin = new DBVaccinContext(),
                FournisseurDeDate = new FournisseurDeMaintenant()
            };
            p.Peupler();
            p.EnregistrerVaccin(p.CreerNouveauVaccin("SIOA95032911", "Pfizer"));
            Console.WriteLine(p.LePlusRecent());
        }

        public void Peupler()
        {
            if (DaoVaccin.ObtenirVaccins().Count() == 0)
            {
                Vaccin dose1Mylene = new Vaccin
                {
                    Date = new DateTime(2021, 01, 24),
                    NAMPatient = "LAPM12345678",
                    Type = "Moderna"
                };

                Vaccin dose1Gaston = new Vaccin
                {
                    Date = new DateTime(2021, 01, 15),
                    NAMPatient = "BHEG12345678",
                    Type = "Pfizer"
                };

                DaoVaccin.AjouterVaccin(dose1Mylene);
                DaoVaccin.AjouterVaccin(dose1Gaston);

                DaoVaccin.Sauvegarder();
            }
        }

        public Vaccin CreerNouveauVaccin(string nam, string type)
        {
            return new Vaccin
            {
                NAMPatient = nam,
                Type = type,
                Date = DateTime.Now
            };
        }

        public void EnregistrerVaccin(Vaccin n)
        {
            var memePatient = DaoVaccin.ObtenirVaccins().Where(v => v.NAMPatient == n.NAMPatient);
            Vaccin vaccin = CreerNouveauVaccin("", "");

            if (memePatient.Count() > 1)
                throw new ArgumentException("Patient déjà vacciné deux fois");
            if (memePatient.Count() == 1 && memePatient.First().Type != vaccin.Type)
                throw new ArgumentException("Un patient ne peut pas recevoir deux" +
                "types de vaccins");

            DaoVaccin.AjouterVaccin(vaccin);
            DaoVaccin.Sauvegarder();
        }

        public Vaccin LePlusRecent(IEnumerable<Vaccin> vaccins)
        {
            return vaccins.OrderBy(v => v.Date).Last();
        }

        public Vaccin LePlusRecent()
        {
            return LePlusRecent(DaoVaccin.ObtenirVaccins());
        }
    }
}
