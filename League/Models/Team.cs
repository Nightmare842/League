using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Team
{
    private int id;
    private byte[] logo;
    private string name;
    private string league;
    private string description;
    private List<string> members;
    private string connectionString;

    public Team(int id, byte[] logo, string name, string league, string description, string connectionString)
    {
        this.id = id;
        this.logo = logo;
        this.name = name;
        this.league = league;
        this.description = description;
        this.connectionString = connectionString;
        this.members = GetMembersFromDb();
    }

    public void AddMember(string member)
    {
        members.Add(member);
        AddMemberToDb(member);
    }

    public void RemoveMember(string member)
    {
        members.Remove(member);
        RemoveMemberFromDb(member);
    }

    public int Id
    {
        get { return id; }
    }

    public byte[] Logo
    {
        get { return logo; }
        set { logo = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string League
    {
        get { return league; }
        set { league = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public List<string> Members
    {
        get { return members; }
    }

    private void AddMemberToDb(string member)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "INSERT INTO TeamMembers (TeamId, Member) VALUES (@teamId, @member)";
                command.Parameters.AddWithValue("@teamId", id);
                command.Parameters.AddWithValue("@member", member);
                command.ExecuteNonQuery();
            }
        }
    }

    private void RemoveMemberFromDb(string member)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "DELETE FROM TeamMembers WHERE TeamId = @teamId AND Member = @member";
                command.Parameters.AddWithValue("@teamId", id);
                command.Parameters.AddWithValue("@member", member);
                command.ExecuteNonQuery();
            }
        }
    }
}
