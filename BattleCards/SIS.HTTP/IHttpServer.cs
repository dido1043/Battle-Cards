﻿namespace SIS.HTTP
{
    public interface IHttpServer
    {
        
        Task StartAsync(int port);
    }
}
