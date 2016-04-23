using InventoryManagementSystem.components;
using InventoryManagementSystem.utilitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using InventoryManagementSystem.database.basic;
using InventoryManagementSystem.control;

namespace InventoryManagementSystem.dataAccess
{
    /// <summary>
    /// Data-Access-Klasse der Entität 'Festplatte'.
    /// Führt alle Operationen für die Entität auf der Datenbank aus.
    /// </summary>
    public class DiskDataAccess : DatabaseBasic
    {

        /// <summary>
        /// Gibt den Tabellennamen zusammen mit dem konfigurierten Präfix zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string GetTableName()
        {
            ConfigProzesser config = new ConfigProzesser();
            return config.getDBPraefix() + "festplatte";
        }

        /// <summary>
        /// Speichert ein Objekt der Entität 'Festplatte' in der Datenbank
        /// </summary>
        /// <param name="entity">Das Objekt, welches gespeichert wird</param>
        public void Save(Disk entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO `" + this.GetTableName() + "`(`Beschreibung`, `Kapazität`, `SSD`, `Zoll`, `ID_Hersteller`) "
                                + "VALUES ('" + entity.Description + "'," + entity.Capacity + ",'" + Convert.ToInt32(entity.Ssd) + "','" + entity.Inch.ToString().Replace(',','.') + "',"
                                + entity.Producer.Id + ")";
            Console.WriteLine(command.CommandText);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                command.CommandText = "INSERT INTO `" + this.GetTableName() + "`(`ID_Festplatte`, `ID_Schnittstelle`, `Anzahl`) VALUES "
                                    + "(" + this.GetLastEntity<Disk>().Id + "," + entity.PhysicalInterfaces[i].PhysicalInterface.Id + "," + entity.PhysicalInterfaces[i].Number + ")";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Verändert einen bestehenden Datensatz der Entität 'Festplatte' in der Datenbank.
        /// Ermittelt auch nicht mehr genutzte Referenzen und löscht diese aus der Beziehungstabelle.
        /// </summary>
        /// <param name="entity">Die veränderte Entität</param>
        public void Update(Disk entity)
        {
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlCommand interfaceCommand = connection.CreateCommand();
            string usedInterfaces = "";

            command.CommandText = "UPDATE `" + this.GetTableName() + "` SET `Beschreibung`='" + entity.Description + "', `Kapazität`=" + entity.Capacity + ", `SSD`='" + Convert.ToInt32(entity.Ssd) 
                                + "', `Zoll`='" + entity.Inch.ToString().Replace(',','.') + "', `ID_Hersteller`=" + entity.Producer.Id + " WHERE id = " + entity.Id;

            connection.Open();

            for (int i = 0; i < entity.PhysicalInterfaces.Count; i++)
            {
                interfaceCommand.CommandText = "UPDATE `" + this.GetTableName() + "` SET `Anzahl`=" + entity.PhysicalInterfaces[i].Number
                                             + " WHERE `ID_Festplatte` = " + entity.Id + " AND `ID_Schnittstelle` = " + entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                interfaceCommand.ExecuteNonQuery();
                usedInterfaces += entity.PhysicalInterfaces[i].PhysicalInterface.Id;
                if (i != entity.PhysicalInterfaces.Count - 1)
                {
                    usedInterfaces += ",";
                }
            }
            interfaceCommand.CommandText = "DELETE FROM `" + this.GetTableName() + "` WHERE `ID_Festplatte` = " + entity.Id
                                         + " AND `ID_Schnittstelle` NOT IN (" + usedInterfaces + ")";
            interfaceCommand.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Mappt einen Datensatz aus der Datenbank auf ein Objekt vom Typ 'Festplatte'
        /// </summary>
        /// <param name="reader">Der Datensatz, welcher gemappt wird</param>
        /// <returns>Disk</returns>
        protected override object MapToEntity(MySqlDataReader reader)
        {
            Disk disk = new Disk();
            ProducerDataAccess producerDataAccess = new ProducerDataAccess();

            disk.Id = Int32.Parse(reader.GetValue(0).ToString());
            disk.Description = reader.GetValue(1).ToString();
            disk.Capacity = ulong.Parse(reader.GetValue(2).ToString());
            disk.Ssd = Boolean.Parse(reader.GetValue(3).ToString());
            disk.Inch = Double.Parse(reader.GetValue(4).ToString());
            disk.Producer = producerDataAccess.GetEntityById<Producer>(Int32.Parse(reader.GetValue(5).ToString()));
            disk.PhysicalInterfaces = this.GetPhysicalInterfaces(disk);

            return disk;
        }

        /// <summary>
        /// Liest alle Beziehungen zu der Entität 'Schnittstelle' aus der Datenbank
        /// </summary>
        /// <param name="entity">Die Festplatte, zu welcher die Schnittstellen ermittelt werden</param>
        /// <returns>Liste von PhysicalInterfaceWithCount</returns>
        private List<PhysicalInterfaceWithCount> GetPhysicalInterfaces(Disk entity)
        {
            List<PhysicalInterfaceWithCount> physicalInterfaces = new List<PhysicalInterfaceWithCount>();
            PhysicalInterfaceDataAccess physicalInterfaceDataAccess = new PhysicalInterfaceDataAccess();
            MySqlConnection connection = this.CreateConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `" + this.GetTableName() + "` WHERE id_festplatte = " + entity.Id;

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
    }
}