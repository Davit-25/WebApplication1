
using WebApplication1.Models;

namespace WebApplication1.Entities
{
    public class PackageOfCinema
    {
        public int id { get; set; }
        public int idFilms { get; set; }
        public string idSessions { get; set; }
        public FilmsList Films { get; set; }
        public Sessions Sessions { get; set; }

        
    }
}
