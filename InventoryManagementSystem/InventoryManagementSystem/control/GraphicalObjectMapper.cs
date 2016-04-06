using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.components;

namespace InventoryManagementSystem.control
{
    public class GraphicalObjectMapper
    {
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
