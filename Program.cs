using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UDP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            string IP, text, dots = "...", txtFile;
            int count = 0, port;

            l1:
            Console.WriteLine("[ + ] Wpisz scieżke do twojego pliku .txt: ");
            txtFile = Console.ReadLine();
            Console.WriteLine("[ / ] Inicjowanie...");

            foreach (char ch in dots)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine(ch);
            }

            try
            {
                using (StreamReader sr = new StreamReader(txtFile))
                    text = sr.ReadToEnd();
            }
            catch (FileNotFoundException finoex)
            {
                Console.WriteLine("[ -.- ] Ścieżka pliku nie została odnaleziona lub plik nie istnieje");
                goto l1;
            }
            Console.WriteLine("[ + ] Trwa uruchamianie, proszę czekać");
            System.Threading.Thread.Sleep(2000);

            Console.WriteLine("[ + ] Wpisz IP :");
            IP = Console.ReadLine();
            Console.WriteLine("[ / ] Inicjowanie...");

            foreach (char ch in dots)
            {
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine(ch);
            }

            Console.WriteLine("[ + ] Wpisz PORT :");
            var messageport = Console.ReadLine();
            if (messageport == "")
            {
                port = 80;
            }
            else
            {
                port = Convert.ToInt32(Console.ReadLine());
            }



            Console.WriteLine("[ + ] Włączanie Procesu");
            System.Threading.Thread.Sleep(2000);

            byte[] packetData = System.Text.ASCIIEncoding.ASCII.GetBytes(text);

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            l2:
            try
            {
                client.SendTo(packetData, ep);
                count++;
                Console.WriteLine("[ + ] Trwa wysylanie pakietow (Nacisnij ENTER aby ponowic wysylanie pakietu) ");
                Console.ReadLine();
                goto l2;
            }
            catch (SystemException syex)
            {
                Console.WriteLine("[ -.- ] Cos poszlo nie tak. Zrestartuj program");
                Console.ReadLine();
                goto l1;

            }
            
        }
    }
}
