﻿using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.database.basic
{
    /// <summary>
    /// Basis Klasse für Datenbank-Verbindung
    /// </summary>
    public abstract class DatabaseBasic
    {
        /// <summary>
        /// Dient als Vorgabe für abgeleitete Klassen
        /// </summary>
        public virtual string getTableName()
        {
            return "TableName";
        }

        /// <summary>
        /// Baut eine Verbindung mit der Datenbank auf, basierend auf den Konfigurationen
        /// </summary>
        /// <returns>MySqlConnection</returns>
        public MySqlConnection CreateConnection()
        {
            ConfigProzesser config = new ConfigProzesser();
            string myConnectionString = "SERVER=" + config.getDBHost()
                                      + ";DATABASE=" + config.getDBName()
                                      + ";UID=" + config.getDBUser()
                                      + ";PASSWORD=" + config.getDBPassword() + ";";
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            return connection;
        }
        /*
           *   Liest den Datensatz der Entität 'Festplatte' aus der Datenbank, die der übergebenen ID
           *   entspricht
           */
           /*
        public virtual T GetEntityById<T>(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            T monitor = (T)this.MapToEntity(reader);
            connection.Close();

            return monitor;
        }

        protected abstract object MapToEntity(MySqlDataReader reader);*/
    }
}
