using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowDiscord
{
    public class FlowMessage
    {
        public string Text { get; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float Speed { get; }

        public FlowMessage(string text, float positionX, float positionY, float speed)
        {
            Text = text;
            PositionX = positionX;
            PositionY = positionY;
            Speed = speed;
        }
    }
}
