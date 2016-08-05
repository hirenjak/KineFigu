using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Kinect;

namespace TestKinect
{
    public partial class Form1 : Form
    {
        KinectSensor kSensor;
        Body[] bodies;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initilize();
        }

        private void bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using(var bFrame = e.FrameReference.AcquireFrame())
            {
                if(bFrame == null) { return; }

                bFrame.GetAndRefreshBodyData(bodies);
            }

            foreach(var value in bodies)
            {
                if(value.LeanTrackingState == TrackingState.Tracked)
                {
                    textBox1.Text = value.Joints[JointType.HandLeft].Position.X.ToString();
                    pictureBox1.Location = new Point((int)(value.Joints[JointType.HandRight].Position.X * 100.0f) + 100, (int)(value.Joints[JointType.HandRight].Position.Y * 100.0f) * -1 + 100);
                }
            }
        }

        private void Initilize()
        {
            try
            {
                kSensor = KinectSensor.GetDefault();

                if (kSensor == null) { throw new Exception("エラー：Kinectが開けません"); }


                kSensor.Open();

                bodies = new Body[kSensor.BodyFrameSource.BodyCount];

                var bFrameReader = kSensor.BodyFrameSource.OpenReader();
                bFrameReader.FrameArrived += bodyFrameReader_FrameArrived;

            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
                Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
