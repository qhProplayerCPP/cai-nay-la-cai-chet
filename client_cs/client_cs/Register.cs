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

namespace client_cs
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            connect();
        }
        IPEndPoint ip;
        Socket client_socket;
        void connect()
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
            Thread listen = new Thread(receive);
            listen.IsBackground = true;
            listen.Start();
        }
        void receive()
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
                        Application.Run(new Login_success());
                    }
                    else
                    {
                        MessageBox.Show("This user existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }
        byte[] serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
        object deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }  
        private void register_button_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty)
            {
                object message = "register " + textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " " + textBox4.Text;
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("This user is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
