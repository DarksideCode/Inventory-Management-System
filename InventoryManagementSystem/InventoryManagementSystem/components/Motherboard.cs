using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.components
{
    class Motherboard
    {
        private int id;

        private string description;

        private int inch;

        private string socket;

        private List<PhysicalInterface> physicalInterfaces;

        private Producer producer;


        public Motherboard() { }

        public int Id
        {
            get { return this.id; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int Inch
        {
            get { return this.inch; }
            set { this.inch = value; }
        }

        public string Socket
        {
            get { return this.socket; }
            set { this.socket = value; }
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
