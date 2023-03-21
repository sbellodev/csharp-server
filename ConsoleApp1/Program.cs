using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EjemploSockets
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress direccion = IPAddress.Any; // Escucha en cualquier dirección IP local
            int puerto = 1234; // Puerto de escucha
            servidor.Bind(new IPEndPoint(direccion, puerto));
            servidor.Listen(10); // Permite hasta 10 conexiones en espera
            Console.WriteLine("Servidor iniciado en {0}:{1}", direccion, puerto);

            while (true)
            {
                Socket cliente = servidor.Accept(); // Acepta una nueva conexión entrante
                Console.WriteLine("Cliente conectado: {0}", cliente.RemoteEndPoint.ToString());

                byte[] buffer = new byte[1024];
                int bytesRecibidos = cliente.Receive(buffer);
                string mensaje = Encoding.ASCII.GetString(buffer, 0, bytesRecibidos);
                Console.WriteLine("Mensaje recibido: {0}", mensaje);

                string respuesta = "Hola desde el servidor";
                byte[] bufferRespuesta = Encoding.ASCII.GetBytes(respuesta);
                cliente.Send(bufferRespuesta);

                cliente.Shutdown(SocketShutdown.Both);
                cliente.Close();
            }

            //servidor.Close();
        }
    }
}
