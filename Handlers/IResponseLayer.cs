namespace ApiTeste.Handlers
{
    public interface IResponseLayer
    {
        dynamic Result { get; set; }
        bool HasErrors { get; set; }
        dynamic Error { get; set; }
        IResponseLayer AddMessage(bool executed, dynamic message);
    }
}
