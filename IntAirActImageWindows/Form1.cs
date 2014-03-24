using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IntAirAct;

namespace IntAirActImageWindows
{
    public partial class Form1 : Form
    {
        private IADevice device;
        private IAIntAirAct intAirAct; 

        public Form1()
        {
            InitializeComponent();
        }

        public void LoadImageFromURL(String url)
        {
            this.pictureBox1.LoadAsync(url);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // Cannot submit an empty field into the list
            if (textBox1.Text.Length > 0)
            {
                listBox1.Items.Add(textBox1.Text);
                textBox1.Text = null;
            }
        }

        // Sets the device which the listbox should send to. This happens when the device is detected.
        public void setDevice(IADevice readyDevice)
        {
            this.device = readyDevice; 
        }

        public void setIntAirAct(IAIntAirAct intAirAct)
        {
            this.intAirAct = intAirAct;
        }

        public ListBox getListBox()
        {
            return listBox1; 
        }


        public ListBox getListBox2()
        {
            return listBox2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.device != null)
            {
                intAirAct.SendRequest(makeRequest(), device);
            }
            else
            {
                

            }
        }

        private IARequest makeRequest()
        {

            // Build request 
            IARequest request = new IARequest(new IARoute("PUT", "/dictionary"));
            //listBox1.Items.Add("hello");
            //listBox1.Items.Add("hi");
            request.SetBodyWith(listBox1.Items);

            return request; 

        }

    }
}
