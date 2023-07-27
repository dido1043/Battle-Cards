﻿using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SIS.HTTP
{
    public class HttpServer : IHttpServer
    {
        List<Route> routeTable = new List<Route>();
        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
        }

       
        public async Task StartAsync(int port)
        {
            TcpListener listener =
                new TcpListener(IPAddress.Loopback, port);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                ProcessClientAsync(client);
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();

                List<byte> data = new List<byte>();
                int position = 0;
                byte[] buffer = new byte[4092];
                //byte[] data = new byte[0];
                while (true)
                {
                    int count = await stream.ReadAsync(buffer, position, buffer.Length);
                    position += count;
                    if (count == 0)
                    {
                        break;
                    }
                    if (count < buffer.Length)
                    {
                        var partialBuffer = new byte[count];
                        Array.Copy(buffer, partialBuffer, count);
                        data.AddRange(partialBuffer);
                        break;
                    }
                    else
                    {
                        data.AddRange(buffer);
                    }


                }

                var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                var request = new HttpRequest(requestAsString);

                HttpResponse response;
                var route = this.routeTable.FirstOrDefault(x => x.Path == request.Path
                && x.Method == request.Method);
                Console.WriteLine(requestAsString);
                if(route != null)
                {
                   
                    response = route.Action(request);
                }
                else
                {
                    response = new HttpResponse("text/html", new byte[0], HttpStatus.NotFound);
                }

                
                var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());
                await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                await stream.WriteAsync(response.Body, 0, response.Body.Length);
                Console.WriteLine(response.ToString());

                client.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

        }

    }
}
