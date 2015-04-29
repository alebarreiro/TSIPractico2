using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Fleck;
using System.Net;

namespace BusinessLogicLayer
{
    class WebsocketChat
    {
        private static Dictionary<IWebSocketConnection, string> connections = new Dictionary<IWebSocketConnection, string>();
        public WebsocketChat()
        {
            var server = new WebSocketServer("ws://mvcemployees.azurewebsites.net/");
            //"ws://mvcemployees.azurewebsites.net:8181"
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Debug.WriteLine("Alguien se ha conectado...");
                };

                socket.OnMessage = message =>
                {
                    // first use
                    if (!connections.ContainsKey(socket))
                    {
                        connections.Add(socket, message);

                    }
                    else
                    {
                        string name;
                        if (connections.TryGetValue(socket, out name))
                        {
                            string m = "{ \"id\": \"" + name + "\", \"msg\": \"" + message + "\"}";
                            foreach (var c in connections)
                            {
                                    c.Key.Send(m);
                            }
                        }

                    }

                };

                socket.OnClose = () =>
                {
                    Debug.WriteLine("Websocket close...");
                    connections.Remove(socket);
                };

            });

        }
    }
}
