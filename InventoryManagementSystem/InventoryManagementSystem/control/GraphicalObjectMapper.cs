using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.components;

namespace InventoryManagementSystem.control
{
    /// <summary>
    /// Stellt eine Reihe von Methoden für das Mapping von Entitäten in ihre jeweiligen
    /// GraphicalObject-Klassen zur Verfügung.
    /// </summary>
    public class GraphicalObjectMapper
    {
        /// <summary>
        /// Mappt eine Liste von Festplatten auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von Disk</param>
        /// <returns>Liste von DiskGraphicalObject</returns>
        public List<DiskGraphicalObject> MapToGraphicalObject(List<Disk> entities)
        {
            List<DiskGraphicalObject> list = new List<DiskGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                DiskGraphicalObject go = new DiskGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }

        /// <summary>
        /// Mappt eine Liste von Grafikkarten auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von GrapgicCard</param>
        /// <returns>Liste von GraphicCardGraphicalObject</returns>
        public List<GraphicCardGraphicalObject> MapToGraphicalObject(List<GraphicCard> entities)
        {
            List<GraphicCardGraphicalObject> list = new List<GraphicCardGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                GraphicCardGraphicalObject go = new GraphicCardGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }

        /// <summary>
        /// Mappt eine Liste von Monitoren auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von Monitor</param>
        /// <returns>Liste von MonitorGraphicalObject</returns>
        public List<MonitorGraphicalObject> MapToGraphicalObject(List<Monitor> entities)
        {
            List<MonitorGraphicalObject> list = new List<MonitorGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                MonitorGraphicalObject go = new MonitorGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }

        /// <summary>
        /// Mappt eine Liste von Hauptplatinen auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von Motherboard</param>
        /// <returns>Liste von MotherboardGraphicalObject</returns>
        public List<MotherboardGraphicalObject> MapToGraphicalObject(List<Motherboard> entities)
        {
            List<MotherboardGraphicalObject> list = new List<MotherboardGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                MotherboardGraphicalObject go = new MotherboardGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }

        /// <summary>
        /// Mappt eine Liste von Schnittstellen auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von PhysicalInterface</param>
        /// <returns>Liste von PhysicalInterfaceGraphicalObject</returns>
        public List<PhysicalInterfaceGraphicalObject> MapToGraphicalObject(List<PhysicalInterface> entities)
        {
            List<PhysicalInterfaceGraphicalObject> list = new List<PhysicalInterfaceGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                PhysicalInterfaceGraphicalObject go = new PhysicalInterfaceGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }

        /// <summary>
        /// Mappt eine Liste von Prozessoren auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von Processor</param>
        /// <returns>Liste von ProcessorGraphicalObject</returns>
        public List<ProcessorGraphicalObject> MapToGraphicalObject(List<Processor> entities)
        {
            List<ProcessorGraphicalObject> list = new List<ProcessorGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                ProcessorGraphicalObject go = new ProcessorGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }

        /// <summary>
        /// Mappt eine Liste von Herstellern auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von Producer</param>
        /// <returns>Liste von ProducerGraphicalObject</returns>
        public List<ProducerGraphicalObject> MapToGraphicalObject(List<Producer> entities)
        {
            List<ProducerGraphicalObject> list = new List<ProducerGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                ProducerGraphicalObject go = new ProducerGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }

        /// <summary>
        /// Mappt eine Liste von Arbeitsspeichern auf deren Anzeige-Objekte
        /// </summary>
        /// <param name="entities">Liste von RandomAccessMemory</param>
        /// <returns>Liste von RandomAccessMemoryGraphicalObject</returns>
        public List<RandomAccessMemoryGraphicalObject> MapToGraphicalObject(List<RandomAccessMemory> entities)
        {
            List<RandomAccessMemoryGraphicalObject> list = new List<RandomAccessMemoryGraphicalObject>();
            for (int i = 0; i < entities.Count; i++)
            {
                RandomAccessMemoryGraphicalObject go = new RandomAccessMemoryGraphicalObject();
                go.MapFromEntity(entities[i]);
                list.Add(go);
            }
            return list;
        }
    }
}