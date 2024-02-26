using WebApplication1.Entities;
using WebApplication1.Iterfaces;
using WebApplication1.Mapping;
using AspNetCore;
using WebApplication1.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Services
{
    public class FilmListService : IFilmListService
    {
        private readonly CinemaTableContext _tableContext;
        private readonly IFilmListMapping<FilmsList, ModelFilmList> _filmListMapping;
     
        public FilmListService(CinemaTableContext context)
        {
            _tableContext=context;
            _filmListMapping = new MapperFilmList();
        }
      

        public IEnumerable<FilmsList> GetFilmsList(string searchString)
        {
         
                return _tableContext.filmListDB.Where(e => string.IsNullOrEmpty(searchString) || e.name.Contains(searchString));

        }
        public FilmListResponce CreateFilmList(ModelFilmList request)
        {
           
            var filmListEntity = _filmListMapping.FilmListMapFromModelToEntity(request);
            var newFilmsList = _tableContext.filmListDB.Add(filmListEntity);
            _tableContext.SaveChanges();
           
                return new FilmListResponce {modelFilmList  = request };
        }

        public DeleteFilmListResponce DeleteFilmList(DeleteFilmListRequest deleteFilmListRequest)
        {
            var deleteToFilmList = _tableContext.filmListDB.Find(deleteFilmListRequest.DeleteRequestID);
            if (deleteToFilmList == null)
            {

                throw new DbUpdateException($"This id{deleteFilmListRequest.DeleteRequestID} doesn't exists");
            }
            _tableContext.filmListDB.Remove(deleteToFilmList);
            _tableContext.SaveChanges();
            return new DeleteFilmListResponce { IsdeletedFilmList = true };
        }

        public GetFilmListResponce GetFilmList(GetFilmListRequest getFilmListRequest)
        {
            var filmList = _tableContext.filmListDB.Find(getFilmListRequest.GetRequestID);
            if (filmList==null)
            {
                return new GetFilmListResponce { };
            }

            var filmListModel=_filmListMapping.FilmListMapFromEntityToModel(filmList);
            var responce= new GetFilmListResponce { GetModelFilmList = filmListModel };
            return responce;
        }

        
        public UpdateFilmListResponce UpdateFilmList(UpdateFilmListRequest updateFilmListRequest)
        {
            var filmListExists = _tableContext.filmListDB.Any(e => e.id == updateFilmListRequest.UpdatetoFilmList.id);
          
              var exitingEntity = _tableContext.filmListDB.Find(updateFilmListRequest.UpdatetoFilmList.id);
               
            

           
            _filmListMapping.FilmListMapFromModelToEntity(updateFilmListRequest.UpdatetoFilmList, exitingEntity);
            _tableContext.SaveChanges(); 
            return new UpdateFilmListResponce {UpdatedFilmList= updateFilmListRequest.UpdatetoFilmList };

        }
    }
}
