using Leap;

namespace KineFigu
{
    class LeapMotion
    {
        class UserListener : Listener
        {
            public bool extendFlag;

            public UserListener():base()
            {
                extendFlag = false;
            }

            public override void OnFrame(Controller controller)
            {
                Frame frame = controller.Frame();

                foreach (Hand hand in frame.Hands)
                {
                    if (hand.Fingers[3].IsExtended) { extendFlag = true; }
                    else { extendFlag = false; }
                }

                base.OnFrame(controller);
            }
        }

        UserListener uListener;
        Controller controller;

        public LeapMotion()
        {
            uListener = new UserListener();
            controller = new Controller();

            controller.AddListener(uListener);
        }

        public bool Get_Flag()
        {
            return uListener.extendFlag;
        }

        public void Stop()
        {
            controller.RemoveListener(uListener);
            controller.Dispose();
        }
    }
}
