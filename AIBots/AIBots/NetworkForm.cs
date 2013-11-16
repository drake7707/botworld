using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIBots
{
    public partial class NetworkForm<World, Bot, Settings> : Form
        where Settings :  AbstractSettings
        where Bot : AbstractBot<World,Settings>
        where World : IWorld<Bot, Settings>
    {
        private IMainForm<Bot> parent;

        public NetworkForm(IMainForm<Bot> parent)
        {
            this.parent = parent;

            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Bot b = parent.SelectedBot;
            if (b != null)
            {
                NeuralNetworkDrawer.DrawNetwork(b.Network, b.CurrentNeuronState, e.Graphics, this.ClientSize.Width, this.ClientSize.Height);
            }
            base.OnPaint(e);
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
