using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Iterfaces
{
    public interface ISessionsService
    {
        IEnumerable<Sessions>GetAllSessions();
        CreateSessionsResponce CreateSessions(ModelSessions modelSessions);
        GetSessionsResponce GetSessions(GetSessionsRequest getSessionsRequest);
        UpdateSessionsResponce UpdateSessions(UpdateSessionsRequest updateSessionsRequest);
        DeleteSessionsResponce DeleteSessions(DeleteSessionsRequest deleteSessionsRequest);
    }
}
