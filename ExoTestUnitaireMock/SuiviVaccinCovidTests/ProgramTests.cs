using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuiviVaccinCovid;
using SuiviVaccinCovid.Modele;
using System;
using System.Collections.Generic;
using System.Text;
using SuiviVaccinCovid;
using System.Linq;
using Moq;

namespace SuiviVaccinCovid.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void LePlusRecentTest()
        {

            Program p = new Program();
            List<Vaccin> vaccins = new List<Vaccin> {
        new Vaccin
        {
            NAMPatient = "AAAA10101010",
            Type = "Pfizer",
            Date = new DateTime(2021,11,21)
        },
        new Vaccin
        {
            NAMPatient = "BBBB10101010",
            Type = "Pfizer",
            Date = new DateTime(2021,11,30)
        },
        new Vaccin {
            NAMPatient = "CCCC10101010",
            Type = "Moderna",
            Date = new DateTime(2021,11,2)
        }
        };
            Vaccin reponse = new Vaccin
            {
                NAMPatient = "BBBB10101010",
                Type = "Pfizer",
                Date = new DateTime(2021, 11, 30)
            };
            Assert.AreEqual(reponse, p.LePlusRecent(vaccins));
        }

        [TestMethod()]
        public void LePlusRecentVideTest()
        {
            Program p = new Program();
            List<Vaccin> vaccins = new List<Vaccin>();
            Assert.ThrowsException<InvalidOperationException>(() => p.LePlusRecent(vaccins));
        }

        [TestMethod()]
        public Vaccin LePlusRecent(IEnumerable<Vaccin> vaccins)
        {
            if (vaccins.Count() != 0)
                return vaccins.OrderBy(v => v.Date).Last();
            return null;
        }

        [TestMethod()]
        public void CreerNouveauVaccinTest()
        {
            DateTime d = new DateTime(2021, 03, 27, 2, 34, 55, 392);
            Mock<IFournisseurDeDate> mockFournisseurDate = new Mock<IFournisseurDeDate>();
            mockFournisseurDate.Setup(m => m.Now).Returns(d);
            Program p = new Program
            {
                FournisseurDeDate = mockFournisseurDate.Object
            };
            Vaccin v = new Vaccin
            {
                NAMPatient = "AAAA99999999",
                Type = "ABC",
                Date = d
            };
            Vaccin cree = p.CreerNouveauVaccin("AAAA99999999", "ABC");
            Assert.AreEqual(v, cree);
        }

        [TestMethod()]
        public void EnregistrerVaccinTest()
        {
            List<Vaccin> vaccins = new List<Vaccin> {
            new Vaccin 
            { 
                NAMPatient = "AAAA10101010",
                Type = "Pfizer", 
                Date = new DateTime(2021,11,21)
            },
            new Vaccin 
            { 
                NAMPatient = "BBBB10101010", 
                Type = "Pfizer", 
                Date = new DateTime(2021,11,30)
            },
            new Vaccin 
            { 
                NAMPatient = "CCCC10101010",
                Type = "Moderna", 
                Date = new DateTime(2021,11,2)},
            };

            Mock<IDaoVaccin> mockContexte = new Mock<IDaoVaccin>();
            mockContexte.Setup(m => m.AjouterVaccin(It.IsAny<Vaccin>()));
            mockContexte.Setup(m => m.ObtenirVaccins()).Returns(vaccins);
            mockContexte.Setup(m => m.Sauvegarder());
            
            Program p = new Program
            {
                DaoVaccin = mockContexte.Object
            };
            Vaccin v = new Vaccin
            {
                NAMPatient = "DDDD10101010",
                Type = "Pfizer",
                Date = new DateTime(2021, 03, 27)
            };
            
            p.EnregistrerVaccin(v); 
            mockContexte.Verify(m => m.AjouterVaccin(v), Times.Once); 
            mockContexte.Verify(m => m.AjouterVaccin(It.IsAny<Vaccin>()), Times.Once);
            mockContexte.Verify(m => m.Sauvegarder());
        }
    }
}