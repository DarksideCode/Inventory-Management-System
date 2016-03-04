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

            ram.Description = "Dies ist ein Test";
            ram.Memory = 8000;
            ram.ClockRate = 12000;

            process.save(ram);
            RandomAccessMemory dbRAM = process.getById(1);

            Assert.AreEqual(ram.Memory, dbRAM.Memory);
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

            processor.Description = "Dies ist ein Test";
            processor.Model = "i5";
            processor.Core = 4;
            processor.CommandSet = "RISC";
            processor.Architecture = 64;
            processor.ClockRate = 3.40;

            process.save(processor);
            Processor dbProcessor = process.getById(1);

            Assert.AreEqual(processor.CommandSet, dbProcessor.CommandSet);
        }

        [TestMethod]
        public void getDiskFromDatabase()  
        {
            HardDiskDriveProcess process = new HardDiskDriveProcess();
            Disk hdd = new Disk();

            hdd.Description = "Dies ist ein Test";
            hdd.Capacity = 1000;
            hdd.Ssd = false;
            hdd.Inch = 3.5;

            process.save(hdd);
            Disk dbHDD = process.getById(1);

            Assert.AreEqual(hdd.Capacity, dbHDD.Capacity);
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
        public void getPhysicalInterfaceFromDatabase()
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
