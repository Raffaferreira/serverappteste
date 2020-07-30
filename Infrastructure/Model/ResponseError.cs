using ApiTeste.Service.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTeste.Infrastructure.Model
{
    public class ResponseError
    {
        protected async Task<Response> Errors(List<dynamic> failures)
        {
            var response = new Response();

            foreach (var failure in failures)
            {
                response.AddError(failure);
            }

            return await Task.FromResult(response as Response);
        }

        protected async Task<Response> Errors(dynamic failures)
        {
            var response = new Response();
            response.AddError(failures);
            return await Task.FromResult(response as Response);
        }

        protected async Task<Response> Errors(Message failures)
        {
            var response = new Response();
            response.AddError(failures);
            return await Task.FromResult(response as Response);
        }

        protected async Task<Response> Errors(List<Message> failures)
        {
            var response = new Response();

            foreach (var failure in failures)
            {
                response.AddError(failure);
            }

            return await Task.FromResult(response as Response);
        }
    }
}
