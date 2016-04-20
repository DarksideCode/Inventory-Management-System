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
        public virtual string GetTableName()
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
            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "` WHERE id = " + id;

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
            command.CommandText = "SELECT MAX(id) FROM `" + this.GetTableName() + "`";

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

            command.CommandText = "DELETE FROM `" + this.GetTableName() + "` WHERE id = " + id;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        /// <summary>
        /// Liestet alle Datensätze der Entität aus der Datenbank
        /// </summary>
        /// <returns>Liste der Entität</returns>
        public List<T> GetAllEntities<T>()
        {
            List<T> entitys = new List<T>();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "`";

            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                T entity = (T)this.MapToEntity(reader);
                entitys.Add(entity);
            }

            return entitys;
        }

        /// <summary>
        /// Liest den Datensatz der jeweiligen Entität aus der Datenbank, die dem übergebenen Namen
        /// entspricht. Durch den generischen Ansatz muss der Typ der Entität übergeben werden.
        /// Das Feld indem geprüft werden soll muss mit übergeben werden.
        /// </summary>
        /// <typeparam name="T">Entitätsklasse</typeparam>
        /// <param name="fieldName">Feld in der Datenbank</param>
        /// <param name="name">Gesuchter Datensatz</param>
        /// <returns>Objekt der jeweiligen Entität</returns>
        public virtual T GetEntityByName<T>(string fieldName, string name)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "` WHERE "+ fieldName +" = '" + name + "';";
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            T entity = (T)this.MapToEntity(reader);
            connection.Close();

            return entity;
        }
    }
}
