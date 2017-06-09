using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Media;

namespace AquaSensor
{
    public partial class AquaSensorWindow : Form
    {
        SerialPort sp = new SerialPort();
        int i = 0;
        string x;
        int wart;
        public AquaSensorWindow()
        {
            InitializeComponent();
            sp.PortName = "COM3";
            sp.BaudRate = 9600;
            chart1.Series.Add("series1");
        //chart1.Series["series1"].Color = Color.Red;
    }
  //  public static SystemSound Beep { get; }

    private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            try
            {
                x = sp.ReadLine();
                label3.Text = x;
                wart = int.Parse(x);

               if (1050 - wart > 0)
               {
                        chart1.Series["series1"].Points.AddXY(i, 1050 - wart);
                if (1050 - wart < 100)
                {
                    chart1.Series["series1"].Color = Color.Red;

                        //System.Media.SystemSounds.Beep.Play();
                        //System.Media.SystemSounds.Asterisk.Play();
                        try
                        {
                            SoundPlayer simpleSound = new SoundPlayer(@"‪alert.wav");
                            simpleSound.Play();
                        }catch {

                        }
                    }
                else
                {
                    chart1.Series["series1"].Color = Color.Green;
                }


                if (1050-wart > 300)
                    label3.ForeColor = System.Drawing.Color.LightGreen;
                else if (1050 - wart > 200)
                    label3.ForeColor = System.Drawing.Color.Yellow;
                else if (1050 - wart > 100)
                    label3.ForeColor = System.Drawing.Color.IndianRed;
                else if (1050 - wart > 60)
                    label3.ForeColor = System.Drawing.Color.Red;
                else
                    label3.ForeColor = System.Drawing.Color.DarkRed;
            }
            }
            catch
            {
                label3.Text = "NO SIGNAL";
                try
                {
                    sp.Open();
                }
                catch
                {

                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (sp.IsOpen == false)
            {
                try
                {
                    sp.Open();
                    timer1.Start();
                }
                catch
                {
                    label3.Text = "NO SIGNAL";
                }
            }

        }
    }
}
