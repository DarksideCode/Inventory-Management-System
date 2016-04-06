using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /// <summary>
    /// Data-Access-Klasse der Entität 'Hauptplatine'.
    /// Führt alle Operationen für die Entität auf der Datenbank aus.
    /// </summary>
    public class MotherboardDataAccess : DatabaseBasic
    {
        /// <summary>
        /// Gibt den Tabellennamen zusammen mit dem konfigurierten Präfix zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "hauptplatine";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Hauptplatine' in der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
        public void Save(Motherboard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            
            command.CommandText = "INSERT INTO `" + this.getTableName() + "`(`Beschreibung`, `Zoll`, `Sockel`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "','" + entity.Inch + "','" + entity.Socket + "'," + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `" + this.getTableName() + "_schnittstelle`(`ID_Hauptplatine`, `ID_Schnittstelle`, `Anzahl`) "
                                    + "VALUES (" + this.GetLastEntity<Motherboard>().Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + ","
                                    + entity.PhysicalInterfaces[i].Number + ")";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Verändert einen bestehenden Datensatz der Entität 'Hauptplatine' in der Datenbank.
        /// Ermittelt auch nicht mehr genutzte Referenzen und löscht diese aus der Beziehungstabelle.
        /// </summary>
        /// <param name="entity">Die veränderte Entität</param>
        public void Update(Motherboard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlCommand interfaceCommand = connection.CreateCommand();
            string usedInterfaces = "";

            command.CommandText = "UPDATE `" + this.getTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Zoll`='" + entity.Inch + "', `Sockel`='" + entity.Socket 
                                + "', `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

            connection.Open();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                interfaceCommand.CommandText = "UPDATE `" + this.getTableName() + "_schnittstelle` SET `Anzahl`=" + entity.PhysicalInterfaces[i].Number
                                             + " WHERE `ID_Hauptplatine` = " + entity.Id + " AND `ID_Schnittstelle` = " + entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                interfaceCommand.ExecuteNonQuery();
                usedInterfaces += entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                if (i != entity.PhysicalInterfaces.Count - 1)
                {
                    usedInterfaces += ",";
                }
            }
            interfaceCommand.CommandText = "DELETE FROM `" + this.getTableName() + "_schnittstelle` WHERE `ID_Hauptplatine` = " + entity.Id
                                         + " AND `ID_Schnittstelle` NOT IN (" + usedInterfaces + ")";
            interfaceCommand.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Liest alle Datensätze der Entität 'Hauptplatine' aus der Datenbank
        /// </summary>
        /// <returns>Liste von Motherboard</returns>
        public List<Motherboard> GetAllEntities()
        {
            List<Motherboard> motherboards = new List<Motherboard>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Motherboard motherboard = (Motherboard) this.MapToEntity(reader);
                motherboards.Add(motherboard);
            }

            return motherboards;
        }

        /// <summary>
        /// Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Hauptplatine'
        /// </summary>
        /// <param name="reader">Der Datensatz, welcher gemappt wird</param>
        /// <returns>Motherboard</returns>
        protected override object MapToEntity(MySqlDataReader reader)
        {
            Motherboard motherboard = new Motherboard();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            motherboard.Id = Int32.Parse(reader.GetValue(0).ToString());
            motherboard.Description = reader.GetValue(1).ToString();
            motherboard.Inch = Double.Parse(reader.GetValue(2).ToString());
            motherboard.Socket = reader.GetValue(3).ToString();
            motherboard.Producer = producerDataAccess.GetEntityById<Producer>(Int32.Parse(reader.GetValue(4).ToString()));
            motherboard.PhysicalInterfaces = this.GetPhysicalInterfaces(motherboard);

            return motherboard;
        }

        /// <summary>
        /// Liest alle Beziehungen zu der Entität 'Schnittstelle' aus der Datenbank
        /// </summary>
        /// <param name="entity">Die Hauptplatine, zu welcher die Schnittstellen ermittelt werden</param>
        /// <returns>Liste von PhysicalInterfaceWithCount</returns>
        private List<PhysicalInterfaceWithCount> GetPhysicalInterfaces(Motherboard entity)
        {
            List<PhysicalInterfaceWithCount> physicalInterfaces = new List<PhysicalInterfaceWithCount>();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `" + this.getTableName() + "_schnittstelle` WHERE id_hauptplatine = " + entity.Id;

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
