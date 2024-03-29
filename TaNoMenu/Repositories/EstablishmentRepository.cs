using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TaNoMenu.Exceptions;
using TaNoMenu.Models;
using TaNoMenu.Models.Establishments;


namespace TaNoMenu.Repositories
{
    public class EstablishmentRepository : AbstractRepository<Establishment>
    {
        public EstablishmentRepository(IConfiguration configuration): base(configuration) { }

        public override void Add(Establishment item)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO Establishments (Name, Description, Street, Number, Complement, Category, Picture, Neighborhood, Zip, City, Phone, Email, Password) VALUES(@Name, @Description, @Street, @Number, @Complement, @Category, @Picture, @Neighborhood, @Zip, @City, @Phone, @Email, @Password)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, item);
            }
        }

        public override void Remove(int id)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM Establishments" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }
        
        public override void Update(Establishment item)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "UPDATE Establishments SET Name = @Name, Description = @Description,"
                                + " Street = @Street, Number= @Number," 
                                + " Complement = @Complement, Neighborhood= @Neighborhood," 
                                + " Zip = @Zip, City= @City," 
                                + " Picture = @Picture, Category = @Category," 
                                + " Phone = @Phone, Email= @Email," 
                                + " Password = @Password" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(sQuery, item);
            }
        }
        
        public override Establishment FindById(int id)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                var sQuery = "SELECT * FROM Recipes"
                             + " WHERE EstablishmentId = @Id;";
                var recipeQuery = "SELECT * FROM Establishments"
                                  + " WHERE Id = @Id;";
                dbConnection.Open();
                SqlMapper.GridReader multi = dbConnection.QueryMultiple(sQuery + " " + recipeQuery , new {Id = id});
                List<Recipe> recipes = multi.Read<Recipe>().ToList();
                List<Establishment> establishments = multi.Read<Establishment>().ToList();
                if (!establishments.Any())
                {
                    throw new HttpException("Estabelecimento não encontrado", 404);
                }

                Establishment establishment = establishments.First();
                establishment.Recipes = recipes;
                return establishment;
            }
        }
        
        public Establishment Authenticable(Establishment establishment)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Establishments"
                             + " WHERE Email = @email" +
                             " AND Password = @password";
                
                dbConnection.Open();
                IEnumerable<Establishment>establishments = dbConnection.Query<Establishment>(sQuery, establishment);
                if (!establishments.Any())
                {
                    throw new Exception("Usuário ou senha inválidos");
                }

                return establishments.First();
            }
        }
        
        public override IEnumerable<Establishment> FindAll()
        { 
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Establishment>("SELECT * FROM Establishments");
            }
        }
    }
}