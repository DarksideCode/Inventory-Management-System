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

        [TestMethod]
        public void getProzessorFromDatabase()
        {
            ProzessorProcess process = new ProzessorProcess();
            Prozessor prozessor = new Prozessor();

            prozessor.setBeschreibung("Dies ist ein Test");
            prozessor.setModell("i5");
            prozessor.setKerne(4);
            prozessor.setBefehlssatz("RISC");
            prozessor.setArchitektur(64);
            prozessor.setTaktrate(3.40);

            process.save(prozessor);
            Prozessor dbProzessor = process.getById(1);

            Assert.AreEqual(prozessor.getBefehlssatz(), dbProzessor.getBefehlssatz());
        }

        [TestMethod]
        public void getFestplatteFromDatabase()  
        {
            FestplatteProcess process = new FestplatteProcess();
            Festplatte festplatte = new Festplatte();

            festplatte.setBeschreibung("Dies ist ein Test");
            festplatte.setKapazitaet(1000);
            festplatte.setSSD(true);
            festplatte.setZoll(3.5);

            process.save(festplatte);
            Festplatte dbFestplatte = process.getById(1);

            Assert.AreEqual(festplatte.getKapazitaet(), dbFestplatte.getKapazitaet());
        }

        [TestMethod]
        public void getMonitorFromDatabase()
        {
            MonitorProcess process = new MonitorProcess();
            Monitor monitor = new Monitor();

            monitor.setBeschreibung("Dies ist ein Test");
            //TODO: Schlauere Idee für Auflöung
            monitor.setAufloesung("1920 x 1080");
            monitor.setZoll(24);
            monitor.setSeitenverhaeltnis("4:3");

            process.save(monitor);
            Monitor dbMonitor = process.getById(1);

            Assert.AreEqual(monitor.getZoll(), dbMonitor.getZoll());
        }

        [TestMethod]
        public void getSchnittstelleFromDatabase()
        {
            SchnittstelleProcess process = new SchnittstelleProcess;
            Schnittstelle schnittstelle = new Schnittstelle();

            schnittstelle.setName("DVI");
            schnittstelle.setBeschreibung("Digital Visual Interface - Eine elektronische Schnittstelle zur Übertragung von Videodaten.");
            schnittstelle.setSeriell(true);
            schnittstelle.setUebertragungsrate(10000); // TB/s natürlich

            process.save(schnittstelle);
            Schnittstelle dbSchnittstelle = process.getById(1);

            Assert.AreEqual(schnittstelle.getBeschreibung(), dbSchnittstelle.getBeschreibung());
        }
    }
}
