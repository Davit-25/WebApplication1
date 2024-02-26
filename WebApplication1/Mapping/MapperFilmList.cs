using WebApplication1.Entities;
using WebApplication1.Iterfaces;
using WebApplication1.Models;

namespace WebApplication1.Mapping
{
    public class MapperFilmList : IFilmListMapping<FilmsList, ModelFilmList>
    {
        

        ModelFilmList IFilmListMapping<FilmsList, ModelFilmList>.FilmListMapFromEntityToModel(FilmsList filmsList) => new ModelFilmList()
        {
            id = filmsList.id,
            name = filmsList.name,
            genre = filmsList.genre,
            setAge = filmsList.setAge,
            imbd = filmsList.imbd,
            sessions = filmsList.sessions,
        };

        public FilmsList FilmListMapFromModelToEntity(ModelFilmList model)
        {
            var entity=new FilmsList();
            FilmListMapFromModelToEntity(model, entity);
            return entity;
        }

        public void FilmListMapFromModelToEntity(  ModelFilmList model, FilmsList entity)
        {
            entity.id = model.id;
            entity.name = model.name;
            entity.genre = model.genre;
            entity.setAge = model.setAge;
            entity.imbd = model.imbd;
            entity.sessions = model.sessions;
        }

       
    }
}
