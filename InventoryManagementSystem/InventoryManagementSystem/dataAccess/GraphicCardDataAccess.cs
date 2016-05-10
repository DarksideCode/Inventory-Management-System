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
        public override string GetTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.DBPraefix + "grafikkarte";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Grafikkarte' in die Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
        public void Save(GraphicCard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `" + this.GetTableName() + "`(`Beschreibung`, `Taktrate`, "
                                + "`Modelbezeichnung`, `Grafikspeicher`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "'," + entity.ClockRate.ToString().Replace(',','.') + ",'" + entity.Model + "',"
                                + entity.Memory + "," + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `" + this.GetTableName() + "_schnittstelle`(`ID_Grafikkarte`, `ID_Schnittstelle`, `Anzahl`) "
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

            command.CommandText = "UPDATE `" + this.GetTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Taktrate`='" + entity.ClockRate.ToString().Replace(',','.') + "', "
                                + "`Modelbezeichnung`='" + entity.Model + "', `Grafikspeicher`=" + entity.Memory + ", `ID_Hersteller`=" + entity.Producer.Id 
                                + " WHERE id = " + entity.Id;

            connection.Open();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                if (this.IsInterfaceInUse(entity.PhysicalInterfaces[i].PhysicalInterface, entity)) {
                    interfaceCommand.CommandText = "UPDATE `" + this.GetTableName() + "_schnittstelle` SET `Anzahl`=" + entity.PhysicalInterfaces[i].Number
                                             + " WHERE `ID_Grafikkarte` = " + entity.Id + " AND `ID_Schnittstelle` = " + entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                } else {
                    interfaceCommand.CommandText = "INSERT INTO `" + this.GetTableName() + "_schnittstelle`(`ID_Grafikkarte`, `ID_Schnittstelle`, `Anzahl`) "
                                                 + "VALUES (" + entity.Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + ","
                                                 + entity.PhysicalInterfaces[i].Number + ")";
                }

                interfaceCommand.ExecuteNonQuery();
                usedInterfaces += entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                if (i != entity.PhysicalInterfaces.Count - 1)
                {
                    usedInterfaces += ",";
                }
            }
            if (usedInterfaces.Length > 0)
            {
                interfaceCommand.CommandText = "DELETE FROM `" + this.GetTableName() + "_schnittstelle` WHERE `ID_Grafikkarte` = " + entity.Id
                                             + " AND `ID_Schnittstelle` NOT IN (" + usedInterfaces + ")";
            }
            else
            {
                interfaceCommand.CommandText = "DELETE FROM `" + this.GetTableName() + "_schnittstelle` WHERE `ID_Grafikkarte` = " + entity.Id;
            }

            interfaceCommand.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
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
            graphicCard.Memory = ulong.Parse(reader.GetValue(4).ToString());
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

            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "_schnittstelle` WHERE id_grafikkarte = " + entity.Id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PhysicalInterface physicalInterface = physicalInterfaceDataAccess.GetEntityById<PhysicalInterface>(Int32.Parse(reader.GetValue(1).ToString()));
                uint count = uint.Parse(reader.GetValue(2).ToString());
                physicalInterfaces.Add(new PhysicalInterfaceWithCount(physicalInterface, count));
            }

            return physicalInterfaces;
        }

        /// <summary>
        /// Prüft, ob ein Interface bereits der Entität zugewiesen ist
        /// </summary>
        /// <param name="physicalInterface">Die Schnittstelle auf welche geprüft wird</param>
        /// <param name="entity">Die Entität, welche geprüft wird</param>
        /// <returns>true, wenn eine Schnittstelle zugewiesen wurde</returns>
        private bool IsInterfaceInUse(PhysicalInterface physicalInterface, GraphicCard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            bool result;

            connection.Open();
            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "_schnittstelle` WHERE `ID_Grafikkarte` = " + entity.Id
                                + " AND `ID_Schnittstelle` = " + physicalInterface.Id;
            result = command.ExecuteReader().HasRows;
            connection.Close();

            return result;
        }
    }
}
