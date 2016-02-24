using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.components
{
    class Monitor
    {
        private int id;

        private string description;

        private int resolution;

        private int inch;

        private int aspectRatio;

        private Producer producer;

        private List<PhysicalInterface> physicalInterfaces;

        public Monitor() { }

        public int Id
        {
            get { return this.id; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int Resolution
        {
            get { return this.resolution; }
            set { this.resolution = value; }
        }

        public int Inch
        {
            get { return this.inch; }
            set { this.inch = value; }
        }

        public int AspectRatio
        {
            get { return this.aspectRatio; }
            set { this.aspectRatio = value; }
        }

        public Producer Producer
        {
            get { return this.producer; }
            set { this.producer = value; }
        }

        public List<PhysicalInterface> PhysicalInterfaces
        {
            get { return this.physicalInterfaces; }
            set { this.physicalInterfaces = value; }
        }

        public void AddPhysicalInterface(PhysicalInterface physicalInterface)
        {
            this.physicalInterfaces.Add(physicalInterface);
        }

    }
}
