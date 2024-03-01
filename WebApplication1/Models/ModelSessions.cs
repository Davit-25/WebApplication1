using WebApplication1.Entities;

namespace WebApplication1.Models
{
    public class ModelSessions
    {
        public int id { get; set; }
        public DateTime dateTime { get; set; }
        public decimal price { get; set; }
        public List<PackageOfCinema> films { get; set; }
    }
}
