using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementSystem.components;
using InventoryManagementSystem.validation;

namespace UnitTests
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod]
        public void validateProducer()
        {
            Producer producer = new Producer();
            ProducerValidator validator = new ProducerValidator();

            producer.CompanyName = "Siemens";
            producer.PhoneNumber = 123456;
            producer.Email = "info@siemens.com";
            producer.Website = "http://siemens.de";
            producer.PostalCode = 21335;
            producer.Place = "Lüneburg";
            producer.Street = "Lüneburger Straße";
            producer.HouseNumber = 1;

            Assert.AreEqual(false, validator.CheckConsistency(producer));
        }

        [TestMethod]
        public void validateGraphicCard()
        {
            GraphicCard graphicCard = new GraphicCard();
            GraphicCardValidator validator = new GraphicCardValidator();

            graphicCard.Description = "Dies ist ein Test";
            graphicCard.ClockRate = 5;
            graphicCard.Model = "ASD::123";
            graphicCard.Memory = 2000;

            Assert.AreEqual(false, validator.CheckConsistency(graphicCard));
        }

        [TestMethod]
        public void validateRandomAccessMemory()
        {
            RandomAccessMemory ram = new RandomAccessMemory();
            RandomAccessMemoryValidator validator = new RandomAccessMemoryValidator();

            ram.Description = "Dies ist ein Test";
            ram.Memory = 8000;
            ram.ClockRate = 12000;

            Assert.AreEqual(validator.Validate(ram), null);
        }

        [TestMethod]
        public void validateMotherboard()
        {
            Motherboard motherboard = new Motherboard();
            MotherboardValidator validator = new MotherboardValidator();

            motherboard.Description = "Dies ist ein Test";
            motherboard.Inch = 24.2;
            motherboard.Socket = null;

            Assert.AreNotEqual(validator.Validate(motherboard), null);
        }

        [TestMethod]
        public void validateProcessor()
        {
            Processor processor = new Processor();
            ProcessorValidator validator = new ProcessorValidator();

            processor.Description = "Dies ist ein Test";
            processor.Model = "i5";
            processor.Core = -6;
            processor.CommandSet = "RISCC!";
            processor.Architecture = 64;
            processor.ClockRate = 3.40;

            Assert.AreEqual(validator.Validate(processor), null);
        }

        [TestMethod]
        public void validateDisk()
        {
            Disk hdd = new Disk();
            DiskValidator validator = new DiskValidator();

            hdd.Description = "Dies ist ein Test";
            hdd.Capacity = -50;
            hdd.Ssd = false;
            hdd.Inch = -3.5;

            Assert.AreEqual(validator.Validate(hdd), null);
        }

        [TestMethod]
        public void validateMonitor()
        {
            Monitor monitor = new Monitor();
            MonitorValidator validator = new MonitorValidator();

            monitor.Description = "Dies ist ein Test";
            monitor.Resolution = 1000;
            monitor.Inch = 24;
            monitor.AspectRatio = 4;

            Assert.AreNotEqual(validator.Validate(monitor), null);
        }

        [TestMethod]
        public void validatePhysicalInterface()
        {
            PhysicalInterface physicalInterface = new PhysicalInterface();
            PhysicalInterfaceValidator validator = new PhysicalInterfaceValidator();

            physicalInterface.Name = "DVI";
            physicalInterface.Description = "Digital Visual Interface - Eine elektronische Schnittstelle zur Übertragungn von Videodaten.";
            physicalInterface.Serial = true;
            physicalInterface.TransferRate = 1000;

            Assert.AreNotEqual(validator.Validate(physicalInterface), null);
        }

    }
}
