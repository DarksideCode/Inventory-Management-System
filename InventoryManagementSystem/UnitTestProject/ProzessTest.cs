using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementSystem.components;
using InventoryManagementSystem.DB_Models;

namespace UnitTests
{
    [TestClass]
    public class ProzessTest
    {
        [TestMethod]
        public void getProducerFromDatabase()
        {
            ProducerDataAccess dataAccess = new ProducerDataAccess();
            Producer producer = new Producer();

            producer.CompanyName = "Siemens";
            producer.PhoneNumber = 123456;
            producer.Email = "info@siemens.com";
            producer.Website = "www.siemens.de";
            producer.PostalCode = 21335;
            producer.Place = "Lüneburg";
            producer.Street = "Lüneburger Straße";
            producer.HouseNumber = 1;

            dataAccess.Save(producer);
            Producer dbProducer = dataAccess.GetLastEntity();

            Assert.AreEqual(producer.Website, dbProducer.Website);
        }

        [TestMethod]
        public void getGraphicCardFromDatabase()
        {
            GraphicCardDataAccess dataAccess = new GraphicCardDataAccess();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            GraphicCard graphicCard = new GraphicCard();

            graphicCard.Description = "Dies ist ein Test";
            graphicCard.ClockRate = 5;
            graphicCard.Model = "GTX1234";
            graphicCard.Memory = 2000;
            graphicCard.Producer = producerDataAccess.GetLastEntity();

            dataAccess.Save(graphicCard);
            GraphicCard dbGraphicCard = dataAccess.GetLastEntity();

            Assert.AreEqual(graphicCard.Description, dbGraphicCard.Description);
        }

        [TestMethod]
        public void getRandomAccessMemoryFromDatabase()
        {
            RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            RandomAccessMemory ram = new RandomAccessMemory();

            ram.Description = "Dies ist ein Test";
            ram.Memory = 8000;
            ram.ClockRate = 12000;
            ram.Producer = producerDataAccess.GetLastEntity();

            dataAccess.Save(ram);
            RandomAccessMemory dbRAM = dataAccess.GetLastEntity();

            Assert.AreEqual(ram.Memory, dbRAM.Memory);
        }

        [TestMethod]
        public void getMotherboardFromDatabase()
        {
            MotherboardDataAccess process = new MotherboardDataAccess();
            Motherboard motherboard = new Motherboard();

            motherboard.Description = "Dies ist ein Test";
            motherboard.Inch = 24.2;
            motherboard.Socket = "PGA 988B";

            process.Save(motherboard);
            Motherboard dbMotherboard = process.GetEntityById(1);

            Assert.AreEqual(motherboard.Socket, dbMotherboard.Socket);
        }

        [TestMethod]
        public void getProcessorFromDatabase()
        {
            ProcessorDataAccess process = new ProcessorDataAccess();
            Processor processor = new Processor();

            processor.Description = "Dies ist ein Test";
            processor.Model = "i5";
            processor.Core = 4;
            processor.CommandSet = "RISC";
            processor.Architecture = 64;
            processor.ClockRate = 3.40;

            process.Save(processor);
            Processor dbProcessor = process.GetEntityById(1);

            Assert.AreEqual(processor.CommandSet, dbProcessor.CommandSet);
        }

        [TestMethod]
        public void getDiskFromDatabase()  
        {
            DiskDataAccess process = new DiskDataAccess();
            Disk hdd = new Disk();

            hdd.Description = "Dies ist ein Test";
            hdd.Capacity = 1000;
            hdd.Ssd = false;
            hdd.Inch = 3.5;

            process.Save(hdd);
            Disk dbHDD = process.GetEntityById(1);

            Assert.AreEqual(hdd.Capacity, dbHDD.Capacity);
        }

        [TestMethod]
        public void getMonitorFromDatabase()
        {
            MonitorDataAccess process = new MonitorDataAccess();
            Monitor monitor = new Monitor();

            monitor.Description = "Dies ist ein Test";
            monitor.Resolution = 1000;
            monitor.Inch = 24;
            monitor.AspectRatio = 4;

            process.Save(monitor);
            Monitor dbMonitor = process.GetEntityById(1);

            Assert.AreEqual(monitor.Inch, dbMonitor.Inch);
        }

        [TestMethod]
        public void getPhysicalInterfaceFromDatabase()
        {
            PhysicalInterfaceDataAccess process = new PhysicalInterfaceDataAccess();
            PhysicalInterface physicalInterface = new PhysicalInterface();

            physicalInterface.Name = "DVI";
            physicalInterface.Description = "Digital Visual Interface - Eine elektronische Schnittstelle zur Übertragungn von Videodaten.";
            physicalInterface.Serial = true;
            physicalInterface.TransferRate = 1000;

            process.Save(physicalInterface);
            PhysicalInterface dbPhysicalInterface = process.GetEntityById(1);

            Assert.AreEqual(physicalInterface.Description, dbPhysicalInterface.Description);
        }
    }
}
