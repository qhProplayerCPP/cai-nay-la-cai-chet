using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class Client : Form
    {
        public Client(string s, string r)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            sender = s;
            receiver = r;
            connect();
        }

        private IPEndPoint ip;
        private Socket client_socket;
        private string sender, receiver;

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
            Thread receive_from_sv = new Thread(receive);
            receive_from_sv.IsBackground = true;
            receive_from_sv.Start();
        }

        private void Client_Load(object send, EventArgs e)
        {
            try
            {
                this.Text = sender + "-" + receiver;
                client_socket.Send(serialize("on_chatbox" + "|" + sender + "|" + receiver));
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }

        private void Client_FormClosed(object send, FormClosedEventArgs e)
        {
            try
            {
                client_socket.Send(serialize("off_chatbox" + "|" + sender + "|" + receiver));
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }

        private void send_button_Click(object send, EventArgs e)
        {
            if (typebox.Text != string.Empty)
            {
                client_socket.Send(serialize("message|" + sender + "|" + receiver + "|" + typebox.Text));
                add_message(typebox.Text);
                typebox.Clear();
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
                        if (info[0] == "receiver_off")
                        {
                            MessageBox.Show(receiver + " has disconnected from the chat");
                            this.Close();
                        }
                        else if (info[0] == "message")
                        {
                            add_message_from_sv(info[3]);
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

        private void add_message(string s)
        {
            chatbox.Items.Add(new ListViewItem() { Text = s });
        }

        private void chatbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void add_message_from_sv(string s)
        {
            chatbox.Items.Add(new ListViewItem() { Text = receiver + ": " + s });
        }
    }
}