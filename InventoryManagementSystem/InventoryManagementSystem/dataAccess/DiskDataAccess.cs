using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.DB_Models
{
    /*
    *   Data-Access-Klasse der Entität 'Festplatte'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class DiskDataAccess
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
         *   Speichert ein Objekt der Entität 'Festplatte' in die Datenbank
         */
        public void Save(Disk entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `ims_festplatte`(`Beschreibung`, `Kapazität`, `SSD`, `Zoll`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "'," + entity.Capacity + ",'" + entity.Ssd + "','" + entity.Inch + "',"
                                + entity.Producer.Id + ")";

            //TODO: Beziehung zu Schnittstellen in Datenbank speichern

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Löscht ein Objekt der Entität 'Festplatte' aus der Datenbank 
         */
        public void Delete(Disk entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `ims_festplatte` WHERE id = " + entity.Id;

            connection.Open();
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
            command.CommandText = "SELECT * FROM `ims_festplatte` WHERE id = " + id;

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
            command.CommandText = "SELECT MAX(id) FROM `ims_festplatte`";

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
            command.CommandText = "SELECT * FROM `ims_festplatte`";

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

            //TODO: Schnittstellen aus der Datenbank lesen.

            return disk;
        }
    }
}
