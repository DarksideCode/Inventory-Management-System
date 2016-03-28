using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /*
    *   Data-Access-Klasse der Entität 'Arbeitsspeicher'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class RandomAccessMemoryDataAccess : DatabaseBasic
    {
        /**
        * gibt den Tabellen Namen zurück.
        **/
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "arbeitsspeicher";
        }

        /*
        *   Speichert ein Objekt der Entität 'Arbeitsspeicher' in die Datenbank
        */
        public void Save(RandomAccessMemory entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `" + this.getTableName() + "`(`Beschreibung`, `Speicher`, `Taktrate`, "
                                + "`ID_Hersteller`) VALUES ('" + entity.Description + "'," + entity.Memory + ","
                                + entity.ClockRate + "," + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Löscht ein Objekt der Entität 'Arbeitsspeicher' aus der Datenbank 
         */
        public void Delete(RandomAccessMemory entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `"+ this.getTableName() +"` WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Verändert einen bestehenden Datensatz der Entität `Arbeitsspeicher` in der Datenbank
         */
        public void Update(RandomAccessMemory entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            //UPDATE `ims_arbeitsspeicher` SET `ID`=[value-1],`Beschreibung`=[value-2],`Speicher`=[value-3],`Taktrate`=[value-4],`ID_Hersteller`=[value-5] WHERE 1
            command.CommandText = "UPDATE `"+ this.getTableName() +"` SET `Beschreibung`='" + entity.Description + "', `Speicher`=" + entity.Memory + ", `Taktrate`='" + entity.ClockRate 
                                + "', `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

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
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "` WHERE id = " + id;

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
        *   Liest alle Datensätze der Entität 'Grafikkarte' aus der Datenbank
        */
        public List<RandomAccessMemory> GetAllEntities()
        {
            List<RandomAccessMemory> rams = new List<RandomAccessMemory>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "`";

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

            return ram;
        }
    }
}
