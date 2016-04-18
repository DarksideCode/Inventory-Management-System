using System;
using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /// <summary>
    /// Data-Access-Klasse der Entität 'Hersteller'.
    /// Führt alle Operationen für die Entität auf der Datenbank aus.
    /// </summary>
    public class ProducerDataAccess : DatabaseBasic
    {
        /// <summary>
        /// Gibt den Tabellennamen zusammen mit dem konfigurierten Präfix zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string GetTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "hersteller";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Hersteller' in der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
        public void Save(Producer entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `" + this.GetTableName() + "`(`Firma`, `Telefon`, `Email`, `Webseite`, `PLZ`, `Ort`, `Straße`, `Hausnummer`) "
                                + "VALUES ('" + entity.CompanyName + "'," + entity.PhoneNumber + ",'" + entity.Email + "',"
                                + "'" + entity.Website + "'," + entity.PostalCode + ",'" + entity.Place + "','" + entity.Street + "'," + entity.HouseNumber + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Verändert einen bestehenden Datensatz der Entität 'Hersteller' in der Datenbank.
        /// Ermittelt auch nicht mehr genutzte Referenzen und löscht diese aus der Beziehungstabelle.
        /// </summary>
        /// <param name="entity">Die veränderte Entität</param>
        public void Update(Producer entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE `" + this.GetTableName() + "` SET `Firma`='" + entity.CompanyName + "', `Telefon`=" + entity.PhoneNumber + ", `Email`='" + entity.Email 
                                + "', `Webseite`='" + entity.Website + "', `PLZ`=" + entity.PostalCode + ", `Ort`='" + entity.Place + "', `Straße`='" + entity.Street 
                                + "', `Hausnummer`=" + entity.HouseNumber + " WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Hersteller'
        /// </summary>
        /// <param name="reader">Der Datensatz, welcher gemappt wird</param>
        /// <returns>Producer</returns>
        protected override object MapToEntity(MySqlDataReader reader)
        {
            Producer producer = new Producer();

            producer.Id = Int32.Parse(reader.GetValue(0).ToString());
            producer.CompanyName = reader.GetValue(1).ToString();
            producer.PhoneNumber = uint.Parse(reader.GetValue(2).ToString());
            producer.Email = reader.GetValue(3).ToString();
            producer.Website = reader.GetValue(4).ToString();
            producer.PostalCode = uint.Parse(reader.GetValue(5).ToString());
            producer.Place = reader.GetValue(6).ToString();
            producer.Street = reader.GetValue(7).ToString();
            producer.HouseNumber = uint.Parse(reader.GetValue(8).ToString());

            return producer;
        }
    }
}
