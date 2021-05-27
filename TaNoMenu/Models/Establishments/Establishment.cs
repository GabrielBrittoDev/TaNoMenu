using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaNoMenu.Models.Establishments
{
    public class Establishment 
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string Picture { get; set; }
        public string Category { get; set; }
        public int Number { get; set; }
        public int Complement { get; set; }
        public string Neighborhood { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Recipe> Recipes { get; set; }

        public Establishment()
        {
        }
        
    }
}