using System;
using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace InventoryManagementSystem.DB_Models
{
    /*
    *   Data-Access-Klasse der Entität 'Hersteller'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class ProducerDataAccess
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
        *   Speichert ein Objekt der Entität 'Hersteller' in die Datenbank
        */
        public void Save(Producer entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `ims_hersteller`(`Firma`, `Telefon`, `Email`, `Webseite`, `PLZ`, `Ort`, `Straße`, `Hausnummer`) "
                                + "VALUES ('" + entity.CompanyName + "'," + entity.PhoneNumber + ",'" + entity.Email + "',"
                                + "'" + entity.Website + "'," + entity.PostalCode + ",'" + entity.Place + "','" + entity.Street + "'," + entity.HouseNumber + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Löscht ein Objekt der Entität 'Hersteller' aus der Datenbank 
         */
        public void Delete(Producer entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `ims_hersteller` WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        *   Liest den Datensatz der Entität 'Hersteller' aus der Datenbank, die der übergebenen ID
        *   entspricht
        */
        public Producer GetEntityById(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_hersteller` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            
            reader.Read();
            Producer producer = this.MapToEntity(reader);

            connection.Close();

            return producer;
        }

        /*
        *   Liest den zuletzt gespeicherten Datensatz der Entität 'Hersteller' aus der Datenbank
        */
        public Producer GetLastEntity()
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT MAX(id) FROM `ims_hersteller`";

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
        *   Liest alle Datensätze der Entität 'Hersteller' aus der Datenbank
        */
        public List<Producer> GetAllEntities()
        {
            List<Producer> producers = new System.Collections.Generic.List<Producer>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_hersteller`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Producer producer = this.MapToEntity(reader);
                producers.Add(producer);
            }

            return producers;
        }

        /*
        *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Hersteller' (Producer)
        */
        private Producer MapToEntity(MySqlDataReader reader)
        {
            Producer producer = new Producer();

            producer.Id = Int32.Parse(reader.GetValue(0).ToString());
            producer.CompanyName = reader.GetValue(1).ToString();
            producer.PhoneNumber = Int32.Parse(reader.GetValue(2).ToString());
            producer.Email = reader.GetValue(3).ToString();
            producer.Website = reader.GetValue(4).ToString();
            producer.PostalCode = Int32.Parse(reader.GetValue(5).ToString());
            producer.Place = reader.GetValue(6).ToString();
            producer.Street = reader.GetValue(7).ToString();
            producer.HouseNumber = Int32.Parse(reader.GetValue(8).ToString());

            return producer;
        }
    }
}
