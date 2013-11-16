using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIBots
{
    public abstract class AbstractWorld<Bot, S> : IWorld<Bot, S>
        where Bot : IBot 
        where S : AbstractSettings
    {
          public List<Bot> Bots { get; set; }

          public S Settings { get; set; }



          public abstract void Update();


          public abstract void Initialize(S settings, IEnumerable<Bot> bots = null);
         
          public abstract void Draw(System.Drawing.Graphics g, int w, int h, int selectedBot);

          public abstract void DrawOverlay(System.Drawing.Graphics g, int w, int h, int selectedBot);
    }
}
