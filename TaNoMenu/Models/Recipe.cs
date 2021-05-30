using System.Collections.Generic;
using TaNoMenu.Models.Establishments;

namespace TaNoMenu.Models
{
    public class Recipe 
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public float Price { get; set; }
        public int CookTime { get; set; }
        public int EstablishmentId { get; set; }
    }
}