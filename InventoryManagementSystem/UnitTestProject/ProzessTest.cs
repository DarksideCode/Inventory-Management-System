using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementSystem.components;
using InventoryManagementSystem.dataAccess;

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
        public void getPhysicalInterfaceFromDatabase()
        {
            PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
            PhysicalInterface physicalInterface = new PhysicalInterface();

            physicalInterface.Name = "DVI";
            physicalInterface.Description = "Digital Visual Interface - Eine elektronische Schnittstelle zur Übertragungn von Videodaten.";
            physicalInterface.Serial = true;
            physicalInterface.TransferRate = 1000;

            dataAccess.Save(physicalInterface);
            PhysicalInterface dbPhysicalInterface = dataAccess.GetLastEntity();

            Assert.AreEqual(physicalInterface.Description, dbPhysicalInterface.Description);
        }

        [TestMethod]
        public void getGraphicCardFromDatabase()
        {
            GraphicCardDataAccess dataAccess = new GraphicCardDataAccess();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            GraphicCard graphicCard = new GraphicCard();

            graphicCard.Description = "Dies ist ein Test";
            graphicCard.ClockRate = 5;
            graphicCard.Model = "GTX1234";
            graphicCard.Memory = 2000;
            graphicCard.Producer = producerDataAccess.GetLastEntity();
            graphicCard.AddPhysicalInterface(new PhysicalInterfaceWithCount(physicalInterfaceDataAccess.GetLastEntity(), 3));

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
            MotherboardDataAccess dataAccess = new MotherboardDataAccess();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            Motherboard motherboard = new Motherboard();

            motherboard.Description = "Dies ist ein Test";
            motherboard.Inch = 24.2;
            motherboard.Socket = "PGA 988B";
            motherboard.Producer = producerDataAccess.GetLastEntity();
            motherboard.AddPhysicalInterface(new PhysicalInterfaceWithCount(physicalInterfaceDataAccess.GetLastEntity(), 3));

            dataAccess.Save(motherboard);
            Motherboard dbMotherboard = dataAccess.GetLastEntity();

            Assert.AreEqual(motherboard.Socket, dbMotherboard.Socket);
        }

        [TestMethod]
        public void getProcessorFromDatabase()
        {
            ProcessorDataAccess dataAccess = new ProcessorDataAccess();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            Processor processor = new Processor();

            processor.Description = "Dies ist ein Test";
            processor.Model = "i5";
            processor.Core = 4;
            processor.CommandSet = "RISC";
            processor.Architecture = 64;
            processor.ClockRate = 3.40;
            processor.Producer = producerDataAccess.GetLastEntity();

            dataAccess.Save(processor);
            Processor dbProcessor = dataAccess.GetLastEntity();

            Assert.AreEqual(processor.CommandSet, dbProcessor.CommandSet);
        }

        [TestMethod]
        public void getDiskFromDatabase()  
        {
            DiskDataAccess dataAccess = new DiskDataAccess();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            Disk hdd = new Disk();

            hdd.Description = "Dies ist ein Test";
            hdd.Capacity = 1000;
            hdd.Ssd = false;
            hdd.Inch = 3.5;
            hdd.Producer = producerDataAccess.GetLastEntity();
            hdd.AddPhysicalInterface(new PhysicalInterfaceWithCount(physicalInterfaceDataAccess.GetLastEntity(), 3));

            dataAccess.Save(hdd);
            Disk dbHDD = dataAccess.GetLastEntity();

            Assert.AreEqual(hdd.Capacity, dbHDD.Capacity);
        }

        [TestMethod]
        public void getMonitorFromDatabase()
        {
            MonitorDataAccess dataAccess = new MonitorDataAccess();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            Monitor monitor = new Monitor();

            monitor.Description = "Dies ist ein Test";
            monitor.Resolution = 1000;
            monitor.Inch = 24;
            monitor.AspectRatio = 4;
            monitor.Producer = producerDataAccess.GetLastEntity();
            monitor.AddPhysicalInterface(new PhysicalInterfaceWithCount(physicalInterfaceDataAccess.GetLastEntity(), 3));

            dataAccess.Save(monitor);
            Monitor dbMonitor = dataAccess.GetLastEntity();

            Assert.AreEqual(monitor.Inch, dbMonitor.Inch);
        }


        [TestMethod]
        public void deleteMonitorFromDatabase()
        {
            MonitorDataAccess dataAccess = new MonitorDataAccess();
            Monitor monitor = dataAccess.GetLastEntity();

            dataAccess.Delete(monitor);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || monitor.Id == dataAccess.GetLastEntity().Id);
        }

        [TestMethod]
        public void deleteDiskFromDatabase()
        {
            DiskDataAccess dataAccess = new DiskDataAccess();
            Disk disk = dataAccess.GetLastEntity();

            dataAccess.Delete(disk);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || disk.Id == dataAccess.GetLastEntity().Id);
        }

        [TestMethod]
        public void deleteProcessorFromDatabase()
        {
            ProcessorDataAccess dataAccess = new ProcessorDataAccess();
            Processor processor = dataAccess.GetLastEntity();

            dataAccess.Delete(processor);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || processor.Id == dataAccess.GetLastEntity().Id);
        }

        [TestMethod]
        public void deleteMotherboardFromDatabase()
        {
            MotherboardDataAccess dataAccess = new MotherboardDataAccess();
            Motherboard motherboard = dataAccess.GetLastEntity();

            dataAccess.Delete(motherboard);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || motherboard.Id == dataAccess.GetLastEntity().Id);
        }

        [TestMethod]
        public void deleteRandomAccessMemoryFromDatabase()
        {
            RandomAccessMemoryDataAccess dataAccess = new RandomAccessMemoryDataAccess();
            RandomAccessMemory randomAccessMemory = dataAccess.GetLastEntity();

            dataAccess.Delete(randomAccessMemory);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || randomAccessMemory.Id == dataAccess.GetLastEntity().Id);
        }

        [TestMethod]
        public void deleteGraphicCardFromDatabase()
        {
            GraphicCardDataAccess dataAccess = new GraphicCardDataAccess();
            GraphicCard graphicCard = dataAccess.GetLastEntity();

            dataAccess.Delete(graphicCard);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || graphicCard.Id == dataAccess.GetLastEntity().Id);
        }

        [TestMethod]
        public void deleteProducerFromDatabase()
        {
            ProducerDataAccess dataAccess = new ProducerDataAccess();
            Producer producer = dataAccess.GetLastEntity();

            dataAccess.Delete(producer);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || producer.Id == dataAccess.GetLastEntity().Id);
        }

        [TestMethod]
        public void deletePhysicalInterfaceFromDatabase()
        {
            PhysicalInterfaceDataAccess dataAccess = new PhysicalInterfaceDataAccess();
            PhysicalInterface physicalInterface = dataAccess.GetLastEntity();

            dataAccess.Delete(physicalInterface);

            Assert.IsTrue(dataAccess.GetLastEntity() == null || physicalInterface.Id == dataAccess.GetLastEntity().Id);
        }
    }
}
