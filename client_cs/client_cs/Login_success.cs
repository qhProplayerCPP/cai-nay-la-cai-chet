using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class Login_success : Form
    {
        public Login_success(string socket_name)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client_name = socket_name;
            connect();
        }

        private IPEndPoint ip;
        private Socket client_socket;
        private string client_name;

        private void connect()
        {
            ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2503);
            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client_socket.Connect(ip);
            }
            catch
            {
                MessageBox.Show("Cant connect to server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //==================huy
            Thread recv_from_sv = new Thread(receive);
            recv_from_sv.IsBackground = true;
            recv_from_sv.Start();
            //=====================
        }

        //==============================huy
        private void check_online_HUY()
        {
            try
            {
                client_socket.Send(serialize("online " + client_name));
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }

        private void quit_button_Click(object sender, EventArgs e)
        {
            client_socket.Send(serialize("offline " + client_name));
            this.Close();
        }

        private void chat_button_Click_1(object sender, EventArgs e)
        {
            if (userchat_textBox.Text != string.Empty && userchat_textBox.Text != client_name)
            {
                client_socket.Send(serialize("chat " + client_name + " " + userchat_textBox.Text));
            }
            else
            {
                MessageBox.Show("Input invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void receive()
        {
            check_online_HUY();
            Thread receive = new Thread(() =>
              {
                  try
                  {
                      while (true)
                      {
                          byte[] data = new byte[1024 * 5000];
                          client_socket.Receive(data);
                          string message = (string)deserialize(data);
                          string[] info = message.Split(' ');
                          MessageBox.Show(message);
                          if (info[0] == "online" || info[0] == "offline")
                          {
                              Thread is_online = new Thread(() =>
                                {
                                    if (info[0] == "online")
                                    {
                                        Application.Run(new client(client_name, userchat_textBox.Text));
                                    }
                                    else
                                    {
                                        MessageBox.Show("This user doesn't online/exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                });
                              is_online.IsBackground = true;
                              is_online.Start();
                          }
                          else if (info[0] == "message")
                          {
                              string[] info1 = message.Split('|');
                              string[] temp = info1[0].Split(' ');
                              string sender = temp[1];
                              string receiver = info1[1];

                              Thread open = new Thread(() =>
                                {
                                    Application.Run(new client(receiver, sender));
                                });
                              open.IsBackground = true;
                              open.Start();
                          }
                      }
                  }
                  catch
                  {
                      client_socket.Close();
                  }
              });
            receive.IsBackground = true;
            receive.Start();
        }

        //=================================
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

        private void changepass_button_Click(object sender, EventArgs e)
        {
        }

        private void setup_button_Click(object sender, EventArgs e)
        {
        }
    }
}