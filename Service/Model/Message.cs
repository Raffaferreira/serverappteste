using System.Net;

namespace ApiTeste.Service.Model
{
    public class Message
    {
        public dynamic Result { get; set; }
        public string message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Executed { get; set; }

        public Message(HttpStatusCode codigo, dynamic returment = null)
        {
            Result = returment;
            StatusCode = codigo;

            if (StatusCode == HttpStatusCode.OK || StatusCode == HttpStatusCode.Created)
            {
                Executed = true;
            }
            else
            {
                Executed = false;
            }
        }

        public Message(HttpStatusCode codigo, string mensagem, dynamic returment = null)
        {
            Result = returment;
            message = mensagem;
            StatusCode = codigo;

            if (StatusCode == HttpStatusCode.OK || StatusCode == HttpStatusCode.Created)
            {
                Executed = true;
            }
            else
            {
                Executed = false;
            }
        }
    }
}
