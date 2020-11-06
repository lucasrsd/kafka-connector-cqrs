using System.Threading.Tasks;
using EventsProcessor.ServiceReferences.Base;
using RestSharp;

namespace EventsProcessor.ServiceReferences.PropostaApi
{
    public interface IPropostaApi
    {
        Task ExecuteHttp(Method method, object content);
    }
}