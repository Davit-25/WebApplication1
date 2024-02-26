using WebApplication1.Entities;

namespace WebApplication1.Models
{
    public class ModelFilmList
    {
        
        public int id { get; set; }
      
        public  string   name { get; set; }
    
        public  string   genre { get; set; }
        public int setAge { get; set; }
        public double imbd { get; set; }

        public  List<PackageOfCinema>    sessions { get; set; }


    }
}
