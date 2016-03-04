using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;

namespace InventoryManagementSystem.DB_Models
{
    public class ProducerDataAccess
    {
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

        public Producer GetEntityById(int id)
        {
            Producer producer = new Producer();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_hersteller` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            
            reader.Read();
            producer.Id = Int32.Parse(reader.GetValue(0).ToString());
            producer.CompanyName = reader.GetValue(1).ToString();
            producer.PhoneNumber = Int32.Parse(reader.GetValue(2).ToString());
            producer.Email = reader.GetValue(3).ToString();   
            producer.Website = reader.GetValue(4).ToString();
            producer.PostalCode = Int32.Parse(reader.GetValue(5).ToString());
            producer.Place = reader.GetValue(6).ToString();
            producer.Street = reader.GetValue(7).ToString();
            producer.HouseNumber = Int32.Parse(reader.GetValue(8).ToString());
            
            connection.Close();

            return producer;
        }
    }
}
