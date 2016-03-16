﻿using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;

namespace InventoryManagementSystem.DB_Models
{
   /*
    *   Data-Access-Klasse der Entität 'Hauptplatine'
    *   Führt alle Operationen für die Entität auf der Datenbank aus.
    */
    public class MotherboardDataAccess : DatabaseBasic
    {
        /*
        *   Speichert ein Objekt der Entität 'Hauptplatine' in die Datenbank
        */
        public void Save(Motherboard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            
            command.CommandText = "INSERT INTO `ims_hauptplatine`(`Beschreibung`, `Zoll`, `Sockel`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "','" + entity.Inch + "','" + entity.Socket + "'," + entity.Producer.Id + ")";

            //TODO: Beziehung zu Schnittstellen in Datenbank speichern

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `ims_hauptplatine_schnittstelle`(`ID_Hauptplatine`, `ID_Schnittstelle`, `Anzahl`) "
                                    + "VALUES (" + this.GetLastEntity().Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + ","
                                    + entity.PhysicalInterfaces[i].Number + ")";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /*
        *  Löscht ein Objekt der Entität 'Hauptplatine' aus der Datenbank 
        *  */
        public void Delete(Motherboard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            
            command.CommandText = "DELETE FROM `ims_hauptplatine` WHERE id = " + entity.Id;
            
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
            command.CommandText = "SELECT * FROM `ims_hauptplatine` WHERE id = " + id;

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
            command.CommandText = "SELECT MAX(id) FROM `ims_hauptplatine`";

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
        *   Liest alle Datensätze der Entität 'Hauptplatine' aus der Datenbank
        */
        public List<Motherboard> GetAllEntities()
        {
            List<Motherboard> motherboards = new List<Motherboard>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `ims_hauptplatine`";

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
            motherboard.PhysicalInterfaces = this.GetPhysicalInterfaces(motherboard);

            return motherboard;
        }

        /*
        *  Liest alle Beziehungen zu der Entität 'Schnittstelle' aus der Datenbank
        */
        private List<PhysicalInterfaceWithCount> GetPhysicalInterfaces(Motherboard entity)
        {
            List<PhysicalInterfaceWithCount> physicalInterfaces = new List<PhysicalInterfaceWithCount>();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `ims_hauptplatine_schnittstelle` WHERE id_hauptplatine = " + entity.Id;

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
