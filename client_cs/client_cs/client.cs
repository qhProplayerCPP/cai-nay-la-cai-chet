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
    public partial class client : Form
    {
        //cai nay chua dung toi nhieu
        public client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            connect();
        }
        private void send_button_Click(object sender, EventArgs e)
        {
            send();
            add_message(type_box.Text);
        }
        IPEndPoint ip;
        Socket client_socket;
        void connect()
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
            //Thread listen = new Thread(receive);
            //listen.IsBackground = true;
            //listen.Start();
        }
        void close()
        {
            client_socket.Close();
        }
        void send()
        {
            if (type_box.Text != string.Empty)
                client_socket.Send(serialize(type_box.Text)); 
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
                    add_message_from_server(message);
                }
            }
            catch
            {
                close();
            }
        }
        void add_message(string s)
        {
            chat_box.Items.Add(new ListViewItem() { Text = "Client: " + s });
            type_box.Clear();
        }
        void add_message_from_server(string s)
        {
            chat_box.Items.Add(new ListViewItem() { Text = "Server: " + s });
            type_box.Clear();
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

        private void client_Load(object sender, EventArgs e)
        {

        }
    }
}
