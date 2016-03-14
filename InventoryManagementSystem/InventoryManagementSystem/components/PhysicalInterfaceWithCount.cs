using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.components
{
    public class PhysicalInterfaceWithCount
    {
        private PhysicalInterface physicalInterface;

        private int number;

        public PhysicalInterfaceWithCount (PhysicalInterface physicalInterface, int number)
        {
            this.physicalInterface = physicalInterface;
            this.number = number;
        }

        public PhysicalInterface PhysicalInterface
        {
            get { return this.physicalInterface; }
            set { this.physicalInterface = value; }
        }

        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }
    }
}
