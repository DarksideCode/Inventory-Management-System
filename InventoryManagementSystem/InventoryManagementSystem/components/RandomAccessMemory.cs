using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.components
{
    /*
     *  Klasse für die Entität Arbeitsspeicher
     */
    public class RandomAccessMemory
    {
        private int id;

        private string description;

        private int capacity;

        private double clockRate;

        private Producer producer;

        public RandomAccessMemory() { }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int Memory
        {
            get { return this.capacity; }
            set { this.capacity = value; }
        }

        public double ClockRate
        {
            get { return this.clockRate; }
            set { this.clockRate = value; }
        }

        public Producer Producer
        {
            get { return this.producer; }
            set { this.producer = value; }
        }

    }
}
