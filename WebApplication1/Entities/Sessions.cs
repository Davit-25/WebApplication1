using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    public class Sessions
    {
        public int id { get; set; }
        public DateTime dateTime { get; set; }
        public decimal price { get; set; }
        public List<PackageOfCinema> films { get; set;}
    }
}
