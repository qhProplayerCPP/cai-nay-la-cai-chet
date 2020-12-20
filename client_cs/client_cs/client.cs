using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class client : Form
    {
        public client(string s, string r)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            sender = s;
            receiver = r;
            connect();
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            send();
            add_message(type_box.Text);
        }

        private IPEndPoint ip;
        private Socket client_socket;
        private string sender, receiver;

        private void connect()
        {
            ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2503);
            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
            try
            {
                client_socket.Connect(ip);
            }
            catch
            {
                MessageBox.Show("Cant connect to server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            Thread listen = new Thread(receive);
            listen.IsBackground = true;
            listen.Start();
        }

        private void send()
        {
            if (type_box.Text != string.Empty)
                client_socket.Send(serialize("message " + sender + "|" + receiver + "|" + type_box.Text));
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
                        string[] temp = info[0].Split(' ');
                        string text = info[2];
                        string sender1 = temp[1];
                        string receiver1 = info[1];
                        if (temp[0] == "message" && sender1 == receiver && receiver1 == sender)
                        {
                            add_message_from_server(sender1 + ": " + text);
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

        private void add_message(string s)
        {
            chat_box.Items.Add(new ListViewItem() { Text = s });
            type_box.Clear();
        }

        private void add_message_from_server(string s)
        {
            chat_box.Items.Add(new ListViewItem() { Text = s });
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

        private void client_Load(object sender, EventArgs e)
        {
        }
    }
}