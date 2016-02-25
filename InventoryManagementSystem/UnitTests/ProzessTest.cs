using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementSystem;
using InventoryManagementSystem.components;

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

            producer.CompanyName = "Siemens";
            producer.PhoneNumber = 123456;
            producer.Email = "info@siemens.com";
            producer.Website = "www.siemens.de";
            producer.PostalCode = 21335;
            producer.Place = "Lüneburg";
            producer.Street = "Lüneburger Straße";
            producer.HouseNumber = 1;

            process.save(producer);
            Producer dbProducer = process.getById(1);

            Assert.AreEqual(producer.Website, dbProducer.Website);
        }

        [TestMethod]
        public void getGraphicCardFromDatabase()
        {
            GraphicCardProcess process = new GraphicCardProcess();
            GraphicCard graphicCard = new GraphicCard();

            graphicCard.Description = "Dies ist ein Test";
            graphicCard.ClockRate = 5;
            graphicCard.Model = "GTX1234";
            graphicCard.Memory = 2000;

            process.save(graphicCard);
            GraphicCard dbGraphicCard = process.getById(1);

            Assert.AreEqual(graphicCard.Description, dbGraphicCard.Description);
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

            motherboard.Description = "Dies ist ein Test";
            motherboard.Inch = 24.2;
            motherboard.Socket = "PGA 988B";

            process.save(motherboard);
            Motherboard dbMotherboard = process.getById(1);

            Assert.AreEqual(motherboard.Socket, dbMotherboard.Socket);
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

            monitor.Description = "Dies ist ein Test";
            monitor.Resolution = 1000;
            monitor.Inch = 24;
            monitor.AspectRatio = 4;

            process.save(monitor);
            Monitor dbMonitor = process.getById(1);

            Assert.AreEqual(monitor.Inch, dbMonitor.Inch);
        }

        [TestMethod]
        public void getInterfaceFromDatabase()
        {
            InterfaceProcess process = new InterfaceProcess();
            PhysicalInterface physicalInterface = new PhysicalInterface();

            physicalInterface.Name = "DVI";
            physicalInterface.Description = "Digital Visual Interface - Eine elektronische Schnittstelle zur Übertragungn von Videodaten.";
            physicalInterface.Serial = true;
            physicalInterface.TransferRate = 1000;

            process.save(physicalInterface);
            PhysicalInterface dbPhysicalInterface = process.getById(1);

            Assert.AreEqual(physicalInterface.Description, dbPhysicalInterface.Description);
        }
    }
}
