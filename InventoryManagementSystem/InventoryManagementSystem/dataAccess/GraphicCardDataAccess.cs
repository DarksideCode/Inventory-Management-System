using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.DB_Models
{
    /*
    *   Data-Access-Klasse der Entität 'Grafikkarte'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class GraphicCardDataAccess
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
        *   Speichert ein Objekt der Entität 'Grafikkarte' in die Datenbank
        */
        public void Save(GraphicCard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `ims_grafikkarte`(`Beschreibung`, `Taktrate`, "
                                + "`Modelbezeichnung`, `Grafikspeicher`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "'," + entity.ClockRate + ",'" + entity.Model + "',"
                                + entity.Memory + "," + entity.Producer.Id + ")";

            //TODO: Beziehung zu Schnittstellen in Datenbank speichern

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Löscht ein Objekt der Entität 'Grafikkarte' aus der Datenbank 
         */
        public void Delete(GraphicCard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `ims_grafikkarte` WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        *   Liest den Datensatz der Entität 'Grafikkarte' aus der Datenbank, die der übergebenen ID
        *   entspricht
        */
        public GraphicCard GetEntityById(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_grafikkarte` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            reader.Read();
            GraphicCard graphicCard = this.MapToEntity(reader);

            connection.Close();

            return graphicCard;
        }

        /*
        *   Liest den zuletzt gespeicherten Datensatz der Entität 'Grafikkarte' aus der Datenbank
        */
        public GraphicCard GetLastEntity()
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(id) FROM `ims_grafikkarte`";

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
        public List<GraphicCard> GetAllEntities()
        {
            List<GraphicCard> graphicCards = new List<GraphicCard>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_grafikkarte`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GraphicCard graphicCard = this.MapToEntity(reader);
                graphicCards.Add(graphicCard);
            }

            return graphicCards;
        }

        /*
        *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Grafikkarte' (GraphicCard)
        */
        private GraphicCard MapToEntity(MySqlDataReader reader)
        {
            GraphicCard graphicCard = new GraphicCard();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            graphicCard.Id = Int32.Parse(reader.GetValue(0).ToString());
            graphicCard.Description = reader.GetValue(1).ToString();
            graphicCard.ClockRate = Double.Parse(reader.GetValue(2).ToString());
            graphicCard.Model = reader.GetValue(3).ToString();
            graphicCard.Memory = Int32.Parse(reader.GetValue(4).ToString());
            graphicCard.Producer = producerDataAccess.GetEntityById(Int32.Parse(reader.GetValue(5).ToString()));

            //TODO: Schnittstellen aus der Datenbank lesen
            graphicCard.PhysicalInterfaces = null;

            return graphicCard;
        }
    }
}
