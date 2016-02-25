﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.components
{
    public class PhysicalInterface
    {
        private int id;

        private string name;

        private string description;

        private bool serial;

        private int transferRate;


        public PhysicalInterface()
        {
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public bool Serial
        {
            get { return this.serial; }
            set { this.serial = value; }
        }

        public int TransferRate
        {
            get { return this.transferRate; }
            set { this.transferRate = value; }
        }

    }
}