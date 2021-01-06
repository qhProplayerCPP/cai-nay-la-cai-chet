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
        public Login_success(string socket_name, string ip_addr)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client_name = socket_name;
            ip_address = ip_addr;
            connect();
        }

        private IPEndPoint ip;
        private Socket client_socket;
        private string client_name,ip_address;

        //==============================huy
        private void connect()
        {
            ip = new IPEndPoint(IPAddress.Parse(ip_address), 2503);
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
            Thread receive_from_sv = new Thread(receive);
            receive_from_sv.IsBackground = true;
            receive_from_sv.Start();
            //=====================
        }

        private void Login_success_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = client_name;
                client_socket.Send(serialize("online" + "|" + client_name));
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }

        private void chat_button_Click_1(object sender, EventArgs e)
        {
            if (userchat_textBox.Text != string.Empty && userchat_textBox.Text != client_name)
            {
                client_socket.Send(serialize("chat|" + client_name + "|" + userchat_textBox.Text));
            }
            else
            {
                MessageBox.Show("Input invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_success_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                client_socket.Send(serialize("offline" + "|" + client_name));
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }

        private void receive()
        {
            Thread receive = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        byte[] data = new byte[1024 * 5000];
                        client_socket.Receive(data);
                        string message = (string)deserialize(data);
                        string[] info = message.Split('|');
                        if (info[0] == "chat")
                        {
                            Thread chat_thread = new Thread(() =>
                              {
                                  Application.Run(new Client(info[1], info[2],ip_address));
                              });
                            chat_thread.IsBackground = true;
                            chat_thread.Start();
                        }
                        else if (info[0] == "cantchat")
                        {
                            MessageBox.Show("This user does not online/exist");
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

        //============================== Hien
        private void changepass_button_Click(object sender, EventArgs e)
        {
            ChangePass form = new ChangePass(client_name);
            form.ShowDialog();
        }

        //==================================

        private void setup_button_Click(object sender, EventArgs e)
        {
            Setup form = new Setup(client_name);
            form.ShowDialog();
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            FileHandler filehandle = new FileHandler(client_name, ip_address);
            filehandle.ShowDialog();
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            CheckUser form = new CheckUser();
            form.ShowDialog();
        }

        private void userchat_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}