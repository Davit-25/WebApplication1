using System.Text.Json.Serialization;

namespace WebApplication1.Entities
{
    public class FilmsList
    {

        public int id { get; set; }

        public string name { get; set; }
        public string genre { get; set; }
        public int setAge { get; set; }
        public double imbd { get; set; }

        public List<PackageOfCinema> sessions { get; set; }
    }
}
