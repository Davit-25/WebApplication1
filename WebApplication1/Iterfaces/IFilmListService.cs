using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Iterfaces
{
    public interface IFilmListService
    {
        IEnumerable<FilmsList> GetFilmsList(string searchString);
        FilmListResponce CreateFilmList(ModelFilmList modelFilmList);
        GetFilmListResponce GetFilmList(GetFilmListRequest getFilmListRequest);
        UpdateFilmListResponce UpdateFilmList(UpdateFilmListRequest updateFilmListRequest);
        DeleteFilmListResponce DeleteFilmList(DeleteFilmListRequest deleteFilmListRequest);
    }
}
