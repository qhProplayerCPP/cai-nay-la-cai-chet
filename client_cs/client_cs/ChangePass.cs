using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class ChangePass : Form
    {
        public ChangePass(string socket_name, string ip_addr)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client_name = socket_name;
            ip_address = ip_addr;
            connect(ip_address);
        }

        private IPEndPoint ip;
        private Socket client_socket;
        private string client_name, ip_address;
        private int connect(string ip_box)
        {
            ip = new IPEndPoint(IPAddress.Parse(ip_box), 2503);
            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client_socket.Connect(ip);
            }
            catch
            {
                MessageBox.Show("Cant connect to server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            Thread listen = new Thread(receive);
            listen.IsBackground = true;
            listen.Start();
            return 1;
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
                        MessageBox.Show("Change password successfully!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void ChangePass_Load(object sender, EventArgs e)
        {

        }

        private void oldpassword_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (client_name != string.Empty && oldpassword_textBox.Text != string.Empty && newpassword_textBox.Text != string.Empty)
            {
                int check = connect(ip_address);
                if (check == 1)
                {
                    var dia = MessageBox.Show("Do you want to encrypt?", "Notification", MessageBoxButtons.YesNo);
                    if (dia == DialogResult.Yes)
                    {
                        byte[] newpass = encrypt(Encoding.ASCII.GetBytes(newpassword_textBox.Text), "dcmongtule");
                        string s = System.Text.Encoding.UTF8.GetString(newpass, 0, newpass.Length);
                        object message = "changepass" + "|" + client_name + "|" + oldpassword_textBox.Text + "|" + newpassword_textBox.Text + "|" + s + "|Y";
                        client_socket.Send(serialize(message));
                    }
                    else
                    {
                        object message = "changepass" + "|" + client_name + "|" + oldpassword_textBox.Text + "|" + newpassword_textBox.Text + "|N";
                        client_socket.Send(serialize(message));
                    }
                }
                else
                {
                    MessageBox.Show("Server IP invalid");
                }
            }
            else
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
        public static byte[] encrypt(byte[] plain, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plain, 0, plain.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
    }
}
