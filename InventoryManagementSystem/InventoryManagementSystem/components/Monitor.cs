﻿using System.Collections.Generic;

namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Monitor'
    /// </summary>
    public class Monitor
    {
        private List<PhysicalInterfaceWithCount> physicalInterfaces;

        public Monitor() 
        {
            this.physicalInterfaces = new List<PhysicalInterfaceWithCount>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public uint Resolution { get; set; }

        public double Inch { get; set; }

        public uint AspectRatio { get; set; }

        public Producer Producer { get; set; }

        public List<PhysicalInterfaceWithCount> PhysicalInterfaces
        {
            get { return this.physicalInterfaces; }
            set { this.physicalInterfaces = value; }
        }

        public void AddPhysicalInterface(PhysicalInterfaceWithCount physicalInterface)
        {
            this.physicalInterfaces.Add(physicalInterface);
        }
    }

    /// <summary>
    /// Ein grafisches Objekt der Entität 'Monitor' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class MonitorGraphicalObject
    {
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public uint Auflösung { get; set; }
        public double Zoll { get; set; }
        public uint Seitenverhältnis { get; set; }
        public string Hersteller { get; set; }
        public string Schnittstellen { get; set; }

        public void MapFromEntity(Monitor entity)
        {
            this.Id = entity.Id;
            this.Beschreibung = entity.Description;
            this.Auflösung = entity.Resolution;
            this.Zoll = entity.Inch;
            this.Seitenverhältnis = entity.AspectRatio;
            this.Hersteller = entity.Producer.CompanyName;
            string schnittstellen = "";
            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                if (i > 0)
                    schnittstellen += ", ";
                schnittstellen += entity.PhysicalInterfaces[i].PhysicalInterface.Name;
                schnittstellen += " (" + entity.PhysicalInterfaces[i].Number + ")";
            }
            this.Schnittstellen = schnittstellen;
        }
    }
}