﻿namespace Dream.API.Services
{
    public interface IMailService
    {
        public void Send(string subject, string message);
    }
}
