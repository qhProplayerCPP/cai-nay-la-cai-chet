﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
namespace server_cs
{
    public partial class server : Form
    {
        public server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            connect();
        }

        private IPEndPoint ip;
        private Socket server_socket;
        private List<CLIENT> client_list;

        private struct CLIENT
        {
            public Socket client;
            public string client_name;
            private bool status;

            public void set_status(bool set)
            {
                status = set;
            }
        }

        private List<user_info> all_users;

        private struct user_info
        {
            public string username;
            public string password;
            public string fullname;
            public string dob;
        }

        private void get_data(ref List<user_info> all_users)
        {
            all_users = new List<user_info>();
            using (StreamReader fin = new StreamReader("database.txt"))
            {
                while (!fin.EndOfStream)
                {
                    user_info temp;
                    temp.username = fin.ReadLine();
                    temp.password = fin.ReadLine();
                    temp.fullname = fin.ReadLine();
                    temp.dob = fin.ReadLine();
                    all_users.Add(temp);
                }
            }
        }

        private void connect()
        {
            client_list = new List<CLIENT>();
            ip = new IPEndPoint(IPAddress.Any, 2503);
            server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
            server_socket.Bind(ip);
            server_socket.Listen(50);

            //======================huy
            Thread receive_from_client = new Thread(receive);
            receive_from_client.IsBackground = true;
            receive_from_client.Start();
            //=========================
        }

        private byte[] serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        private object deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

        //=================================hien

        //=================================huy
        private void login(ref Socket client, string[] info)
        {
            CLIENT temp = new CLIENT();
            try
            {
                get_data(ref all_users);
                add_message("Dang o thread login");
                bool check = false;
                foreach (user_info item in all_users)
                {
                    if (item.username == info[1] && item.password == info[2])
                    {
                        check = true;
                        break;
                    }
                }
                if (check == true)
                {
                    client.Send(serialize("true"));
                    temp.client = client;
                    temp.client_name = info[1];
                    client_list.Add(temp);
                    string login_notice = "User " + temp.client_name + " connected!";
                    add_message(login_notice);
                }
                else
                {
                    client.Send(serialize("false"));
                }
            }
            catch
            {
                client_list.Remove(temp);
                client.Close();
            }
        }


        private void register(ref Socket client, string[] info)
        {
            CLIENT temp = new CLIENT();
            try
            {
                get_data(ref all_users);
                add_message("Dang o thread register");
                bool check = true;
                foreach (user_info item in all_users)
                {
                    if (item.username == info[1])
                    {
                        check = false;
                        break;
                    }
                }
                if (check == false)
                {
                    client.Send(serialize("false"));
                }
                else
                {
                    client.Send(serialize("true"));
                    using (StreamWriter sw = new StreamWriter("database.txt", true))
                    {
                        sw.WriteLine(info[1]);
                        sw.WriteLine(info[2]);
                        sw.WriteLine(info[3]);
                        sw.WriteLine(info[4]);
                    }
                    temp.client = client;
                    temp.client_name = info[1];
                    client_list.Add(temp);
                    string login_notice = "User " + temp.client_name + " connected!";
                    add_message(login_notice);
                }
            }
            catch
            {
                client_list.Remove(temp);
                client.Close();
            }
        }

        private void check_online_user(ref Socket client, string[] info)
        {
            try
            {
                add_message("Dang o thread check");
                if (info[0] == "online")
                {
                    for (int i = 0; i < client_list.Count(); i++)
                    {
                        if (client_list[i].client_name == info[1])
                        {
                            client_list[i].set_status(true);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < client_list.Count(); i++)
                    {
                        if (client_list[i].client_name == info[1])
                        {
                            client_list.Remove(client_list[i]);
                            i--;
                        }
                    }
                }
                foreach (CLIENT item in client_list)
                {
                    add_message("Online " + item.client_name);
                }
            }
            catch
            {
                client.Close();
            }
        }
        private void senddirectoryback(ref Socket client, string[] info)
        {
            try
            {
                add_message("Dang o thread download 1");
                if (info[0] == "GETLISTFILE")
                {
                    for (int i = 0; i < client_list.Count(); i++)
                    {
                        if (client_list[i].client_name == info[1])
                        {
                            string[] file = Directory.GetFiles(@"files\");
                            client_list[i].client.Send(serialize("LISTFILE" + "|" + string.Join("$", file)));
                        }
                    }
                }
            }
            catch
            {
                client.Close();
            }
        }
        private void upload(ref Socket client, string[] info)
        {
            try
            {
                add_message("Dang o thread upload");
                if (info[0] == "DATA")
                {
                    for (int i = 0; i < client_list.Count(); i++)
                    {
                        if (client_list[i].client_name == info[1])
                        {
                            var receiveThread = new Thread(filename =>
                            {
                                var cmd = ((string)filename).Split('|');
                                var listener = new TcpListener(IPAddress.Parse(GetLocalIpAddress()), 2503);
                                var bufferSize = 1024;
                                var bytesRead = 0;
                                var allBytesRead = 0;

                                // Start listening
                                listener.Start();

                                // Accept client
                                var Client = listener.AcceptTcpClient();
                                listener.Stop();
                                var netStream = Client.GetStream();

                                // Read length of incoming data
                                var length = new byte[4];
                                bytesRead = netStream.Read(length, 0, 4);
                                var dataLength = BitConverter.ToInt32(length, 0);

                                // Read the data
                                var bytesLeft = dataLength;
                                var datas = new byte[dataLength];

                                while (bytesLeft > 0)
                                {
                                    var nextPacketSize = bytesLeft > bufferSize ? bufferSize : bytesLeft;
                                    bytesRead = netStream.Read(datas, allBytesRead, nextPacketSize);
                                    allBytesRead += bytesRead;
                                    bytesLeft -= bytesRead;
                                }

                                if (cmd[1] == "Y")
                                    datas = decrypt(datas, "dcmongtule");
                                // Save to files
                                File.WriteAllBytes("files\\" + (string)cmd[0], datas);
                                // Clean up
                                netStream.Close();
                                Client.Close();
                            });
                            receiveThread.Start(info[2] + "|" + info[3]);
                        }
                    }
                }
            }
            catch
            {
                client.Close();
            }
        }
        private void download(ref Socket client, string[] info)
        {
            try
            {
                add_message("Dang o thread download 2");
                if (info[0] == "GETFILE")
                {
                    for (int i = 0; i < client_list.Count(); i++)
                    {
                        if (client_list[i].client_name == info[1])
                        {
                            Thread sendThread = new Thread(() =>
                            {
                                string[] cmd = ((string)info[2]).Split('|');
                                var ipAddress = IPAddress.Parse(cmd[2]);
                                var port = 2503;
                                var bufferSize = 1024;
                                var client_ = new TcpClient();
                                try
                                {
                                    client_.Connect(new IPEndPoint(ipAddress, port));
                                }
                                catch
                                {
                                    client_.Close();
                                    return;
                                }

                                var netStream = client_.GetStream();
                                var datas = File.ReadAllBytes("files\\" + cmd[0]);
                                if (cmd[2] == "Y")
                                    datas = encrypt(datas, "dcmongtule");
                                // Build the package
                                var dataLength = BitConverter.GetBytes(datas.Length);
                                var package = new byte[4 + datas.Length];
                                dataLength.CopyTo(package, 0);
                                datas.CopyTo(package, 4);

                                // Send to server
                                var bytesSent = 0;
                                var bytesLeft = package.Length;

                                while (bytesLeft > 0)
                                {
                                    var nextPacketSize = bytesLeft > bufferSize ? bufferSize : bytesLeft;

                                    netStream.Write(package, bytesSent, nextPacketSize);
                                    bytesSent += nextPacketSize;
                                    bytesLeft -= nextPacketSize;
                                }

                                netStream.Flush();
                                netStream.Dispose();
                                netStream.Close();
                                client_.Close();
                                client_.Dispose();
                            });
                            sendThread.Start(info[2] + "|" + info[3] + "|" + info[4]);
                        }
                    }
                }
            }
            catch
            {
                client.Close();
            }
        }
        private void receive()
        {
            while (true)
            {
                Socket client = server_socket.Accept();
                Thread receive = new Thread(() =>
                  {
                      try
                      {
                          while (true)
                          {
                              byte[] data = new byte[1024 * 5000];
                              client.Receive(data);
                              string message = (string)deserialize(data);
                              add_message("Dang o thread receive: " + message);
                              string[] info = message.Split('|');
                              if (info[0] == "login")
                              {
                                  Thread check_login = new Thread(() =>
                                  {
                                      login(ref client, info);
                                  });
                                  check_login.IsBackground = true;
                                  check_login.Start();
                              }
                              else if (info[0] == "register")
                              {
                                  Thread check_register = new Thread(() =>
                                  {
                                      register(ref client, info);
                                  });
                                  check_register.IsBackground = true;
                                  check_register.Start();
                              }
                              else if (info[0] == "online" || info[0] == "offline")
                              {
                                  Thread check_online = new Thread(() =>
                                  {
                                      check_online_user(ref client, info);
                                  });
                                  check_online.IsBackground = true;
                                  check_online.Start();
                              }
                              else if (info[0]=="DATA")
                              {
                                  Thread upload_file = new Thread(() =>
                                  {
                                      upload(ref client, info);
                                  });
                                  upload_file.IsBackground = true;
                                  upload_file.Start();
                              }
                              else if (info[0]=="GETLISTFILE")
                              {
                                  Thread repdir = new Thread(() =>
                                  {
                                      senddirectoryback(ref client, info);
                                  });
                                  repdir.IsBackground = true;
                                  repdir.Start();
                              }
                              else if (info[0]=="GETFILE")
                              {
                                  Thread download_file = new Thread(() =>
                                  {
                                      download(ref client, info);
                                  });
                                  download_file.IsBackground = true;
                                  download_file.Start();
                              }
                          }
                      }
                      catch
                      {
                          client.Close();
                      }
                  });
                receive.IsBackground = true;
                receive.Start();
            }
        }

        private void add_message(string s)
        {
            chat_box.Items.Add(new ListViewItem() { Text = "Server: " + s });
        }

        //=================================nhan
        private string GetLocalIpAddress()
        {
            UnicastIPAddressInformation mostSuitableIp = null;
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var network in networkInterfaces)
            {
                if (network.OperationalStatus != OperationalStatus.Up)
                    continue;
                var properties = network.GetIPProperties();
                if (properties.GatewayAddresses.Count == 0)
                    continue;
                foreach (var address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    if (IPAddress.IsLoopback(address.Address))
                        continue;

                    if (!address.IsDnsEligible)
                    {
                        if (mostSuitableIp == null)
                            mostSuitableIp = address;
                        continue;
                    }
                    if (address.PrefixOrigin != PrefixOrigin.Dhcp)
                    {
                        if (mostSuitableIp == null || !mostSuitableIp.IsDnsEligible)
                            mostSuitableIp = address;
                        continue;
                    }
                    return address.Address.ToString();
                }
            }
            return mostSuitableIp != null ? mostSuitableIp.Address.ToString() : "";
        }
        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        public static byte[] encrypt(byte[] plain, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plain, 0, plain.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        public static byte[] decrypt(byte[] cipher, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
    }
}