using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.components
{
    public class PhysicalInterfaceWithCount
    {
        public PhysicalInterfaceWithCount (PhysicalInterface physicalInterface, int number)
        {
            this.PhysicalInterface = physicalInterface;
            this.Number = number;
        }

        public PhysicalInterface PhysicalInterface { get; set; }

        public int Number { get; set; }
    }
}