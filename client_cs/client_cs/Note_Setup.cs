using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class Note_Setup : Form
    {
        public Note_Setup(string socket_name, string ip_addr)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client_name = socket_name;
            ip_address = ip_addr;
            connect();
        }

        private IPEndPoint ip;
        private Socket client_socket;
        private string client_name, ip_address;

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
            Thread listen = new Thread(receive);
            listen.IsBackground = true;
            listen.Start();
        }

        private void receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    client_socket.Receive(data);
                    string message = (string)deserialize(data);
                    if (message == "true")
                    {
                        MessageBox.Show("Updated note successfully!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This user is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
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

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void setupNote_button_Click(object sender, EventArgs e)
        {
            if (Note_textBox.Text != string.Empty)
            {
                IPAddress[] iptemp = Dns.GetHostAddresses(Dns.GetHostName());
                object message = "Note" + "|" + client_name + "|" + Note_textBox.Text + "|" + iptemp[1].ToString();
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
