namespace WebApplication3.Models
{
    public class Cardjson
    {
        public int Id { get; set; }
        public string jsoncard { get; set; }
        public bool type { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
    }
}
