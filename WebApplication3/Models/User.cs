using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public List<Cardjson> cardjsons { get; set; }
    }
}
