using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

    class Program
    {

    // Main Method
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Server is running...");
        while (true)
        {
            using (TcpClient client = listener.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // client closed connection

                    string input = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    string response;

                    // Check if input is a number
                    int number;
                    if (int.TryParse(input, out number))
                    {
                        int result = number * 2;
                        response = "Number * 2: " + result.ToString();
                    }
                    else
                    {
                        response = "Uppercase text: " + input.ToUpper();
                    }

                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
        }
    }
}
    