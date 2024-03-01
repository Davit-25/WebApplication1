using Azure.Core;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Iterfaces;
using WebApplication1.Mapping;
using WebApplication1.Models;


namespace WebApplication1.Services
{
    public class SessionsService:ISessionsService
    {
        private readonly CinemaTableContext _tableContext;

        private readonly IFilmListMapping<Sessions,ModelSessions> _mappings;

        public SessionsService(CinemaTableContext context)
        {
            _tableContext=context;
            _mappings = new MapperSessions();
        }

        public IEnumerable<Sessions> GetAllSessions()
        {
            IEnumerable<Sessions> sessions = _tableContext.sessionsDB.ToList();
            return sessions;
        }
        public CreateSessionsResponce CreateSessions(ModelSessions modelSessions)
        {
            var sessionsEntity = _mappings.FilmListMapFromModelToEntity(modelSessions);
            var newSessions = _tableContext.sessionsDB.Add(sessionsEntity);
            _tableContext.SaveChanges();

            return new CreateSessionsResponce { modelSessions= modelSessions };
        }

        public DeleteSessionsResponce DeleteSessions(DeleteSessionsRequest deleteSessionsRequest)
        {
            var deleteToSessions = _tableContext.sessionsDB.Find(deleteSessionsRequest.DeleteSessRequestID);
            if (deleteToSessions == null)
            {

                throw new DbUpdateException($"This id{deleteSessionsRequest.DeleteSessRequestID} doesn't exists");
            }
            _tableContext.sessionsDB.Remove(deleteToSessions);
            _tableContext.SaveChanges();
            return new DeleteSessionsResponce { IsdeletedSessions=true };
        }


        public GetSessionsResponce GetSessions(GetSessionsRequest getSessionsRequest)
        {
            var sessions = _tableContext.sessionsDB.Find(getSessionsRequest.GetSesRequestID);
            if (sessions == null)
            {
                return new GetSessionsResponce { };
            }

            var sessionsModel = _mappings.FilmListMapFromEntityToModel(sessions);
            var responce = new GetSessionsResponce { GetModelSessions= sessionsModel };
            return responce;
        }

        public UpdateSessionsResponce UpdateSessions(UpdateSessionsRequest updateSessionsRequest)
        {
            var sessionsExists = _tableContext.sessionsDB.Any(e => e.id == updateSessionsRequest.UpdateToSessions.id);

            var exitingEntity = _tableContext.sessionsDB.Find(updateSessionsRequest.UpdateToSessions.id);




            _mappings.FilmListMapFromModelToEntity(updateSessionsRequest.UpdateToSessions, exitingEntity);
            _tableContext.SaveChanges();
            return new UpdateSessionsResponce {UpdatedSessions= updateSessionsRequest.UpdateToSessions };
        }
    }
}
