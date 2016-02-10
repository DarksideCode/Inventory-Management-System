using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementSystem;

namespace UnitTests
{
    [TestClass]
    public class ProzessTest
    {
        [TestMethod]
        public void getHerstellerFromDatabase()
        {
            HerstellerProcess process = new HerstellerProcess();
            Hersteller hersteller = new Hersteller();

            hersteller.setFirma("Siemens");
            hersteller.setTelefon(123456);
            hersteller.setEmail("info@siemens.com");
            hersteller.setWebsite("www.siemens.de");
            hersteller.setPLZ(21335);
            hersteller.setOrt("Lüneburg");
            hersteller.setStrasse("Lüneburger Straße");
            hersteller.setHausnummer(1);

            process.save(hersteller);
            Hersteller dbHersteller = process.getById(1);
 
            Assert.AreEqual(hersteller.getWebsite(), dbHersteller.getWebsite());
        }

        [TestMethod]
        public void getGrafikkarteFromDatabase()
        {
            GrafikkarteProcess process = new GrafikkarteProcess();
            Grafikkarte grafikkarte = new Grafikkarte();

            grafikkarte.setBeschreibung("Dies ist ein Test");
            grafikkarte.setTaktrate(5);
            grafikkarte.setModell("GTX1234");
            grafikkarte.setSpeicher(2000);

            process.save(grafikkarte);
            Grafikkarte dbGrafikkarte = process.getById(1);

            Assert.AreEqual(grafikkarte.getBeschreibung(), dbGrafikkarte.getBeschreibung());
        }

        [TestMethod]
        public void getArbeitsspeicherFromDatabase()
        {
            ArbeitsspeicherProcess process = new ArbeitsspeicherProcess();
            Arbeitsspeicher arbeitsspeicher = new Arbeitsspeicher();

            arbeitsspeicher.setBeschreibung("Dies ist ein Test");
            arbeitsspeicher.setSpeicher(8000);
            arbeitsspeicher.setTaktrate(12000);

            process.save(arbeitsspeicher);
            Arbeitsspeicher dbArbeitsspeicher = process.getById(1);

            Assert.AreEqual(arbeitsspeicher.getSpeicher(), dbArbeitsspeicher.getSpeicher());
        }

        [TestMethod]
        public void getHauptplatineFromDatabase()
        {
            HauptplatineProcess process = new HauptplatineProcess();
            Hauptplatine hauptplatine = new Hauptplatine();

            hauptplatine.setBeschreibung("Dies ist ein Test");
            hauptplatine.setZoll(24.2);
            hauptplatine.setSockel("PGA 988B");

            process.save(hauptplatine);
            Hauptplatine dbHauptplatine = process.getById(1);

            Assert.AreEqual(hauptplatine.getSockel(), dbHauptplatine.getSockel());
        }
    }
}
