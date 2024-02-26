using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Iterfaces
{
    public interface IFilmListMapping<TEntity, TModel>
    {
        TModel FilmListMapFromEntityToModel(TEntity entity);
      
        TEntity FilmListMapFromModelToEntity(TModel model);
        void FilmListMapFromModelToEntity(TModel model, TEntity entity);
       
    }
}
