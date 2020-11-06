using System.Threading.Tasks;
using RestSharp;

namespace EventsProcessor.ServiceReferences.Base
{
    public class BaseHttpClient
    {
        public async Task<RestResponse> Execute(string url, Method method, object content)
        {
            var client = new RestClient(url);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(method);
            request.AddJsonBody(content);
            return await ExecuteAsync(client, request);
        }

        private async Task<RestResponse> ExecuteAsync(RestClient client, RestRequest request)
        {
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            return (RestResponse)(await taskCompletion.Task);
        }

    }
}