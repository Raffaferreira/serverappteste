namespace ApiTeste.Handlers
{
    public class ResponseLayer : IResponseLayer
    {
        public dynamic Result { get; set; }
        public bool HasErrors { get; set; }
        public dynamic Error { get; set; }
        public IResponseLayer AddMessage(bool executed, dynamic message)
        {
            if (!executed)
            {
                Result = null;
                HasErrors = true;
                Error = message;
            }
            else
            {
                Error = null;
                HasErrors = false;
                Result = message;
            }

            return this;
        }
    }
}
