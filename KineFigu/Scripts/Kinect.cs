using Microsoft.Kinect;

namespace KineFigu
{
    class Kinect
    {
        KinectSensor kinect;
        Body[] bodies;
        Joint[] joints;

        Vector2PLUS leftHand;
        Vector2PLUS rightHand;

        Vector2PLUS[] rHandPosi;


        public Kinect()
        {
            kinect = KinectSensor.GetDefault();

            kinect.Open();

            bodies = new Body[kinect.BodyFrameSource.BodyCount];

            kinect.BodyFrameSource.OpenReader().FrameArrived += bFrameReader;

            leftHand = new Vector2PLUS();
            rightHand = new Vector2PLUS();

            rHandPosi = new Vector2PLUS[10];
        }

        public void Stop()
        {
            kinect.Close();
        }


        private void bFrameReader(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var bFrame = e.FrameReference.AcquireFrame())
            {
                if (bFrame == null) { return; }
                bFrame.GetAndRefreshBodyData(bodies);
            }

            foreach(var value in bodies)
            {
                if (value.IsTracked)
                {
                    leftHand = new Vector2PLUS(value.Joints[JointType.HandLeft].Position.X, value.Joints[JointType.HandLeft].Position.Y);
                    rightHand = new Vector2PLUS(value.Joints[JointType.HandRight].Position.X, value.Joints[JointType.HandRight].Position.Y);
                }
            }

            for (int ID = 9; ID > 0; ID--)
            {
                rHandPosi[ID] = rHandPosi[ID - 1];
            }

            rHandPosi[0] = rightHand;
        }

        public Vector2PLUS Get_LeftHandPosition()
        {
            return leftHand;
        }

        public Vector2PLUS[] Get_RightHandPosition()
        {
            return rHandPosi;
        }
    }
}
