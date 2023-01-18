using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League.Models
{
    class Spieler
    {
        private int id;
        private string name;
        private int age;
        private string position;
        private string team;
        private string connectionString;

        public Spieler(int id, string name, int age, string position, string team, string connectionString)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.position = position;
            this.team = team;
            this.connectionString = connectionString;
            CreateDbIfNotExists();
        }

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public string Team
        {
            get { return team; }
            set { team = value; }
        }

        public void SaveToDb()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Players (Id, Name, Age, Position, Team) VALUES (@id, @name, @age, @position, @team)";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@position", position);
                    command.Parameters.AddWithValue("@team", team);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateDbIfNotExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Players') CREATE TABLE Players (Id int, Name nvarchar(50), Age int, Position nvarchar(50), Team nvarchar(50))";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
