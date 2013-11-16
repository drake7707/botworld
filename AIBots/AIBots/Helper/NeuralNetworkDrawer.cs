using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots
{
    class NeuralNetworkDrawer
    {

        public static void DrawNetwork(NeuralNetwork network, float[][] neuronState, Graphics g, int w, int h)
        {
            float[] weights = network.GetAllWeights();

            int nrOfCols = 1 + network.NrOfHiddenLayers + 1;
            int maxPerRow = Math.Max(Math.Max(network.NrOfInputs, network.NrOfOutputs), network.NrOfNeuronsPerHiddenLayer);

            float cellWidth = w / (float)nrOfCols;
            float cellHeight = h / (float)maxPerRow;

            float neuronWidth = cellWidth / 2f;
            float neuronHeight = cellHeight / 2f;
            neuronWidth = Math.Min(neuronWidth, neuronHeight);

            List<List<PointF>> neuronPositions = new List<List<PointF>>();
            int currentCol = 0;
            int neuronsPerLayer;

            // input
            neuronsPerLayer = network.NrOfInputs;
            CreateAndDrawLayer(g, h, cellWidth, cellHeight, neuronWidth, neuronHeight, currentCol, neuronPositions, neuronsPerLayer, neuronState[currentCol]);
            currentCol++;

            // hidden layers
            neuronsPerLayer = network.NrOfNeuronsPerHiddenLayer;
            for (int j = 0; j < network.NrOfHiddenLayers; j++)
            {
                CreateAndDrawLayer(g, h, cellWidth, cellHeight, neuronWidth, neuronHeight, currentCol, neuronPositions, neuronsPerLayer, neuronState[currentCol]);
                currentCol++;
            }

            // output
            neuronsPerLayer = network.NrOfOutputs;
            CreateAndDrawLayer(g, h, cellWidth, cellHeight, neuronWidth, neuronHeight, currentCol, neuronPositions, neuronsPerLayer, neuronState[currentCol]);
            currentCol++;

            int curWeight = 0;
            for (int i = 0; i < neuronPositions.Count - 1; i++)
            {
                List<PointF> layer = neuronPositions[i];
                List<PointF> nextLayer = neuronPositions[i + 1];

                foreach (PointF src in layer)
                {
                    foreach (PointF dest in nextLayer)
                    {
                        float weight = weights[curWeight];
                        curWeight++;

                        int val = (int)(weight * 255);
                        if (val < 0)
                        {
                            val = -val;
                            if (val < 0) val = 0;
                            if (val > 255) val = 255;

                            using (Pen p = new Pen(Color.FromArgb(255 - val, 0, val)))
                                g.DrawLine(p, new PointF(src.X + neuronWidth / 2, src.Y), new PointF(dest.X - neuronWidth / 2, dest.Y));

                        }
                        else
                        {
                            if (val < 0) val = 0;
                            if (val > 255) val = 255;

                            using (Pen p = new Pen(Color.FromArgb(255, (int)(weight * 255), 0)))
                                g.DrawLine(p, new PointF(src.X + neuronWidth / 2, src.Y), new PointF(dest.X - neuronWidth / 2, dest.Y));
                        }
                    }
                }
            }
        }

        private static void CreateAndDrawLayer(Graphics g, int h, float cellWidth, float cellHeight, float neuronWidth, float neuronHeight, int currentCol, List<List<PointF>> neuronPositions, int neuronsPerLayer, float[] neuronState)
        {
            List<PointF> layer = new List<PointF>(neuronsPerLayer);
            for (int i = 0; i < neuronsPerLayer; i++)
            {
                float yOffset = (h - (cellHeight * neuronsPerLayer)) / 2f;
                PointF p = new PointF(currentCol * cellWidth + cellWidth / 2f, yOffset + i * cellHeight + cellHeight / 2f);
                layer.Add(p);

                RectangleF r = RectangleF.FromLTRB(p.X - neuronWidth / 2, p.Y - neuronHeight / 2, p.X + neuronWidth / 2, p.Y + neuronHeight / 2);

                int val = (int)(neuronState[i] * 255);

                if (val < 0)
                {
                    val = -val;
                    if (val < 0) val = 0;
                    if (val > 255) val = 255;

                    using (Brush b = new SolidBrush(Color.FromArgb(255 - val, 0, val)))
                        g.FillEllipse(b, r);
                }
                else
                {
                    if (val < 0) val = 0;
                    if (val > 255) val = 255;

                    using (Brush b = new SolidBrush(Color.FromArgb(255, val, 0)))
                        g.FillEllipse(b, r);
                }
                g.DrawEllipse(Pens.Black, r);
            }
            neuronPositions.Add(layer);
        }
    }
}
