﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.components
{
    class GraphicCard
    {
        private int id;

        private string description;

        private int clockRate;

        private string model;

        private int memory;

        private Producer producer;

        private List<PhysicalInterface> physicalInterfaces;

        public GraphicCard()
        {
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int ClockRate
        {
            get { return this.clockRate; }
            set { this.clockRate = value; }
        }

        public string Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

        public int Memory
        {
            get { return this.memory; }
            set { this.memory = value; }
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
