﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            connect();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text != string.Empty && password_textBox.Text != string.Empty)
            {
                object message = "login" + "|" + username_textBox.Text + "|" + password_textBox.Text;
                client_socket.Send(serialize(message));
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IPEndPoint ip;
        private Socket client_socket;

        private void connect()
        {
            ip = new IPEndPoint(IPAddress.Parse("192.168.1.3"), 2503);
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
                        Application.Run(new Login_success(username_textBox.Text));
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

        private void reg_button_Click(object sender, EventArgs e)
        {
            Register form = new Register();
            form.ShowDialog();
        }
    }
}