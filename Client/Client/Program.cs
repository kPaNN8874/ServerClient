using System.Net.Sockets;
using System.Net;
using System.Text;


    class Program
    {

    // Main Method
    static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 5000);
        using (NetworkStream stream = client.GetStream())
        {
            while (true)
            {
                Console.Write("Enter a number or text (or type 'exit' to quit): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit") break;

                byte[] data = Encoding.ASCII.GetBytes(input);
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Server response: " + response);
            }
        }
    }
}