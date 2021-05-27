using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TaNoMenu.Models;


namespace TaNoMenu.Repositories
{
    public class RecipeRepository : AbstractRepository<Recipe>
    {
        public RecipeRepository(IConfiguration configuration): base(configuration) { }

        public override void Add(Recipe item)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO Recipes (Name, EstablishmentId, Description, Picture, Price) VALUES(@Name, @EstablishmentId, @Description, @Picture, @Price)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, item);
            }
        }

        public override void Remove(int id)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM Recipes" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }
        
        public override void Update(Recipe item)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "UPDATE Recipes SET Name = @Name,"
                                + " Description = @Description, Price= @Price," 
                                + " Picture= @Picture" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(sQuery, item);
            }
        }
        
        public override Recipe FindById(int id)
        { 
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Recipes" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Recipe>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
        public override IEnumerable<Recipe> FindAll()
        { 
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Recipe>("SELECT * FROM Recipes");
            }
        }
    }
}