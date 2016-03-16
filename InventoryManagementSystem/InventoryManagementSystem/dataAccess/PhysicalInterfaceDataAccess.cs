using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.DB_Models
{
    /*
    *   Data-Access-Klasse der Entität 'Schnittstelle'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class PhysicalInterfaceDataAccess : DatabasteBasic
    {
        /*
         *   Speichert ein Objekt der Entität 'Schnittstelle' in die Datenbank
         */
        public void Save(PhysicalInterface entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `ims_schnittstelle`(`Name`, `Beschreibung`, `Seriell`, `Übertragungsrate`) "
                                + "VALUES ('" + entity.Name + "','" + entity.Description + "','" + entity.Serial + "',"
                                + entity.TransferRate + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
         *  Löscht ein Objekt der Entität 'Schnittstelle' aus der Datenbank 
         */
        public void Delete(PhysicalInterface entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `ims_schnittstelle` WHERE id = " + entity.Id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        *   Liest den Datensatz der Entität 'Schnittstelle' aus der Datenbank, die der übergebenen ID
        *   entspricht
        */
        public PhysicalInterface GetEntityById(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_schnittstelle` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            reader.Read();
            PhysicalInterface physicalInterface = this.MapToEntity(reader);

            connection.Close();

            return physicalInterface;
        }

        /*
        *   Liest den zuletzt gespeicherten Datensatz der Entität 'Schnittstelle' aus der Datenbank
        */
        public PhysicalInterface GetLastEntity()
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(id) FROM `ims_schnittstelle`";

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
        *   Liest alle Datensätze der Entität 'Schnittstelle' aus der Datenbank
        */
        public List<PhysicalInterface> GetAllEntities()
        {
            List<PhysicalInterface> physicalInterfaces = new List<PhysicalInterface>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_schnittstelle`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                PhysicalInterface physicalInterface = this.MapToEntity(reader);
                physicalInterfaces.Add(physicalInterface);
            }

            return physicalInterfaces;
        }

        /*
        *   Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Schnittstelle' (PhysicalInterface)
        */
        private PhysicalInterface MapToEntity(MySqlDataReader reader)
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
