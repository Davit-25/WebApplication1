using WebApplication1.Entities;
using WebApplication1.Iterfaces;
using WebApplication1.Models;

namespace WebApplication1.Mapping
{
    public class MapperSessions : IFilmListMapping<Sessions, ModelSessions>
    {

        ModelSessions IFilmListMapping<Sessions, ModelSessions>.FilmListMapFromEntityToModel(Sessions sessions) => new ModelSessions()
        {
            id = sessions.id,
            dateTime = sessions.dateTime,
            price = sessions.price,
            films = sessions.films,
        };

        public Sessions FilmListMapFromModelToEntity(ModelSessions model)
        {
            var entity = new Sessions();
            FilmListMapFromModelToEntity(model, entity);
            return entity;
        }

        public void FilmListMapFromModelToEntity(ModelSessions model, Sessions entity)
        {
            entity.id = model.id;
            entity.dateTime = model.dateTime;
            entity.price = model.price;
            entity.films = model.films;
        }



    }
}
