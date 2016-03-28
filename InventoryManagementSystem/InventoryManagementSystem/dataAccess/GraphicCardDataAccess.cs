using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /*
    *   Data-Access-Klasse der Entität 'Grafikkarte'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class GraphicCardDataAccess : DatabaseBasic
    {
        /**
        * gibt den Tabellen Namen zurück.
        **/
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "grafikkarte";
        }

        /*
        *   Speichert ein Objekt der Entität 'Grafikkarte' in die Datenbank
        */
        public void Save(GraphicCard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `" + this.getTableName() + "`(`Beschreibung`, `Taktrate`, "
                                + "`Modelbezeichnung`, `Grafikspeicher`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "'," + entity.ClockRate + ",'" + entity.Model + "',"
                                + entity.Memory + "," + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `" + this.getTableName() + "_schnittstelle`(`ID_Grafikkarte`, `ID_Schnittstelle`, `Anzahl`) "
                                    + "VALUES (" + this.GetLastEntity().Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + ","
                                    + entity.PhysicalInterfaces[i].Number + ")";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /*
         *  Löscht ein Objekt der Entität 'Grafikkarte' aus der Datenbank 
         */
        public void Delete(GraphicCard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            
            command.CommandText = "DELETE FROM `" + this.getTableName() + "` WHERE id = " + entity.Id;
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Verändert einen bestehenden Datensatz der Entität `Grafikkarte` in der Datenbank
         *  Ermittelt auch nicht mehr genutzte Referenzen und löscht diese.
         */
        public void Update(GraphicCard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlCommand interfaceCommand = connection.CreateCommand();
            string usedInterfaces = "";

            command.CommandText = "UPDATE `" + this.getTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Taktrate`='" + entity.ClockRate + "', "
                                + "`Modelbezeichnung`='" + entity.Model + "', `Grafikspeicher`=" + entity.Memory + ", `ID_Hersteller`=" + entity.Producer.Id 
                                + " WHERE id = " + entity.Id;

            connection.Open();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                interfaceCommand.CommandText = "UPDATE `" + this.getTableName() + "_schnittstelle` SET `Anzahl`=" + entity.PhysicalInterfaces[i].Number
                                             + " WHERE `ID_Grafikkarte` = " + entity.Id + " AND `ID_Schnittstelle` = " + entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                interfaceCommand.ExecuteNonQuery();
                usedInterfaces += entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                if (i != entity.PhysicalInterfaces.Count - 1)
                {
                    usedInterfaces += ",";
                }
            }
            interfaceCommand.CommandText = "DELETE FROM `" + this.getTableName() + "_schnittstelle` WHERE `ID_Grafikkarte` = " + entity.Id
                                         + " AND `ID_Schnittstelle` NOT IN (" + usedInterfaces + ")";
            interfaceCommand.ExecuteNonQuery();
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
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "` WHERE id = " + id;

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
        public List<GraphicCard> GetAllEntities()
        {
            List<GraphicCard> graphicCards = new List<GraphicCard>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "`";

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
            graphicCard.PhysicalInterfaces = this.GetPhysicalInterfaces(graphicCard);

            return graphicCard;
        }

        /*
         *  Liest alle Beziehungen zu der Entität 'Schnittstelle' aus der Datenbank
         */
        private List<PhysicalInterfaceWithCount> GetPhysicalInterfaces(GraphicCard entity)
        {
            List<PhysicalInterfaceWithCount> physicalInterfaces = new List<PhysicalInterfaceWithCount>();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `" + this.getTableName() + "_schnittstelle` WHERE id_grafikkarte = " + entity.Id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PhysicalInterface physicalInterface = physicalInterfaceDataAccess.GetEntityById(Int32.Parse(reader.GetValue(1).ToString()));
                int count = Int32.Parse(reader.GetValue(2).ToString());
                physicalInterfaces.Add(new PhysicalInterfaceWithCount(physicalInterface, count));
            }

            return physicalInterfaces;
        }
    }
}
