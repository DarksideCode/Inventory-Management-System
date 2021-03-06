﻿using System.Collections.Generic;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.components
{
    /// <summary>
    /// Klasse für die Entität 'Festplatte'
    /// </summary>
    public class Disk
    {
        private List<PhysicalInterfaceWithCount> physicalInterfaces;

        public Disk() 
        {
            this.physicalInterfaces = new List<PhysicalInterfaceWithCount>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public ulong Capacity { get; set; }

        public bool Ssd { get; set; }

        public double Inch { get; set; }
       
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
    /// Ein grafisches Objekt der Entität 'Festplatte' zur Anzeige der Informationen auf
    /// der Benutzeroberfläche.
    /// </summary>
    public class DiskGraphicalObject
    {
        public int Id { get; set; }
        public string Beschreibung { get; set; }
        public ulong Kapazität { get; set; }
        public string SSD { get; set; }
        public double Zoll { get; set; }
        public string Hersteller { get; set; }
        public string Schnittstellen { get; set; }

        /// <summary>
        /// Wandelt ein Objekt der Entität 'Festplatte' in eine grafisches Objekt.
        /// Übersetzt englische Begriffe und zählt alle Schnittstellen in einer Liste auf.
        /// </summary>
        /// <param name="entity">Objekt vom Typ 'Festplatte'</param>
        public void MapFromEntity(Disk entity)
        {
            this.Id = entity.Id;
            this.Beschreibung = entity.Description;
            this.Kapazität = UnitConverter.ConvertToHigher(entity.Capacity);
            if (entity.Ssd)
                this.SSD = "Ja";
            else
                this.SSD = "Nein";
            this.Zoll = entity.Inch;
            this.Hersteller = entity.Producer.CompanyName;
            string schnittstellen = "";
            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++ )
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