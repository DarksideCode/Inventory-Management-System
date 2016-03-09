using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.DB_Models
{
   /*
    *   Data-Access-Klasse der Entität 'Hauptplatine'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class MotherboardDataAccess
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
            *   Speichert ein Objekt der Entität 'Hauptplatine' in die Datenbank
            */
            public void Save(Motherboard entity)
            {
                MySqlConnection connection = this.CreateConnection();
                MySqlCommand command = connection.CreateCommand();

                //command.CommandText = "INSERT INTO `ims_hauptplantine`(`Beschreibung`, `Zoll`, `Sockel`, "
                //                    + "`ID_Hersteller`) VALUES ('" + entity.Description + "'," + entity.Inch + ",'"
                //                    + entity.Socket + "'," + entity.Producer.Id + ")";
                command.CommandText = "INSERT INTO `ims_hauptplantine`(`Beschreibung`, `Zoll`, `Sockel`, `ID_Hersteller`) "
                                    + "VALUES ('" + entity.Description + "','" + entity.Inch + "','" + entity.Socket + "'," + entity.Producer.Id + ")";

                //TODO: Beziehung zu Schnittstellen in Datenbank speichern

            connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            /*
            *   Liest den Datensatz der Entität 'Hauptplatine' aus der Datenbank, die der übergebenen ID
            *   entspricht
            */
            public Motherboard GetEntityById(int id)
            {
                MySqlConnection connection = this.CreateConnection();
                MySqlCommand command = connection.CreateCommand();
                //TODO: DB-Tabelle in 'hauptplatine' umbenennen
                command.CommandText = "SELECT * FROM `ims_hauptplantine` WHERE id = " + id;

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                reader.Read();
                Motherboard motherboard = this.MapToEntity(reader);

                connection.Close();

                return motherboard;
            }

            /*
            *   Liest den zuletzt gespeicherten Datensatz der Entität 'Hauptplatine' aus der Datenbank
            */
            public Motherboard GetLastEntity()
            {
                MySqlConnection connection = this.CreateConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(id) FROM `ims_hauptplantine`";

                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int id = Int32.Parse(reader.GetValue(0).ToString());

                connection.Close();

                return this.GetEntityById(id);
            }

            /*
            *   Liest alle Datensätze der Entität 'Hauptplatine' aus der Datenbank
            */
            public List<Motherboard> GetAllEntities()
            {
                List<Motherboard> motherboards = new List<Motherboard>();
                MySqlConnection connection = this.CreateConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM `ims_arbeitsspeicher`";

                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Motherboard motherboard = this.MapToEntity(reader);
                    motherboards.Add(motherboard);
                }

                return motherboards;
            }

            /*
            *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Hauptplatine' (Motherboard)
            */
            private Motherboard MapToEntity(MySqlDataReader reader)
            {
                Motherboard motherboard = new Motherboard();
                ProducerDataAccess producerDataAccess = new ProducerDataAccess();

                motherboard.Id = Int32.Parse(reader.GetValue(0).ToString());
                motherboard.Description = reader.GetValue(1).ToString();
                motherboard.Inch = Double.Parse(reader.GetValue(2).ToString());
                motherboard.Socket = reader.GetValue(3).ToString();
                motherboard.Producer = producerDataAccess.GetEntityById(Int32.Parse(reader.GetValue(4).ToString()));

                //TODO: Schnittstellen aus der Datenbank lesen.

                return motherboard;
            }
        }
}
