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
            Thread check_online = new Thread(check_online_HUY);
            check_online.IsBackground = true;
            check_online.Start();
            //=====================
        }

        //==============================huy
        private void check_online_HUY()
        {
            try
            {
                client_socket.Send(serialize("online" + "|" + client_name));
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }

        private void quit_button_Click(object sender, EventArgs e)
        {
            client_socket.Send(serialize("offline" + "|" + client_name));
            this.Close();
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

        private void chat_button_click(object sender, EventArgs e)
        {
        }

        private void changepass_button_Click(object sender, EventArgs e)
        {
        }

        private void setup_button_Click(object sender, EventArgs e)
        {
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            FileHandler filehandle = new FileHandler(client_name);
            filehandle.ShowDialog();
        }
    }
}