using APBD4.Models;
using Microsoft.Data.SqlClient;

namespace APBD4;

public class Database
{
    private readonly string _sqlConn =
        "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";

    public async Task<List<Animal>> GetAnimals(string? orderBy)
    {
        List<Animal> results = new();

        using (SqlConnection conn = new(_sqlConn))
        {
            using (SqlCommand command = new())
            {
                command.Connection = conn;
                command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy ?? "Name"} ASC";
                await conn.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(new Animal
                        {
                            IdAnimal = int.Parse(reader["IdAnimal"].ToString()!),
                            Name = reader["Name"].ToString()!,
                            Description = reader["Description"].ToString()!,
                            Category = reader["Category"].ToString()!,
                            Area = reader["Area"].ToString()!,
                        });
                    }
                }
            }
        }

        return results;
    }

    public async Task CreateAnimal(Animal animal)
    {
        using (SqlConnection conn = new(_sqlConn))
        {
            using (SqlCommand command = new())
            {
                command.Connection = conn;
                command.CommandText =
                    "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@name, @description, @category, @area)";
                await conn.OpenAsync();
                command.Parameters.AddWithValue("name", animal.Name);
                command.Parameters.AddWithValue("description", animal.Description);
                command.Parameters.AddWithValue("category", animal.Category);
                command.Parameters.AddWithValue("area", animal.Area);
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAnimal(Animal animal)
    {
        using (SqlConnection conn = new(_sqlConn))
        {
            using (SqlCommand command = new())
            {
                command.Connection = conn;
                command.CommandText =
                    "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE IdAnimal = @id";
                await conn.OpenAsync();
                command.Parameters.AddWithValue("name", animal.Name);
                command.Parameters.AddWithValue("description", animal.Description);
                command.Parameters.AddWithValue("category", animal.Category);
                command.Parameters.AddWithValue("area", animal.Area);
                command.Parameters.AddWithValue("id", animal.IdAnimal);
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAnimal(int id)
    {
        using (SqlConnection conn = new(_sqlConn))
        {
            using (SqlCommand command = new())
            {
                command.Connection = conn;
                command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @id";
                await conn.OpenAsync();
                command.Parameters.AddWithValue("id", id);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
