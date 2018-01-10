using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Salon
{
    public partial class Editor : Form
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public bool Sold { get; set; }

        public event EventHandler TextChangedEvent;
        public event EventHandler TextUpdateEvent;
        public event EventHandler TextDeleteEvent;

        public Editor()
        {
            InitializeComponent();

            button2.Enabled = false;
            button3.Enabled = false;
        }

        public Editor(int id, string model, decimal price, bool sold) : this()
        {
            numericUpDown2.Value = id;
            textBox2.Text = model;
            numericUpDown1.Value = price;
            checkBox1.Checked = sold;

            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Id = (int)numericUpDown2.Value;
            Model = textBox2.Text;
            Price = numericUpDown1.Value;
            Sold = checkBox1.Checked;
            TextChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Id = (int)numericUpDown2.Value;
            Model = textBox2.Text;
            Price = numericUpDown1.Value;
            Sold = checkBox1.Checked;
            TextUpdateEvent?.Invoke(this, EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextDeleteEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}