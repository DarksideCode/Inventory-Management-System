using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.DB_Models
{
    /*
    *   Data-Access-Klasse der Entität 'Festplatte'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class DiskDataAccess : DatabaseBasic
    {
        /**
        * gibt den Tabellen Namen zurück.
        **/
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "festplatte";
        }

        /*
         *   Speichert ein Objekt der Entität 'Festplatte' in die Datenbank
         */
        public void Save(Disk entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `" + this.getTableName() + "`(`Beschreibung`, `Kapazität`, `SSD`, `Zoll`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "'," + entity.Capacity + ",'" + entity.Ssd + "','" + entity.Inch + "',"
                                + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `ims_festplatte_schnittstelle`(`ID_Festplatte`, `ID_Schnittstelle`, `Anzahl`) "
                                    + "VALUES (" + this.GetLastEntity().Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + ","
                                    + entity.PhysicalInterfaces[i].Number + ")";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /*
         *  Löscht ein Objekt der Entität 'Festplatte' aus der Datenbank 
         */
        public void Delete(Disk entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlCommand interfaceCommand = connection.CreateCommand();

            command.CommandText = "DELETE FROM `" + this.getTableName() + "` WHERE id = " + entity.Id;
            interfaceCommand.CommandText = "DELETE FROM `ims_festplatte_schnittstelle` WHERE id_festplatte = " + entity.Id;

            connection.Open();
            interfaceCommand.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        *   Liest den Datensatz der Entität 'Festplatte' aus der Datenbank, die der übergebenen ID
        *   entspricht
        */
        public Disk GetEntityById(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            reader.Read();
            Disk disk = this.MapToEntity(reader);

            connection.Close();

            return disk;
        }

        /*
        *   Liest den zuletzt gespeicherten Datensatz der Entität 'Festplatte' aus der Datenbank
        */
        public Disk GetLastEntity()
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(id) FROM `" + this.getTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            //Prüft, ob eine ID zurück gegeben wurde, falls nicht ist die Tabelle leer und es wird null zurück gegeben
            if (reader.GetValue(0).ToString().Length > 0) {
                int id = Int32.Parse(reader.GetValue(0).ToString());
                connection.Close();
                return this.GetEntityById(id);
            } else {
                connection.Close();
                return null;
            }
        }

        /*
        *   Liest alle Datensätze der Entität 'Festplatte' aus der Datenbank
        */
        public List<Disk> GetAllEntities()
        {
            List<Disk> disks = new List<Disk>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Disk disk = this.MapToEntity(reader);
                disks.Add(disk);
            }

            return disks;
        }

        /*
        *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Festplatte' (Disk)
        */
        private Disk MapToEntity(MySqlDataReader reader)
        {
            Disk disk = new Disk();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            disk.Id = Int32.Parse(reader.GetValue(0).ToString());
            disk.Description = reader.GetValue(1).ToString();
            disk.Capacity = Int32.Parse(reader.GetValue(2).ToString());
            disk.Ssd = Boolean.Parse(reader.GetValue(3).ToString());
            disk.Inch = Double.Parse(reader.GetValue(4).ToString());
            disk.Producer = producerDataAccess.GetEntityById(Int32.Parse(reader.GetValue(5).ToString()));
            disk.PhysicalInterfaces = this.GetPhysicalInterfaces(disk);

            return disk;
        }

        /*
         *  Liest alle Beziehungen zu der Entität 'Schnittstelle' aus der Datenbank
         */
        private List<PhysicalInterfaceWithCount> GetPhysicalInterfaces(Disk entity)
        {
            List<PhysicalInterfaceWithCount> physicalInterfaces = new List<PhysicalInterfaceWithCount>();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `ims_festplatte_schnittstelle` WHERE id_festplatte = " + entity.Id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PhysicalInterface physicalInterface = physicalInterfaceDataAccess.GetEntityById(Int32.Parse(reader.GetValue(1).ToString()));
                int count = Int32.Parse(reader.GetValue(2).ToString());
                physicalInterfaces.Add(new PhysicalInterfaceWithCount(physicalInterface, count));
            }

            return physicalInterfaces;
        }
    }
}
