namespace Dream.API.Services
{
    public interface ILocalMailService
    {
        public void Send(string subject, string message);
    }
}
