using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace client_cs
{
    public partial class Setup : Form
    {
        public Setup(string socket_name)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client_name = socket_name;
        }

        private string client_name;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Fullname__setup_ form = new Fullname__setup_(client_name);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Date__setup_ form = new Date__setup_(client_name);
            form.ShowDialog();
        }
        private void Setup_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
