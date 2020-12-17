using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace server_cs
{
    public partial class server : Form
    {
        //khoi tao server
        public server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            get_data(ref all_users);
            connect();
        }
        //khai bao bien
        IPEndPoint ip;
        Socket server_socket;
        List<Socket> client_list;
        List<user_info> all_users;
        struct user_info
        {
            public string username;
            public string password;
            public string fullname;
            public string dob;
        }
        //ham doc file
        void get_data(ref List<user_info> all_users)
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
        //ham de client ket noi den server
        void connect()
        {
            client_list = new List<Socket>();
            ip = new IPEndPoint(IPAddress.Any, 2503);
            server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
            server_socket.Bind(ip);
            server_socket.Listen(10);

            Thread check_login = new Thread(login);
            check_login.IsBackground = true;
            check_login.Start();

            Thread check_reg = new Thread(register);
            check_reg.IsBackground = true;
            check_reg.Start();
        }
        //login
        void login()
        {
            while (true)
            {
                Socket client = server_socket.Accept();
                Thread check_user = new Thread(() => {
                    try
                    {
                        while (true)
                        {
                            byte[] data = new byte[1024 * 5000];
                            client.Receive(data);
                            string message = (string)deserialize(data);
                            add_message(message);
                            string[] info = message.Split(' ');
                            bool check = false;
                            if (info[0] != "login")
                                return;
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
                            }
                            else
                            {
                                client.Send(serialize("false"));
                            }
                        }
                    }
                    catch
                    {
                        client_list.Remove(client);
                        client.Close();
                    }
                });
                check_user.IsBackground = true;
                check_user.Start();
            }
        }
        //register
        void register()
        {
            while (true)
            {
                Socket client = server_socket.Accept();
                Thread check_user = new Thread(() => {
                    try
                    {
                        while (true)
                        {
                            byte[] data = new byte[1024 * 5000];
                            client.Receive(data);
                            string message = (string)deserialize(data);
                            add_message(message);
                            string[] info = message.Split(' ');
                            bool check = true;
                            if (info[0] != "register")
                                return;
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
                            }
                        }
                    }
                    catch
                    {
                        client_list.Remove(client);
                        client.Close();
                    }
                });
                check_user.IsBackground = true;
                check_user.Start();
            }
        }
        //ham dong server
        void close()
        {
            server_socket.Close();
        }
        //cai nay cua code mau~ ma chua can toi nen chua dung
        void send(Socket client)
        {
            
        }
        //cai nay cung cua code mau chua dung toi
        void receive(object obj)
        {
            Socket client = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);
                    string message = (string)deserialize(data);
                    add_message(message);
                    foreach (Socket item in client_list)
                    {
                        if (item != null && item != client)
                            item.Send(serialize(message));
                    }
                }
            }
            catch
            {
                client_list.Remove(client);
                client.Close();
            }
        }
        //2 cai nay de hien thi tin nhan thoi
        void add_message(string s)
        {
            chat_box.Items.Add(new ListViewItem() { Text = "Server: " + s });
        }
        //ham phan manh
        byte[] serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
        //ham gom manh
        object deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
    }
}
