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
        public override string GetTableName()
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

            command.CommandText = "INSERT INTO `" + this.GetTableName() + "`(`Name`, `Beschreibung`, `Seriell`, `Übertragungsrate`) "
                                + "VALUES ('" + entity.Name + "','" + entity.Description + "','" + entity.Serial + "',"
                                + entity.TransferRate.ToString().Replace(',','.') + ")";

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

            command.CommandText = "UPDATE `" + this.GetTableName() + "` SET `Name`='" + entity.Name + "', `Beschreibung`='" + entity.Description + "', `Seriell`='" + entity.Serial
                                + "', `Übertragungsrate`=" + entity.TransferRate.ToString().Replace(',','.') + " WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Selektiert einen Datensatz aus der Datenbank, welcher mit dem übergebenen Namen übereinstimmt.
        /// </summary>
        /// <param name="name">Name der Schnittstelle</param>
        /// <returns>Ein Objekt der Entität mit dem übergebenen Namen</returns>
        public PhysicalInterface GetByName(string name)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `ims_schnittstelle` WHERE `Name` = '" + name + "'";

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            return (PhysicalInterface)this.MapToEntity(reader);
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
            physicalInterface.TransferRate = double.Parse(reader.GetValue(4).ToString());

            return physicalInterface;
        }
    }
}
