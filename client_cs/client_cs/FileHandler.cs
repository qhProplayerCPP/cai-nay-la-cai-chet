﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace client_cs
{
    public partial class FileHandler : Form
    {
        public FileHandler(string socket_name)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client_name = socket_name;
            connect();
        }
        private Socket client_socket;
        private IPEndPoint ip;
        private string client_name;
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
        }
        private string GetLocalIpAddress()
        {
            UnicastIPAddressInformation mostSuitableIp = null;
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var network in networkInterfaces)
            {
                if (network.OperationalStatus != OperationalStatus.Up)
                    continue;
                var properties = network.GetIPProperties();
                if (properties.GatewayAddresses.Count == 0)
                    continue;
                foreach (var address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    if (IPAddress.IsLoopback(address.Address))
                        continue;
                    if (!address.IsDnsEligible)
                    {
                        if (mostSuitableIp == null)
                            mostSuitableIp = address;
                        continue;
                    }
                    if (address.PrefixOrigin != PrefixOrigin.Dhcp)
                    {
                        if (mostSuitableIp == null || !mostSuitableIp.IsDnsEligible)
                            mostSuitableIp = address;
                        continue;
                    }
                    return address.Address.ToString();
                }
            }
            return mostSuitableIp != null ? mostSuitableIp.Address.ToString() : "";
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

        public static byte[] decrypt(byte[] cipher, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
        private void upload()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    var dir = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                    var frmNewName = new newfilename { StartPosition = FormStartPosition.CenterParent };
                    if (frmNewName.ShowDialog() == DialogResult.OK)
                    {
                        if (frmNewName.textboxnewname.Text.Trim().Length == 0)
                        {
                            MessageBox.Show("File name cannot be blank. Old file name is kept", "Notification",
                                MessageBoxButtons.OK);
                        }
                        else
                        {
                            dir = dir.Trim();
                            if (dir.Contains('.'))
                                dir = dir.Substring(dir.IndexOf('.'));
                            else dir = string.Empty;
                            dir = frmNewName.textboxnewname.Text + dir;
                        }
                    }

                    var dia = MessageBox.Show("Do you want to encrypt?", "Notification", MessageBoxButtons.YesNo);
                    object message = "DATA" + "|" + client_name + "|" + dir + "|" + (dia == DialogResult.Yes ? "Y" : "N");
                    client_socket.Send(serialize(message));
                    Thread sendThread = new Thread(directory =>
                    {
                        var cmd = ((string)directory).Split('|');
                        var ipAddress = IPAddress.Parse(GetLocalIpAddress());
                        var port = 2503;
                        var bufferSize = 1024;
                        var client = new TcpClient();
                        try
                        {
                            client.Connect(new IPEndPoint(ipAddress, port));
                        }
                        catch
                        {
                            client.Close();
                            return;
                        }

                        var netStream = client.GetStream();
                        var data = File.ReadAllBytes(cmd[0]);
                        //if (cmd[1] == "Y")
                        data = encrypt(data, "dcmongtule");
                        // Build the package
                        var dataLength = BitConverter.GetBytes(data.Length);
                        var package = new byte[4 + data.Length];
                        dataLength.CopyTo(package, 0);
                        data.CopyTo(package, 4);

                        // Send to server
                        var bytesSent = 0;
                        var bytesLeft = package.Length;

                        while (bytesLeft > 0)
                        {
                            var nextPacketSize = bytesLeft > bufferSize ? bufferSize : bytesLeft;

                            netStream.Write(package, bytesSent, nextPacketSize);
                            bytesSent += nextPacketSize;
                            bytesLeft -= nextPacketSize;
                        }

                        netStream.Flush();
                        netStream.Dispose();
                        netStream.Close();
                        client.Close();
                        client.Dispose();
                    });
                    sendThread.Start(openFileDialog.FileName + "|" + (dia == DialogResult.Yes ? "Y" : "N"));
                }
            }
            catch
            {
                MessageBox.Show("Disconnect from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client_socket.Close();
            }
        }
        private void uploadbutton_Click(object sender, EventArgs e)
        {
            Thread newupThread = new Thread(new ThreadStart(upload));
            newupThread.SetApartmentState(ApartmentState.STA);
            newupThread.Start();
        }
        private void downloadbutton_Click(object sender, EventArgs e)
        {
            client_socket.Send(serialize("GETLISTFILE" + "|" + client_name));
            byte[] data = new byte[1024 * 5000];
            string message;
            while (data != null)
            {
                client_socket.Receive(data);
            }
            
            message = (string)deserialize(data);
            var dataArray = message.Split('|');
            if (dataArray[0] == "LISTFILE")
            {
                string[] files = dataArray[1].Split('$');
                var frmListFile = new FileList { StartPosition = FormStartPosition.CenterParent };
                foreach (var item in files)
                {
                    if (item != string.Empty)
                        frmListFile.listboxlistfile.Items.Add(item.Substring(item.IndexOf('\\') + 1));
                }

                if (frmListFile.ShowDialog(this) == DialogResult.OK)
                {
                    var dialogResult = MessageBox.Show(this, "Do you want to encrypt before downloading?", "Notification", MessageBoxButtons.YesNo);
                    client_socket.Send(serialize("GETFILE" + "|" + client_name + "|" + (frmListFile.listboxlistfile.GetItemText(frmListFile.listboxlistfile.SelectedItem)) + "|" + GetLocalIpAddress() + "|" + (dialogResult == DialogResult.Yes ? "Y" : "N")));
                    var receiveThread = new Thread(filename =>
                    {
                        var cmd = ((string)filename).Split('|');
                        var listener = new TcpListener(IPAddress.Parse("192.168.1.3"), 2505);
                        var bufferSize = 1024;
                        var bytesRead = 0;
                        var allBytesRead = 0;

                        // Start listening
                        listener.Start();

                        // Accept client
                        var client = listener.AcceptTcpClient();
                        listener.Stop();
                        var netStream = client.GetStream();

                        // Read length of incoming data
                        var length = new byte[4];
                        bytesRead = netStream.Read(length, 0, 4);
                        var dataLength = BitConverter.ToInt32(length, 0);

                        // Read the data
                        var bytesLeft = dataLength;
                        var datas = new byte[dataLength];

                        while (bytesLeft > 0)
                        {
                            var nextPacketSize = bytesLeft > bufferSize ? bufferSize : bytesLeft;
                            bytesRead = netStream.Read(datas, allBytesRead, nextPacketSize);
                            allBytesRead += bytesRead;
                            bytesLeft -= bytesRead;
                        }

                        if (cmd[1] == "Y")
                            datas = decrypt(datas, "dcmongtule"); 
                        // Save to files
                        var frmNewName = new newfilename { StartPosition = FormStartPosition.CenterParent };
                        if (frmNewName.ShowDialog(this) == DialogResult.Yes &&
                            frmNewName.Text.Trim() != string.Empty)
                        {
                            if (cmd[0].Contains('.'))
                                cmd[0] = cmd[0].Substring(cmd[0].LastIndexOf('.'));
                            cmd[0] = frmNewName.textboxnewname.Text.Trim() + cmd[0];
                        }
                        else
                            MessageBox.Show(this, "New Name is invalid. Old file name is kept", "Notification", MessageBoxButtons.OK);
                        File.WriteAllBytes("received\\" + cmd[0], datas);
                        // Clean up
                        netStream.Close();
                        client.Close();
                    });
                    receiveThread.Start((frmListFile.textboxnewname.Text != string.Empty ? frmListFile.textboxnewname.Text : (string)frmListFile.listboxlistfile.SelectedItem) + "|" + (dialogResult == DialogResult.Yes ? "Y" : "N"));
                }
            }
        }
    }
}

