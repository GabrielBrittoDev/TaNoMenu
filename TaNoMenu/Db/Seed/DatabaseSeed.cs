using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace TaNoMenu.Db.Seed
{
    public static class DatabaseSeed
    {
        private static IDbConnection _dbConnection;

        public static void CreateDb(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration["DatabaseSettings:ConnectionString"]);
            _dbConnection.Open();

            _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Establishments (
                        Id  INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
                        Name VARCHAR(255) NOT NULL,
                        Description TEXT DEFAULT NULL,
                        Category ENUM('Pizzaria', 'Sorveteria', 'Esfirraria', 'Restaurante', 'Pastelaria', 'Lanchonete', 'Churrascaria', 'Outros') DEFAULT 'Outros',
                        Street VARCHAR(255) NULL,
                        Number INTEGER NOT NULL,
                        Complement VARCHAR(255),
                        Neighborhood VARCHAR(255) NOT NULL,
                        Zip VARCHAR(15) NOT NULL,
                        City VARCHAR(100) NOT NULL,
                        Phone VARCHAR(15) NOT NULL,
                        Picture VARCHAR(255) DEFAULT NULL,
                        Email VARCHAR(255) NOT NULL,
                        Password VARCHAR(255) NOT NULL
                    )");
            
            _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Recipes (
                        Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
                        Name VARCHAR(255) NULL,
                        EstablishmentId INT UNSIGNED NOT NULL,
                        Description TEXT DEFAULT NULL,
                        Picture VARCHAR(255) DEFAULT NULL,
                        Price DECIMAL(5,2) NOT NULL,
                        FOREIGN KEY (EstablishmentId) REFERENCES Establishments(Id) ON DELETE CASCADE ON UPDATE CASCADE 
                    )");


            _dbConnection.Close();

        }
    }
}