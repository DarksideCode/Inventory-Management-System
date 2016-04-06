using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.dataAccess
{
    /// <summary>
    /// Data-Access-Klasse der Entität 'Schnittstelle'.
    /// Führt alle Operationen für die Entität auf der Datenbank aus.
    /// </summary>
    public class PhysicalInterfaceDataAccess : DatabaseBasic
    {
        /// <summary>
        /// Gibt den Tabellennamen zusammen mit dem konfigurierten Präfix zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string getTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "schnittstelle";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Schnittstelle' in der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
        public void Save(PhysicalInterface entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `" + this.getTableName() + "`(`Name`, `Beschreibung`, `Seriell`, `Übertragungsrate`) "
                                + "VALUES ('" + entity.Name + "','" + entity.Description + "','" + entity.Serial + "',"
                                + entity.TransferRate + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Verändert einen bestehenden Datensatz der Entität 'Schnittstelle' in der Datenbank.
        /// </summary>
        /// <param name="entity">Die veränderte Entität</param>
        public void Update(PhysicalInterface entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE `" + this.getTableName() + "` SET `Name`='" + entity.Name + "', `Beschreibung`='" + entity.Description + "', `Seriell`='" + entity.Serial
                                + "', `Übertragungsrate`=" + entity.TransferRate + " WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Liest alle Datensätze der Entität 'Schnittstelle' aus der Datenbank
        /// </summary>
        /// <returns>Liste von PhysicalInterface</returns>
        public List<PhysicalInterface> GetAllEntities()
        {
            List<PhysicalInterface> physicalInterfaces = new List<PhysicalInterface>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhysicalInterface physicalInterface = (PhysicalInterface) this.MapToEntity(reader);
                physicalInterfaces.Add(physicalInterface);
            }

            return physicalInterfaces;
        }

        /// <summary>
        /// Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Schnittstelle'
        /// </summary>
        /// <param name="reader">Der Datensatz, welcher gemappt wird</param>
        /// <returns>PhysicalInterface</returns>
        protected override object MapToEntity(MySqlDataReader reader)
        {
            PhysicalInterface physicalInterface = new PhysicalInterface();

            physicalInterface.Id = Int32.Parse(reader.GetValue(0).ToString());
            physicalInterface.Name = reader.GetValue(1).ToString();
            physicalInterface.Description = reader.GetValue(2).ToString();
            physicalInterface.Serial = Boolean.Parse(reader.GetValue(3).ToString());
            physicalInterface.TransferRate = Int32.Parse(reader.GetValue(4).ToString());

            return physicalInterface;
        }
    }
}
