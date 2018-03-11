using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowHostInfo("www.youtubesdhgfgdrt.com");
            ShowHostInfo("www.google.com");
            Console.Write("Enter to close terminal...");
            Console.Read();
        }

        public static void ShowHostInfo(string hostName)
        {
            Host host = ByPing(hostName);
            Console.WriteLine(host._name + "\t" + host._ip + "\t" + host._status);
        }

        public static Host ByPing(string hostName)
        {
            Ping p = new Ping();
            PingReply reply;

            try
            {
                IPAddress hostIP = Dns.GetHostAddresses(hostName)[0];
                reply = p.Send(hostIP);
                if (reply.Status == IPStatus.Success)
                    return new Host(hostName, hostIP.ToString(), true);
            }
            catch
            {
                return Host.FailHost(hostName, false);
            }

            return Host.FailHost(hostName, false);
        }

    }

    public class Host
    {
        public string _name;
        public string _ip;
        public bool _status;

        public Host(string name, string ip, bool status)
        {
            _name = name;
            _ip = ip;
            _status = status;
        }

        public static Host FailHost(string name, bool status)
        {
            return new Host(name, "", status);
        }
    }
}
