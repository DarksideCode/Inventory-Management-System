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
        public void getProducerFromDatabase()
        {
            ProducerProcess process = new ProducerProcess();
            Producer producer = new Producer();

            producer.setCompanyName("Siemens");
            producer.setPhoneNumber(123456);
            producer.setEmail("info@siemens.com");
            producer.setWebsite("www.siemens.de");
            producer.setPostalCode(21335);
            producer.setPlace("Lüneburg");
            producer.setStreet("Lüneburger Straße");
            producer.setHouseNumber(1);

            process.save(producer);
            Producer dbProducer = process.getById(1);

            Assert.AreEqual(producer.getWebsite(), dbProducer.getWebsite());
        }

        [TestMethod]
        public void getGraphicCardFromDatabase()
        {
            GraphicCardProcess process = new GraphicCardProcess();
            GraphicCard graphicCard = new GraphicCard();

            graphicCard.setDescription("Dies ist ein Test");
            graphicCard.setClockRate(5);
            graphicCard.setModel("GTX1234");
            graphicCard.setMemory(2000);

            process.save(graphicCard);
            GraphicCard dbGraphicCard = process.getById(1);

            Assert.AreEqual(graphicCard.getDescription(), dbGraphicCard.getDescription());
        }

        [TestMethod]
        public void getRandomAccessMemoryFromDatabase()
        {
            RandomAccessMemoryProcess process = new RandomAccessMemoryProcess();
            RandomAccessMemory ram = new RandomAccessMemory();

            ram.setDescription("Dies ist ein Test");
            ram.setMemory(8000);
            ram.setClockRate(12000);

            process.save(ram);
            RandomAccessMemory dbRAM = process.getById(1);

            Assert.AreEqual(ram.getMomory(), dbRAM.getMomory());
        }

        [TestMethod]
        public void getMotherboardFromDatabase()
        {
            MotherboardProcess process = new MotherboardProcess();
            Motherboard motherboard = new Motherboard();

            motherboard.setDescription("Dies ist ein Test");
            motherboard.setInch(24.2);
            motherboard.setSocket("PGA 988B");

            process.save(motherboard);
            Motherboard dbMotherboard = process.getById(1);

            Assert.AreEqual(motherboard.getSocket(), dbMotherboard.getSocket());
        }

        [TestMethod]
        public void getProcessorFromDatabase()
        {
            ProcessorProcess process = new ProcessorProcess();
            Processor processor = new Processor();

            processor.setDescription("Dies ist ein Test");
            processor.setModel("i5");
            processor.setCore(4);
            processor.setCommandSet("RISC");
            processor.setArchitecture(64);
            processor.setClockRate(3.40);

            process.save(processor);
            Processor dbProcessor = process.getById(1);

            Assert.AreEqual(processor.getCommandSet(), dbProcessor.getCommandSet());
        }

        [TestMethod]
        public void getHardDiskDriveFromDatabase()  
        {
            HardDiskDriveProcess process = new HardDiskDriveProcess();
            HardDiskDrive hdd = new HardDiskDrive();

            hdd.setDescription("Dies ist ein Test");
            hdd.setCapacity(1000);
            hdd.setSSD(true);
            hdd.setInch(3.5);

            process.save(hdd);
            HardDiskDrive dbHDD = process.getById(1); 

            Assert.AreEqual(hdd.getCapacity(), dbHDD.getCapacity());
        }

        [TestMethod]
        public void getMonitorFromDatabase()
        {
            MonitorProcess process = new MonitorProcess();
            Monitor monitor = new Monitor();

            monitor.setDescription("Dies ist ein Test");
            //TODO: Schlauere Idee für Auflöung
            monitor.setResolution("1920 x 1080");
            monitor.setInch(24);
            monitor.setAspectRatio("4:3");

            process.save(monitor);
            Monitor dbMonitor = process.getById(1);

            Assert.AreEqual(monitor.getInch(), dbMonitor.getInch());
        }

        [TestMethod]
        public void getInterfaceFromDatabase()
        {
            InterfaceProcess process = new InterfaceProcess();
            Inteface inteface = new Inteface();

            inteface.setName("DVI");
            inteface.setDescription("Digital Visual Interface - Eine elektronische Schnittstelle zur Übertragung von Videodaten.");
            inteface.setSerial(true);
            inteface.setTransferRate(10000); // TB/s natürlich

            process.save(inteface);
            Inteface dbInterface = process.getById(1);

            Assert.AreEqual(inteface.getDescription(), dbInterface.getDescription());
        }
    }
}
