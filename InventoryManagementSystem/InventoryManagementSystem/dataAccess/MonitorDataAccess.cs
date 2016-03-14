using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.DB_Models
{
    /*
    *   Data-Access-Klasse der Entität 'Monitor'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class MonitorDataAccess
    {
       /*
        *   Baut eine Verbindung mit der Datenbank auf, basierend auf den Konfigurationen
        */
        private MySqlConnection CreateConnection()
        {
            ConfigProzesser config = new ConfigProzesser();
            string myConnectionString = "SERVER=localhost;"
                                      + "DATABASE=" + config.getDBName()
                                      + ";UID=" + config.getDBUser()
                                      + ";PASSWORD=" + config.getDBPassword() + ";";
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            return connection;
        }

        /*
        *   Speichert ein Objekt der Entität 'Monitor' in die Datenbank
        */
        public void Save(Monitor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `ims_monitor`(`Beschreibung`, `Auflösung`, `Zoll`, `Seitenverhältnis`, "
                                + "`ID_Hersteller`) VALUES ('" + entity.Description + "'," + entity.Resolution + ",'" + entity.Inch + "',"
                                + "'" + entity.AspectRatio + "'," + entity.Producer.Id + ")";

            //TODO: Beziehung zu Schnittstellen in Datenbank speichern

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Löscht ein Objekt der Entität 'Monitor' aus der Datenbank 
         */
        public void Delete(Monitor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `ims_monitor` WHERE id = " + entity.Id;

            connection.Open();
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
            command.CommandText = "SELECT * FROM `ims_monitor` WHERE id = " + id;

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
            command.CommandText = "SELECT MAX(id) FROM `ims_monitor`";

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
            command.CommandText = "SELECT * FROM `ims_monitor`";

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

            monitor.Id = Int32.Parse(reader.GetValue(0).ToString());
            monitor.Description = reader.GetValue(1).ToString();
            monitor.Resolution = Int32.Parse(reader.GetValue(2).ToString());
            monitor.Inch = Double.Parse(reader.GetValue(3).ToString());
            monitor.AspectRatio = Int32.Parse(reader.GetValue(4).ToString());
            monitor.Producer = producerDataAccess.GetEntityById(Int32.Parse(reader.GetValue(5).ToString()));

            //TODO: Schnittstellen aus der Datenbank lesen.

            return monitor;
        }
    }
}
