﻿using LuGa.Core.Device.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace LuGa.Core.Repository
{
    /// <summary>
    /// Repository for Events.
    /// </summary>
    public class EventsRepository : IRepository<Event>
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string connectionString;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public EventsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Mysql connection client
        /// </summary>
        public MySqlConnection Connection => new MySqlConnection(connectionString);

        /// <summary>
        /// Add Event
        /// </summary>
        /// <param name="row">Event object to save</param>
        public void Add(Event row)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "INSERT INTO Events (DeviceId, Action, Zone, Value, TimeStamp) VALUES(@DeviceId, @Action, @Zone, @Value, @TimeStamp)";
                dbConnection.OpenAsync();
                dbConnection.Execute(sQuery, row);
            }
        }

        /// <summary>
        /// Retrieve all Events
        /// </summary>
        /// <returns>List of Events</returns>
        public IEnumerable<Event> GetAll()
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Events";
                dbConnection.OpenAsync();
                return dbConnection.Query<Event>(sQuery);
            }
        }

        /// <summary>
        /// Get particular event
        /// </summary>
        /// <param name="id">Id of event to find</param>
        /// <returns>Found event or null</returns>
        public Event GetById(int id)
        {
            using (MySqlConnection dbConnection = Connection)
            {
                const string sQuery = "SELECT * FROM Events WHERE Id = @Id";
                dbConnection.OpenAsync();
                return dbConnection.Query<Event>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
    }
}
