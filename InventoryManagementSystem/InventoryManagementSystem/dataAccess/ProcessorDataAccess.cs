using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /*
    *   Data-Access-Klasse der Entität 'Prozessor'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class ProcessorDataAccess : DatabaseBasic
    {
        /**
        * gibt den Tabellen Namen zurück.
        **/
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "prozessor";
        }

        /*
         *   Speichert ein Objekt der Entität 'Prozessor' in die Datenbank
         */
        public void Save(Processor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `" + this.getTableName() + "`(`Beschreibung`, `Modell`, `Kerne`, `Befehlssatz`, `Architektur`, `Taktrate`, "
                                + "`ID_Hersteller`) VALUES ('" + entity.Description + "','" + entity.Model + "'," + entity.Core + ",'" + entity.CommandSet 
                                + "'," + entity.Architecture + ",'" + entity.ClockRate + "'," + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Löscht ein Objekt der Entität 'Prozessor' aus der Datenbank 
         */
        public void Delete(Processor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `" + this.getTableName() + "` WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Verändert einen bestehenden Datensatz der Entität 'Prozessor' in der Datenbank
         */
        public void Update(Processor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            //UPDATE `" + this.getTableName() + "` SET `ID`=[value-1],`Beschreibung`=[value-2],`Modell`=[value-3],`Kerne`=[value-4],`Befehlssatz`=[value-5],`Architektur`=[value-6],`Taktrate`=[value-7],`ID_Hersteller`=[value-8] WHERE 1
            command.CommandText = "UPDATE `" + this.getTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Modell`='" + entity.Model + "', `Kerne`=" + entity.Core 
                                + ", `Befehlssatz`='" + entity.CommandSet + "', `Architektur`=" + entity.Architecture + ", `Taktrate`='" + entity.ClockRate 
                                + "', `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        *   Liest den Datensatz der Entität 'Prozessor' aus der Datenbank, die der übergebenen ID
        *   entspricht
        */
        public Processor GetEntityById(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            reader.Read();
            Processor processor = this.MapToEntity(reader);

            connection.Close();

            return processor;
        }

        /*
        *   Liest den zuletzt gespeicherten Datensatz der Entität 'Prozessor' aus der Datenbank
        */
        public Processor GetLastEntity()
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
        *   Liest alle Datensätze der Entität 'Prozessor' aus der Datenbank
        */
        public List<Processor> GetAllEntities()
        {
            List<Processor> processors = new List<Processor>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Processor processor = this.MapToEntity(reader);
                processors.Add(processor);
            }

            return processors;
        }

        /*
        *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Prozessor' (Processor)
        */
        private Processor MapToEntity(MySqlDataReader reader)
        {
            Processor processor = new Processor();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            processor.Id = Int32.Parse(reader.GetValue(0).ToString());
            processor.Description = reader.GetValue(1).ToString();
            processor.Model = reader.GetValue(2).ToString();
            processor.Core = Int32.Parse(reader.GetValue(3).ToString());
            processor.CommandSet = reader.GetValue(4).ToString();
            processor.Architecture = Int32.Parse(reader.GetValue(5).ToString());
            processor.ClockRate = Double.Parse(reader.GetValue(6).ToString());
            processor.Producer = producerDataAccess.GetEntityById(Int32.Parse(reader.GetValue(7).ToString()));

            return processor;
        }
    }
}
