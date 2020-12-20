using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

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
                              string[] info = message.Split(' ');
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
    }
}