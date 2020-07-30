using ApiTeste.Service.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;

namespace ApiTeste.Infrastructure.Model
{
    public class Response
    {
        public HttpStatusCode HttpStatus { get; set; }
        private readonly IList<string> _messages = new List<string>();
        private readonly IList<object> _custommessages = new List<object>();
        public IEnumerable<string> Errors { get; }
        public IEnumerable<object> CustomErrors { get; }
        public object Result { get; }
        public Response()
        {
            Errors = new ReadOnlyCollection<string>(_messages);
            CustomErrors = new ReadOnlyCollection<object>(_custommessages);
        }
        public Response(object result) : this() => Result = result;
        public Response AddError(string message)
        {
            _messages.Add(message);
            return this;
        }

        public Response AddError(Message message)
        {
            _messages.Add(message.message);
            return this;
        }

        public Response(object result, HttpStatusCode statusCode)
        {
            Result = result;
            HttpStatus = statusCode;
            Errors = new ReadOnlyCollection<string>(_messages);
            CustomErrors = new ReadOnlyCollection<object>(_custommessages);
        }
    }
}
