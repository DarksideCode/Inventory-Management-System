using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /// <summary>
    /// Data-Access-Klasse der Entität 'Prozessor'.
    /// Führt alle Operationen für die Entität auf der Datenbank aus.
    /// </summary>
    public class ProcessorDataAccess : DatabaseBasic
    {
        /// <summary>
        /// Gibt den Tabellennamen zusammen mit dem konfigurierten Präfix zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "prozessor";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Prozessor' in der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
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

        /// <summary>
        /// Verändert einen bestehenden Datensatz der Entität 'Prozessor' in der Datenbank.
        /// </summary>
        /// <param name="entity">Die veränderte Entität</param>
        public void Update(Processor entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE `" + this.getTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Modell`='" + entity.Model + "', `Kerne`=" + entity.Core 
                                + ", `Befehlssatz`='" + entity.CommandSet + "', `Architektur`=" + entity.Architecture + ", `Taktrate`='" + entity.ClockRate 
                                + "', `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Prozessor'
        /// </summary>
        /// <param name="reader">Der Datensatz, welcher gemappt wird</param>
        /// <returns>Processor</returns>
        protected override object MapToEntity(MySqlDataReader reader)
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
            processor.Producer = producerDataAccess.GetEntityById<Producer>(Int32.Parse(reader.GetValue(7).ToString()));

            return processor;
        }
    }
}
