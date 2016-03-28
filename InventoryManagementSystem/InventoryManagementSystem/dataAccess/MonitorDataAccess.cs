using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /*
    *   Data-Access-Klasse der Entität 'Monitor'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class MonitorDataAccess : DatabaseBasic
    {
        /**
        * gibt den Tabellen Namen zurück.
        **/
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "monitor";
        }

        /*
        *   Speichert ein Objekt der Entität 'Monitor' in die Datenbank
        */
        public void Save(Monitor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `" + this.getTableName() + "`(`Beschreibung`, `Auflösung`, `Zoll`, `Seitenverhältnis`, "
                                + "`ID_Hersteller`) VALUES ('" + entity.Description + "'," + entity.Resolution + ",'" + entity.Inch + "',"
                                + "'" + entity.AspectRatio + "'," + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `" + this.getTableName() + "_schnittstelle`(`ID_Monitor`, `ID_Schnittstelle`, `Anzahl`) "
                                    + "VALUES (" + this.GetLastEntity().Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + "," 
                                    + entity.PhysicalInterfaces[i].Number + ")";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /*
         *  Löscht ein Objekt der Entität 'Monitor' aus der Datenbank 
         */
        public void Delete(Monitor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `" + this.getTableName() + "` WHERE id = " + entity.Id;
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Verändert einen bestehenden Datensatz der Entität `Monitor` in der Datenbank
         *  Ermittelt auch nicht mehr genutzte Referenzen und löscht diese.
         */
        public void Update(Monitor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlCommand interfaceCommand = connection.CreateCommand();
            string usedInterfaces = "";

            command.CommandText = "UPDATE `" + this.getTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Auflösung`=" + entity.Resolution + ", `Zoll`='" + entity.Inch + "', "
                                + "`Seitenverhältnis`=" + entity.AspectRatio + ", `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

            Console.WriteLine(command.CommandText);

            connection.Open();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                interfaceCommand.CommandText = "UPDATE `" + this.getTableName() + "_schnittstelle` SET `Anzahl`=" + entity.PhysicalInterfaces[i].Number
                                             + " WHERE `ID_Monitor` = " + entity.Id + " AND `ID_Schnittstelle` = " + entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                interfaceCommand.ExecuteNonQuery();
                usedInterfaces += entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                if (i != entity.PhysicalInterfaces.Count - 1)
                {
                    usedInterfaces += ",";
                }
            }
            interfaceCommand.CommandText = "DELETE FROM `" + this.getTableName() + "_schnittstelle` WHERE `ID_Monitor` = " + entity.Id
                                         + " AND `ID_Schnittstelle` NOT IN (" + usedInterfaces + ")";
            interfaceCommand.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        *   Liest den Datensatz der Entität 'Monitor' aus der Datenbank, die der übergebenen ID
        *   entspricht
        */
        public Monitor GetEntityById(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Monitor monitor = this.MapToEntity(reader);
            connection.Close();

            return monitor;
        }

        /*
        *   Liest den zuletzt gespeicherten Datensatz der Entität 'Monitor' aus der Datenbank
        */
        public Monitor GetLastEntity()
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
        *   Liest alle Datensätze der Entität 'Monitor' aus der Datenbank
        */
        public List<Monitor> GetAllEntities()
        {
            List<Monitor> monitors = new List<Monitor>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `" + this.getTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Monitor monitor = this.MapToEntity(reader);
                monitors.Add(monitor);
            }

            return monitors;
        }

        /*
        *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Monitor' (Monitor)
        */
        private Monitor MapToEntity(MySqlDataReader reader)
        {
            Monitor monitor = new Monitor();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();

            monitor.Id = Int32.Parse(reader.GetValue(0).ToString());
            monitor.Description = reader.GetValue(1).ToString();
            monitor.Resolution = Int32.Parse(reader.GetValue(2).ToString());
            monitor.Inch = Double.Parse(reader.GetValue(3).ToString());
            monitor.AspectRatio = Int32.Parse(reader.GetValue(4).ToString());
            monitor.Producer = producerDataAccess.GetEntityById(Int32.Parse(reader.GetValue(5).ToString()));
            monitor.PhysicalInterfaces = this.GetPhysicalInterfaces(monitor);
           
            return monitor;
        }

        /*
         *  Liest alle Beziehungen zu der Entität 'Schnittstelle' aus der Datenbank
         */
        private List<PhysicalInterfaceWithCount> GetPhysicalInterfaces(Monitor entity)
        {
            List<PhysicalInterfaceWithCount> physicalInterfaces = new List<PhysicalInterfaceWithCount>();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `" + this.getTableName() + "_schnittstelle` WHERE id_monitor = " + entity.Id;

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
