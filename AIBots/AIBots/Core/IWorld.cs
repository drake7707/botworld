using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots
{
    public interface IWorld<Bot, Settings>
        where Bot : IBot
        where Settings : AbstractSettings
    {

        void Update();
        void Initialize(Settings settings, IEnumerable<Bot> bots = null);

        List<Bot> Bots { get; }

        void Draw(Graphics g, int w, int h, int selectedBot);
        void DrawOverlay(Graphics g, int w, int h, int selectedBot);


    }
}
