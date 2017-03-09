using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace event_timerExample
{
    public partial class Form1 : Form
    {
        CDrawer gdi = new CDrawer(400,400);
        List<int> xLocations = new List<int>();
        List<int> yLocations = new List<int>();

        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // this is the important stuff for the timer to start it
            //whatever is on the other side of the Tick += is what will get done/checked everytime the timer goes off
            //it has to be a method and must accept object sender and eventargs e
            timer.Tick += Draw;
            timer.Interval = 200;
            timer.Enabled = true;
            timer.Start();

        }
        private void Draw(object sender, EventArgs e)
        {
            Random rng = new Random();
            int locationX = rng.Next(400);
            int locationY = rng.Next(400);
            gdi.AddCenteredEllipse(locationX, locationY, 3, 3, Color.Red, 0, Color.Red);
            listBox1.Items.Add("Dot drawn at: X = " + locationX + " and Y = " + locationY);
            xLocations.Add(locationX);
            yLocations.Add(locationY);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this is all you need to do to stop it
            timer.Enabled = false;
            timer.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(textBox1.Text);
            listBox1.SelectedIndex = value;
        }

        //this is for saving using file stream and binary formatter via serialization
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("circles.bin", FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();

                //this is if it isn't serialized
                //BinaryWriter bw = new BinaryWriter(fs);
                //foreach (int iValue in m_iArray)
                //    bw.Write(iValue);
                //bw.Close();

                bf.Serialize(fs, xLocations);
                fs.Close();
            }
            catch
            {

            }
        }

        //this is for loading files that have been serialized
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("circles.bin", FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();

                //this is for reading non-serialized items, it reads the items out in bytes specifically
                //BinaryReader br = new BinaryReader(fs);

                xLocations = (List<int>)bf.Deserialize(fs);

                fs.Close();
            }
            catch
            {

            }
        }
    }
}
