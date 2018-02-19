using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp_WindowsForm
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            iPv4AddressTextBox1.Text = textBox1.Text;
            textBox2.Text = iPv4AddressTextBox1.Text;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            iPv4AddressTextBox1.Text = textBox1.Text;
            textBox2.Text = iPv4AddressTextBox1.Text;
        }
    }
}