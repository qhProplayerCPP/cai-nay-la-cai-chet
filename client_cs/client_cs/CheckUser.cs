using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class CheckUser : Form
    {
        public CheckUser()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
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
            Thread listen = new Thread(receive);
            listen.IsBackground = true;
            listen.Start();
        }

        //private void receive()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            byte[] data = new byte[1024 * 5000];
        //            client_socket.Receive(data);
        //            string message = (string)deserialize(data);
        //            if (message == "true")
        //            {
        //                MessageBox.Show("This user exists!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                this.Close();
        //            }
        //            else
        //            {
        //                MessageBox.Show("This user is not exists!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        client_socket.Close();
        //    }
        //}
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
                        if (info[0] == "false find")
                        {
                            MessageBox.Show("This user is not exists!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (info[0] == "true find")
                        {
                            MessageBox.Show("This user exists!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (info[0] == "false showdate")
                        {
                            MessageBox.Show("This user is not exists!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (info[0] == "true showdate")
                        {
                            MessageBox.Show(info[1], "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (info[0] == "false showname")
                        {
                            MessageBox.Show("This user is not exists!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (info[0] == "true showname")
                        {
                            MessageBox.Show(info[1], "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (info[0] == "false showall")
                        {
                            MessageBox.Show("This user is not exists!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (info[0] == "true showall")
                        {
                            MessageBox.Show("Username: " + info[1] + Environment.NewLine + "Fullname: " + info[2] + Environment.NewLine + "D.O.B: " + info[3], "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (info[0] == "false online")
                        {
                            MessageBox.Show("This user is not online/exist!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (info[0] == "true online")
                        {
                            MessageBox.Show("This user is online now!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text != string.Empty)
            {
                IPAddress[] iptemp = Dns.GetHostAddresses(Dns.GetHostName());
                object message = "FindUser" + "|"  + username_textBox.Text + "|" + iptemp[1].ToString();
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text != string.Empty)
            {
                IPAddress[] iptemp = Dns.GetHostAddresses(Dns.GetHostName());
                object message = "CheckOnline" + "|" + username_textBox.Text + "|" + iptemp[1].ToString();
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text != string.Empty)
            {
                IPAddress[] iptemp = Dns.GetHostAddresses(Dns.GetHostName());
                object message = "ShowDate" + "|" + username_textBox.Text + "|" + iptemp[1].ToString();
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text != string.Empty)
            {
                IPAddress[] iptemp = Dns.GetHostAddresses(Dns.GetHostName());
                object message = "ShowFullname" + "|" + username_textBox.Text + "|" + iptemp[1].ToString();
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text != string.Empty)
            {
                IPAddress[] iptemp = Dns.GetHostAddresses(Dns.GetHostName());
                object message = "ShowAll" + "|" + username_textBox.Text + "|" + iptemp[1].ToString();
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckUser_Load(object sender, EventArgs e)
        {

        }

    }
}
