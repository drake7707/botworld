using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AIBots.BotWorld;

namespace AIBots
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnAIBots_Click(object sender, EventArgs e)
        {
            var frm = new MainForm<World, Bot, Settings>(new Settings());
            frm.ShowDialog();

        }

        private void btnHarvestBots_Click(object sender, EventArgs e)
        {
            var frm = new MainForm<HarvestWorld.World, HarvestWorld.Bot, HarvestWorld.Settings>(new HarvestWorld.Settings());
            frm.ShowDialog();

        }
    }
}
