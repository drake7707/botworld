using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace AIBots
{
    public partial class MainForm<World, Bot, Settings> : Form, IMainForm<Bot>
        where World : IWorld<Bot, Settings>, new()
        where Bot : AbstractBot<World, Settings>
        where Settings : AbstractSettings
    {
        private Controller<World, Bot, Settings> controller;
        private World worldShown;

        public MainForm(Settings settings)
        {
            InitializeComponent();

          
            controller = new Controller<World, Bot, Settings>(settings);
            controller.Start();
            ShowLatest();


            pgSettings.SelectedObject = controller.Settings;

            sldBot.Maximum = controller.Settings.NrOfBots;
        }


        protected override void OnClosed(EventArgs e)
        {
            controller.Stop();
            base.OnClosed(e);
        }

        private Image lastFrame;
        private void picWorld_Paint(object sender, PaintEventArgs e)
        {

            if (lastFrame == null || lastFrame.Width != picWorld.Width || lastFrame.Height != picWorld.Height)
                lastFrame = new Bitmap(picWorld.Width, picWorld.Height);


            if (chkShowTrails.Checked)
            {
                e.Graphics.DrawImage(lastFrame, new Rectangle(0, 0, lastFrame.Width, lastFrame.Height), 0, 0, lastFrame.Width, lastFrame.Height, GraphicsUnit.Pixel);
            }
            using (Graphics g = Graphics.FromImage(lastFrame))
            {
                if (chkShowTrails.Checked)
                {
                    using (Brush br = new SolidBrush(Color.FromArgb(32, 255, 255, 255)))
                    {
                        g.FillRectangle(br, new Rectangle(0, 0, lastFrame.Width, lastFrame.Height));
                    }
                }
                else
                    g.Clear(Color.White);

                worldShown.Draw(g, picWorld.Width, picWorld.Height, sldBot.Value);
            }

            e.Graphics.DrawImage(lastFrame, new Rectangle(0, 0, lastFrame.Width, lastFrame.Height), 0, 0, lastFrame.Width, lastFrame.Height, GraphicsUnit.Pixel);

            if (chkDrawVision.Checked)
                worldShown.DrawOverlay(e.Graphics, picWorld.Width, picWorld.Height, sldBot.Value);
        }

        private void btnRun_CheckedChanged(object sender, EventArgs e)
        {
            showWorld = btnRun.Checked;
        }

        private float[] CalculateGenomeSpreadOfHallOfFame()
        {
            float[] output = null;

            float[][] weights = controller.GeneticController.HallOfFame.Select(b => b.Network.GetAllWeights()).ToArray();

            if (weights.Length > 0)
            {
                int nrOfWeights = weights[0].Length;
                output = new float[nrOfWeights];
                int nrOfBots = weights.Length;
                for (int j = 0; j < nrOfWeights; j++)
                {
                    float sum = 0;
                    for (int i = 0; i < nrOfBots; i++)
                        sum += weights[i][j];
                    float avgForGen = sum / nrOfBots;

                    float diffSum = 0;
                    for (int i = 0; i < nrOfBots; i++)
                        diffSum += Math.Abs(weights[i][j] - avgForGen);
                    output[j] = diffSum / sum;
                }
            }

            return output;
        }

        private bool showWorld;

        private DateTime lastWorldUpdate;
        private DateTime lastHofSpreadUpdate;
        private int lastGen;
        private int tick = 0;
        private int shownGen;
        private void tmr_Tick(object sender, EventArgs e)
        {
            if (controller.DoEvolve && (DateTime.Now - lastWorldUpdate).TotalSeconds > 60)
            {
                ShowLatest();
            }

            if (showWorld)
            {
                worldShown.Update();

                if (chkAutoSelectBestPerforming.Checked)
                    sldBot.Value = worldShown.Bots.IndexOf(worldShown.Bots.OrderByDescending(b => b.Fitness).First());

                lblBotFitness.Text = string.Format("Fitness: {0:N2}", SelectedBot.Fitness);

                picWorld.Invalidate();

                lblCurrentShownWorld.Text = "Shown world gen: " + shownGen + " (" + tick + ")";
            }

            if (controller.GeneticController.History.Count > 0)
            {
                if ((DateTime.Now - lastHofSpreadUpdate).TotalMilliseconds > 1000)
                {
                    picHofSpread.Invalidate();
                    lastHofSpreadUpdate = DateTime.Now;
                }

                var lastHistory = controller.GeneticController.History.Last();
                lblStatus.Text = "Generation " + controller.GeneticController.Generation + Environment.NewLine +
                                  "Max fitness: " + lastHistory.MaxFitness + Environment.NewLine +
                                 "Min fitness: " + lastHistory.MinFitness + Environment.NewLine +
                                 "Avg fitness: " + lastHistory.AvgFitness;

                var genhistory = controller.GeneticController.History.ToArray();
                for (int i = lastGen; i < genhistory.Length; i++)
                {
                    var history = genhistory[i];
                    graphMax.AddValue(history.MaxFitness);
                    graphMin.AddValue(history.MinFitness);
                    graphAvg.AddValue(history.AvgFitness);
                }
                lastGen = genhistory.Length;
            }
            tick++;
        }

        private void ShowLatest()
        {
            worldShown = new World();
            worldShown.Initialize(controller.Settings, controller.Worlds[0].Bots.Select(b => (Bot)b.Clone()));
          
            tick = 0;
            lastWorldUpdate = DateTime.Now;
            shownGen = controller.GeneticController.Generation;
        }

        private void btnEvolve_Click(object sender, EventArgs e)
        {
            ShowLatest();

        }

        private void btnCopyHistory_Click(object sender, EventArgs e)
        {
            var genHistory = controller.GeneticController.History.ToArray();
            string str = string.Join(Environment.NewLine, genHistory.Select(h => h.MaxFitness + ";" + h.MinFitness + ";" + h.AvgFitness));
            Clipboard.Clear();
            Clipboard.SetText(str);
        }

        private void picHofSpread_Paint(object sender, PaintEventArgs e)
        {
            int w = picHofSpread.Width;
            int h = picHofSpread.Height;

            float[] spread = CalculateGenomeSpreadOfHallOfFame();
            if (spread != null && spread.Length > 0)
            {
                float blockSizeWidth = w / (float)spread.Length;
                float max = 1;

                for (int i = 0; i < spread.Length; i++)
                {
                    float pos = i * blockSizeWidth;
                    float y = (1 - spread[i] / max) * h;
                    RectangleF r = new RectangleF(pos, y, blockSizeWidth, h - y);
                    e.Graphics.FillRectangle(Brushes.DarkGreen, r);
                }

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            controller.Reset();
        }
        private void chkEvolve_CheckedChanged(object sender, EventArgs e)
        {
            controller.DoEvolve = chkEvolve.Checked;
        }

        private void sldBot_ValueChanged(object sender, EventArgs e)
        {
            sldBot.Maximum = controller.Settings.NrOfBots;
        }

        private void btnShowNetwork_Click(object sender, EventArgs e)
        {
            NetworkForm<World, Bot, Settings> frm = new NetworkForm<World, Bot, Settings>(this);
            frm.Show();

        }

        public Bot SelectedBot
        {
            get
            {
                if (sldBot.Value < worldShown.Bots.Count)
                {
                    Bot b = worldShown.Bots[sldBot.Value];
                    return b;
                }
                return null;
            }
        }



    }







}
