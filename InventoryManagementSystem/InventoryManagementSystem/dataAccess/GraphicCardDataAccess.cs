using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /// <summary>
    /// Data-Access-Klasse der Entität 'Grafikkarte'.
    /// Führt alle Operationen für die Entität auf der Datenbank aus.
    /// </summary>
    public class GraphicCardDataAccess : DatabaseBasic
    {
        /// <summary>
        /// Gibt den Tabellennamen zusammen mit dem konfigurierten Präfix zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "grafikkarte";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Grafikkarte' in die Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
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
                                    + "VALUES (" + this.GetLastEntity<GraphicCard>().Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + ","
                                    + entity.PhysicalInterfaces[i].Number + ")";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Löscht ein Objekt der Entität 'Grafikkarte aus der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gelöscht wird</param>
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

        /// <summary>
        /// Liest alle Datensätze der Entität 'Grafikkarte' aus der Datenbank
        /// </summary>
        /// <returns>Liste von GraphicCard</returns>
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
                GraphicCard graphicCard = (GraphicCard) this.MapToEntity(reader);
                graphicCards.Add(graphicCard);
            }

            return graphicCards;
        }

        /// <summary>
        /// Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Grafikkarte'
        /// </summary>
        /// <param name="reader">Der Datensatz, welcher gemappt wird</param>
        /// <returns>GraphicCard</returns>
        protected override object MapToEntity(MySqlDataReader reader)
        {
            GraphicCard graphicCard = new GraphicCard();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            graphicCard.Id = Int32.Parse(reader.GetValue(0).ToString());
            graphicCard.Description = reader.GetValue(1).ToString();
            graphicCard.ClockRate = Double.Parse(reader.GetValue(2).ToString());
            graphicCard.Model = reader.GetValue(3).ToString();
            graphicCard.Memory = Int32.Parse(reader.GetValue(4).ToString());
            graphicCard.Producer = producerDataAccess.GetEntityById<Producer>(Int32.Parse(reader.GetValue(5).ToString()));
            graphicCard.PhysicalInterfaces = this.GetPhysicalInterfaces(graphicCard);

            return graphicCard;
        }

        /// <summary>
        /// Liest alle Beziehungen zu der Entität 'Schnittstelle' aus der Datenbank
        /// </summary>
        /// <param name="entity">Die Grafikkarte, zu welcher die Schnittstellen ermittelt werden</param>
        /// <returns>Liste von PhysicalInterfaceWithCount</returns>
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
                PhysicalInterface physicalInterface = physicalInterfaceDataAccess.GetEntityById<PhysicalInterface>(Int32.Parse(reader.GetValue(1).ToString()));
                int count = Int32.Parse(reader.GetValue(2).ToString());
                physicalInterfaces.Add(new PhysicalInterfaceWithCount(physicalInterface, count));
            }

            return physicalInterfaces;
        }
    }
}
