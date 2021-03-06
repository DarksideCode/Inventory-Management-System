﻿using InventoryManagementSystem.components;
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
        public override string GetTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.DBPraefix + "hauptplatine";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Hauptplatine' in der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
        public void Save(Motherboard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            
            command.CommandText = "INSERT INTO `" + this.GetTableName() + "`(`Beschreibung`, `Zoll`, `Sockel`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "','" + entity.Inch.ToString().Replace(',','.') + "','" + entity.Socket + "'," + entity.Producer.Id + ")";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `" + this.GetTableName() + "_schnittstelle`(`ID_Hauptplatine`, `ID_Schnittstelle`, `Anzahl`) "
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

            command.CommandText = "UPDATE `" + this.GetTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Zoll`='" + entity.Inch.ToString().Replace(',','.') + "', `Sockel`='" + entity.Socket 
                                + "', `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

            connection.Open();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                if (this.IsInterfaceInUse(entity.PhysicalInterfaces[i].PhysicalInterface, entity)) {
                    interfaceCommand.CommandText = "UPDATE `" + this.GetTableName() + "_schnittstelle` SET `Anzahl`=" + entity.PhysicalInterfaces[i].Number
                                             + " WHERE `ID_Hauptplatine` = " + entity.Id + " AND `ID_Schnittstelle` = " + entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                } else {
                    interfaceCommand.CommandText = "INSERT INTO `" + this.GetTableName() + "_schnittstelle`(`ID_Hauptplatine`, `ID_Schnittstelle`, `Anzahl`) "
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
                interfaceCommand.CommandText = "DELETE FROM `" + this.GetTableName() + "_schnittstelle` WHERE `ID_Hauptplatine` = " + entity.Id
                                             + " AND `ID_Schnittstelle` NOT IN (" + usedInterfaces + ")";
            } 
            else
            {
                interfaceCommand.CommandText = "DELETE FROM `" + this.GetTableName() + "_schnittstelle` WHERE `ID_Hauptplatine` = " + entity.Id;
            }

            interfaceCommand.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
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

            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "_schnittstelle` WHERE id_hauptplatine = " + entity.Id;

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
        private bool IsInterfaceInUse(PhysicalInterface physicalInterface, Motherboard entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            bool result;

            connection.Open();
            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "_schnittstelle` WHERE `ID_Hauptplatine` = " + entity.Id
                                + " AND `ID_Schnittstelle` = " + physicalInterface.Id;
            result = command.ExecuteReader().HasRows;
            connection.Close();

            return result;
        }
    }
}