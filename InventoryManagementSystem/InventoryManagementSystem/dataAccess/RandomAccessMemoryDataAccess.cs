using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /// <summary>
    /// Data-Access-Klasse der Entität 'Arbeitsspeicher'.
    /// Führt alle Operationen für die Entität auf der Datenbank aus.
    /// </summary>
    public class RandomAccessMemoryDataAccess : DatabaseBasic
    {
        /// <summary>
        /// Gibt den Tabellennamen zusammen mit dem konfigurierten Präfix zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "arbeitsspeicher";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Arbeitsspeicher' in der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
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

        /// <summary>
        /// Verändert einen bestehenden Datensatz der Entität 'Arbeitsspeicher' in der Datenbank.
        /// </summary>
        /// <param name="entity">Die veränderte Entität</param>
        public void Update(RandomAccessMemory entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE `"+ this.getTableName() +"` SET `Beschreibung`='" + entity.Description + "', `Speicher`=" + entity.Memory + ", `Taktrate`='" + entity.ClockRate 
                                + "', `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Liest alle Datensätze der Entität 'Arbeitsspeicher' aus der Datenbank
        /// </summary>
        /// <returns>Liste von RandomAccessMemory</returns>
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
                RandomAccessMemory ram = (RandomAccessMemory) this.MapToEntity(reader);
                rams.Add(ram);
            }

            return rams;
        }

        /// <summary>
        /// Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Arbeitsspeicher'
        /// </summary>
        /// <param name="reader">Der Datensatz, welcher gemappt wird</param>
        /// <returns>RandomAccessMemory</returns>
        protected override object MapToEntity(MySqlDataReader reader)
        {
            RandomAccessMemory ram = new RandomAccessMemory();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            ram.Id = Int32.Parse(reader.GetValue(0).ToString());
            ram.Description = reader.GetValue(1).ToString();
            ram.Memory = Int32.Parse(reader.GetValue(2).ToString());
            ram.ClockRate = Double.Parse(reader.GetValue(3).ToString());
            ram.Producer = producerDataAccess.GetEntityById<Producer>(Int32.Parse(reader.GetValue(4).ToString()));

            return ram;
        }
    }
}