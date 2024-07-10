//using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Services
{
    internal class DbService
    {
        private readonly string _connectionString;

        public DbService(string dbPath)
        {
            _connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";
        }

        public List<TaskItem> GetTasks()
        {
            var tasks = new List<TaskItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Tasks";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TaskItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IsCompleted = reader.GetBoolean(2)
                        });
                    }
                }
            }

            return tasks;
        }

        public void SaveTask(TaskItem task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = task.Id == 0
                    ? "INSERT INTO Tasks (Name, IsCompleted) VALUES (@Name, @IsCompleted)"
                    : "UPDATE Tasks SET Name = @Name, IsCompleted = @IsCompleted WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    if (task.Id != 0)
                        command.Parameters.AddWithValue("@Id", task.Id);
                    command.Parameters.AddWithValue("@Name", task.Name);
                    command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(TaskItem task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Tasks WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", task.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
