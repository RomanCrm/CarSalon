using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Salon
{
    public partial class Form1 : Form
    {
        BindingList<Car> cars = new BindingList<Car>();
        string pathForXml = "carShowroom.xml";

        public Form1()
        {
            InitializeComponent();
            listBox1.DataSource = cars;
            listBox1.DisplayMember = "Model";

            button3.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Editor editor = new Editor();
            editor.TextChangedEvent += Editor_TextChangedEvent;
            editor.Show();
        }

        private void Editor_TextChangedEvent(object sender, EventArgs e)
        {
            Editor editor = sender as Editor;
            cars.Add(new Car { Id = editor.Id, Model = editor.Model, Price = editor.Price, Sold = editor.Sold });
            button3.Enabled = true;
            button2.Enabled = true;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                Editor editor = new Editor(cars[listBox1.SelectedIndex].Id, cars[listBox1.SelectedIndex].Model, cars[listBox1.SelectedIndex].Price, cars[listBox1.SelectedIndex].Sold);
                editor.TextUpdateEvent += Editor_TextUpdateEvent;
                editor.TextDeleteEvent += Editor_TextDeleteEvent;
                editor.Show();
            }
        }

        private void Editor_TextDeleteEvent(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                cars.Remove(cars[listBox1.SelectedIndex]);
            }
        }

        private void Editor_TextUpdateEvent(object sender, EventArgs e)
        {
            Editor editor = sender as Editor;
            if (listBox1.SelectedIndex >= 0)
            {
                cars[listBox1.SelectedIndex].Id = editor.Id;
                cars[listBox1.SelectedIndex].Model = editor.Model;
                cars[listBox1.SelectedIndex].Price = editor.Price;
                cars[listBox1.SelectedIndex].Sold = editor.Sold;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cars.Remove(cars[listBox1.SelectedIndex]);
            }
        }

        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (cars.Count > 0)
            {
                XmlSerializer serializator = new XmlSerializer(typeof(BindingList<Car>));
                using (Stream stream = new FileStream(pathForXml, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    serializator.Serialize(stream, cars);
                }
            }
        }

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            XmlSerializer serializator = new XmlSerializer(typeof(BindingList<Car>));
            using (Stream stream = new FileStream(pathForXml, FileMode.Open, FileAccess.Read))
            {
                cars = serializator.Deserialize(stream) as BindingList<Car>;
            }
            listBox1.DataSource = cars;
            listBox1.DisplayMember = "Model";
        }

        public class Car : INotifyPropertyChanged
        {
            private int id;
            public int Id
            {
                get => id;
                set
                {
                    id = value;
                    if (PropertyChanged == null)
                    { }
                    else
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                    }
                }
            }

            private string model;
            public string Model
            {
                get => model;
                set
                {
                    model = value;
                    if (PropertyChanged == null)
                    { }
                    else
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                    }
                }
            }

            private decimal price;
            public decimal Price
            {
                get => price;
                set
                {
                    price = value;
                    if (PropertyChanged == null)
                    { }
                    else
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                    }
                }
            }

            private bool sold;
            public bool Sold
            {
                get => sold;
                set
                {
                    sold = value;
                    if (PropertyChanged == null)
                    { }
                    else
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}