using InventoryManagementSystem.components;
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
        /// <returns>String Tablename</returns>
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

        /// <summary>
        /// Liest den Datensatz der jeweiligen Entität aus der Datenbank, die der übergebenen ID
        /// entspricht. Durch den generischen Ansatz muss der Typ der Entität übergeben werden.
        /// </summary>
        /// <typeparam name="T">Entitätsklasse</typeparam>
        /// <param name="id">ID des Datensatzes</param>
        /// <returns>Objekt der jeweiligen Entität</returns>
        public virtual T GetEntityById<T>(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.getTableName() + "` WHERE id = " + id;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            T entity = (T)this.MapToEntity(reader);
            connection.Close();

            return entity;
        }

        protected abstract object MapToEntity(MySqlDataReader reader);

        /// <summary>
        /// Liest den zuletzt gespeicherten Datensatz einer Entität aus der Datenbank
        /// </summary>
        /// <returns>Entität</returns>
        public virtual T GetLastEntity<T>()
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(id) FROM `" + this.getTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            //Prüft, ob eine ID zurück gegeben wurde, falls nicht ist die Tabelle leer und es wird null zurück gegeben
            if (reader.GetValue(0).ToString().Length > 0)
            {
                int id = Int32.Parse(reader.GetValue(0).ToString());
                connection.Close();
                return this.GetEntityById<T>(id);
            }
            else {
                connection.Close();
                return default(T);
            }
        }

        /// <summary>
        /// Löscht ein Objekt der Entität aus der Datenbank
        /// </summary>
        /// <param name="id">Die ID des Objekts, welches gelöscht wird</param>
        public virtual void Delete(int id)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM `" + this.getTableName() + "` WHERE id = " + id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
