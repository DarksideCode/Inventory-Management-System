using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.DB_Models
{
    /*
    *   Data-Access-Klasse der Entität 'Arbeitsspeicher'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class RandomAccessMemoryDataAccess
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
        *   Speichert ein Objekt der Entität 'Arbeitsspeicher' in die Datenbank
        */
        public void Save(RandomAccessMemory entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `ims_arbeitsspeicher`(`Beschreibung`, `Speicher`, `Taktrate`, "
                                + "`ID_Hersteller`) VALUES ('" + entity.Description + "'," + entity.Memory + ","
                                + entity.ClockRate + "," + entity.Producer.Id + ")";

            //TODO: Beziehung zu Schnittstellen in Datenbank speichern

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        *   Liest den Datensatz der Entität 'Arbeitsspeicher' aus der Datenbank, die der übergebenen ID
        *   entspricht
        */
        public RandomAccessMemory GetEntityById(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_arbeitsspeicher` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            reader.Read();
            RandomAccessMemory ram = this.MapToEntity(reader);

            connection.Close();

            return ram;
        }

        /*
        *   Liest den zuletzt gespeicherten Datensatz der Entität 'Arbeitsspeicher' aus der Datenbank
        */
        public RandomAccessMemory GetLastEntity()
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(id) FROM `ims_arbeitsspeicher`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = Int32.Parse(reader.GetValue(0).ToString());

            connection.Close();

            return this.GetEntityById(id);
        }

        /*
        *   Liest alle Datensätze der Entität 'Grafikkarte' aus der Datenbank
        */
        public List<RandomAccessMemory> GetAllEntities()
        {
            List<RandomAccessMemory> rams = new List<RandomAccessMemory>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_arbeitsspeicher`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                RandomAccessMemory ram = this.MapToEntity(reader);
                rams.Add(ram);
            }

            return rams;
        }

        /*
        *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Arbeitsspeicher' (RandomAccessMemory)
        */
        private RandomAccessMemory MapToEntity(MySqlDataReader reader)
        {
            RandomAccessMemory ram = new RandomAccessMemory();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            ram.Id = Int32.Parse(reader.GetValue(0).ToString());
            ram.Description = reader.GetValue(1).ToString();
            ram.Memory = Int32.Parse(reader.GetValue(2).ToString());
            ram.ClockRate = Double.Parse(reader.GetValue(3).ToString());
            ram.Producer = producerDataAccess.GetEntityById(Int32.Parse(reader.GetValue(4).ToString()));

            //TODO: Schnittstellen aus der Datenbank lesen.

            return ram;
        }
    }
}
