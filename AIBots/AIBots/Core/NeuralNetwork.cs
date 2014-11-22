﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AIBots
{
    public class NeuralNetwork
    {
        private NeuralLayer[] layers;
        public int NrOfOutputs { get; private set; }
        public int NrOfNeuronsPerHiddenLayer { get; private set; }
        public int NrOfHiddenLayers { get; private set; }
        public int NrOfInputs { get; private set; }
        private float bias;

        public NeuralNetwork(int nrOfInputs, int nrOfOutputs, int nrHiddenLayers, int nrOfNeuronsPerHiddenLayer, float bias, float[] weights = null)
        {
            this.bias = bias;

            this.NrOfInputs = nrOfInputs;
            this.NrOfHiddenLayers = nrHiddenLayers;
            this.NrOfOutputs = nrOfOutputs;
            this.NrOfNeuronsPerHiddenLayer = nrOfNeuronsPerHiddenLayer;

            layers = new NeuralLayer[nrHiddenLayers + 1];

            layers[0] = new NeuralLayer(nrOfInputs, nrOfNeuronsPerHiddenLayer);
            for (int i = 1; i < nrHiddenLayers; i++)
                layers[i] = new NeuralLayer(nrOfNeuronsPerHiddenLayer, nrOfNeuronsPerHiddenLayer);

            // last layer is the output layer
            layers[nrHiddenLayers] = new NeuralLayer(nrOfNeuronsPerHiddenLayer, nrOfOutputs);


            if (weights == null)
                weights = GetRandomWeights(weights);

            SetWeights(weights);
        }

        private float[] GetRandomWeights(float[] weights)
        {
            weights = GetAllWeights();
            Random rnd = RandomManager.Instance.Random;
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = (float)rnd.NextDouble() * 2 - 1;
            }
            return weights;
        }

        private void SetWeights(float[] weights)
        {
            int w = 0;
            foreach (var layer in layers)
            {
                foreach (var n in layer.Neurons)
                {
                    for (int i = 0; i < n.NeuronWeights.Length; i++)
                    {
                        n.NeuronWeights[i] = weights[w];
                        w++;
                    }
                }
            }
        }


        public float[] Calculate(float[] input)
        {
            float[] currentValues = input;

            foreach (var layer in layers)
            {
                float[] output = new float[layer.Neurons.Length];
                for (int i = 0; i < layer.Neurons.Length; i++)
                {
                    float sum = layer.Neurons[i].Sum(bias, currentValues);

                    // calculate the sigmoid inline to prevent call overhead
                    // and use a lookup table
                    // it's ~x100 faster
                    float outputValue;
                    if (sum >= 10) outputValue = 1;
                    else if (sum < -10) outputValue = 0;
                    else outputValue = sigmoidLookup[SIGMOID_LOOKUP_OFFSET + (int)(sum * SIGMOID_FRACTION)];
                    //        outputValue = Sigmoid(sum);

                    
                    output[i] = outputValue;
                }
                currentValues = output;
            }

            return currentValues;
        }

        public void Train(float[] input, float[] expectedOutput, float learningRate)
        {

            //int inputIndex = 0;
            int outputIndex = NrOfHiddenLayers + 1;

            float[][] values = CalculateActivationPerNeuron(input);

            float[][] deltas = new float[NrOfHiddenLayers + 1][];

            deltas[layers.Length - 1] = new float[NrOfOutputs];
            var lastLayer = deltas[layers.Length - 1];
            var outputValues = values[outputIndex];
            for (int i = 0; i < NrOfOutputs; i++)
                lastLayer[i] = expectedOutput[i] - outputValues[i];

            for (int layerIdx = layers.Length - 2; layerIdx >= 0; layerIdx--)
            {
                var layer = layers[layerIdx];
                deltas[layerIdx] = new float[layer.Neurons.Length];

                var nextLayer = layers[layerIdx + 1];
                var nrOfNeuronsInNextLayer = nextLayer.Neurons.Length;
                var deltasOfNextLayer = deltas[layerIdx + 1];
                var deltasOfCurrentLayer = deltas[layerIdx];
                var nrOfNeuronsInLayer = layer.Neurons.Length;
                for (int j = 0; j < nrOfNeuronsInLayer; j++)
                {
                    float deltaSum = 0;
                    for (int k = 0; k < nrOfNeuronsInNextLayer; k++)
                        deltaSum += nextLayer.Neurons[k].NeuronWeights[j] * deltasOfNextLayer[k];

                    deltasOfCurrentLayer[j] = deltaSum;
                }
            }

            // apply weights
            for (int layerIdx = 0; layerIdx < layers.Length; layerIdx++)
            {
                var layer = layers[layerIdx];
                var valuesOfNextLayer = values[layerIdx + 1];
                // value array from inputs of current layer
                float[] valuesOfInputForLayer = values[layerIdx];
                var deltasOfCurrentLayer = deltas[layerIdx];
                var nrOfNeuronsInLayer = layer.Neurons.Length;
                for (int neuronIdx = 0; neuronIdx < nrOfNeuronsInLayer; neuronIdx++)
                {
                    // dSigmoid = value * (1 - value) (partial derivatives components of the gradient)
                    // value = the output of the current neuron
                    // dValue = the derivative of the value
                    var valueOfNextLayerOfNeuron = valuesOfNextLayer[neuronIdx];
                    float dValue = valueOfNextLayerOfNeuron * (1 - valueOfNextLayerOfNeuron);

                    // delta of current neuron
                    float delta = deltasOfCurrentLayer[neuronIdx];

                    var neuron = layer.Neurons[neuronIdx];
                    var nrOfWeightsOfNeuron = neuron.NeuronWeights.Length;
                    for (int k = 0; k < nrOfWeightsOfNeuron; k++)
                    {
                        // http://home.agh.edu.pl/~vlsi/AI/backp_t_en/backprop.html
                        float newWeight = neuron.NeuronWeights[k] + learningRate * delta * dValue * valuesOfInputForLayer[k];
                        //if (newWeight > 1) newWeight = 1;
                        //if (newWeight < -1) newWeight = -1;
                        neuron.NeuronWeights[k] = newWeight;
                    }
                }
            }
        }

        //public void Train(float[] input, float[] expectedOutput, float learningRate)
        //{

        //    //int inputIndex = 0;
        //    int outputIndex = NrOfHiddenLayers + 1;

        //    float[][] values = CalculateActivationPerNeuron(input);

        //    float[][] deltas = new float[NrOfHiddenLayers + 1][];

        //    deltas[layers.Length - 1] = new float[NrOfOutputs];
        //    for (int i = 0; i < NrOfOutputs; i++)
        //        deltas[layers.Length - 1][i] = expectedOutput[i] - values[outputIndex][i];


        //    for (int layerIdx = layers.Length - 2; layerIdx >= 0; layerIdx--)
        //    {
        //        var layer = layers[layerIdx];
        //        deltas[layerIdx] = new float[layer.Neurons.Length];

        //        for (int j = 0; j < layer.Neurons.Length; j++)
        //        {
        //            float deltaSum = 0;
        //            for (int k = 0; k < layers[layerIdx + 1].Neurons.Length; k++)
        //                deltaSum += layers[layerIdx + 1].Neurons[k].NeuronWeights[j] * deltas[layerIdx+1][k];

        //            deltas[layerIdx][j] = deltaSum;
        //        }
        //    }

        //    // apply weights
        //    for (int layerIdx = 0; layerIdx < layers.Length; layerIdx++)
        //    {
        //        var layer = layers[layerIdx];
        //        for (int neuronIdx = 0; neuronIdx < layer.Neurons.Length; neuronIdx++)
        //        {
        //            // dSigmoid = value * (1 - value)
        //            // value = the output of the current neuron
        //            // dValue = the derivative of the value
        //            float dValue = values[layerIdx + 1][neuronIdx] * (1 - values[layerIdx + 1][neuronIdx]);

        //            // value array from inputs of current layer
        //            float[] valuesOfInputForLayer = values[layerIdx];

        //            // delta of current neuron
        //            float delta = deltas[layerIdx][neuronIdx];

        //            var neuron = layer.Neurons[neuronIdx];
        //            for (int k = 0; k < neuron.NeuronWeights.Length; k++)
        //            {
        //                // http://home.agh.edu.pl/~vlsi/AI/backp_t_en/backprop.html
        //                float newWeight = neuron.NeuronWeights[k] + learningRate * delta * dValue * valuesOfInputForLayer[k];
        //                //if (newWeight > 1) newWeight = 1;
        //                //if (newWeight < -1) newWeight = -1;
        //                neuron.NeuronWeights[k] = newWeight;
        //            }
        //        }
        //    }
        //}

        public float[][] CalculateActivationPerNeuron(float[] input)
        {
            float[][] values = new float[1 + 1 + NrOfHiddenLayers][];

            float[] currentValues = input;
            values[0] = currentValues;

            for (int i = 0; i < layers.Length; i++)
            {
                var layer = layers[i];
                float[] output = new float[layer.Neurons.Length];
                for (int j = 0; j < layer.Neurons.Length; j++)
                {
                    float sum = layer.Neurons[j].Sum(bias, currentValues);

                    //float outputValue = Sigmoid(sum);
                    float outputValue;
                    if (sum >= 10) outputValue = 1;
                    else if (sum < -10) outputValue = 0;
                    else outputValue = sigmoidLookup[10000 + (int)(sum * SIGMOID_FRACTION)];

                    output[j] = outputValue;
                }
                currentValues = output;
                values[i + 1] = currentValues;
            }

            return values;
        }


        private const int SIGMOID_LOOKUP_OFFSET = 10000;
        private const int SIGMOID_LOOKUP_SIZE = 20000;
        private const int SIGMOID_FRACTION = 1000;
        private static float[] sigmoidLookup;
        static NeuralNetwork()
        {
            sigmoidLookup = new float[SIGMOID_LOOKUP_SIZE];

            for (int i = -10 * SIGMOID_FRACTION; i < 10 * SIGMOID_FRACTION; i++)
            {
                float val = (float)i / SIGMOID_FRACTION;
                sigmoidLookup[SIGMOID_LOOKUP_OFFSET + i] = (float)(1 / (1 + Math.Pow(Math.E, -val)));
            }

            /*
             * double sumDiff = 0;
            for (float i = -10; i < 10; i += 0.001f)
            {
                sumDiff += Math.Abs(Sigmoid(i) - (float)(1 / (1 + Math.Pow(Math.E, -i))));
            }
            Console.WriteLine("Total diff: " + sumDiff);
            */

        }

        //private static void RunThisIfYouDoubtInliningSigmoidIsNotWorthIt()
        //{
        //    Stopwatch w = new Stopwatch();
        //    w.Start();
        //    float value = 0;
        //    for (long i = 0; i < 10000000; i++)
        //    {
        //        value = (float)(1 / (1 + Exp(-1.25f)));
        //    }
        //    w.Stop();


        //    Stopwatch w2 = new Stopwatch();
        //    w2.Start();

        //    for (long i = 0; i < 10000000; i++)
        //    {
        //        //value = Sigmoid(-1.25f);
        //        if (test > 10)
        //            value = 1;
        //        else if (test < -10)
        //            value = 0;
        //        else
        //            value = sigmoidLookup[10000 + (int)(test * SIGMOID_FRACTION)];
        //    }
        //    w2.Stop();
        //    Console.WriteLine("v" + value);
        //    Console.WriteLine(w.ElapsedMilliseconds + " " + w2.ElapsedMilliseconds);
        //}
        //static float test = -1.25f;

        private static float Sigmoid(float input)
        {
            //if (input > 5)
            //    return 1;
            //if (input < -5)
            //    return 0;

            return (float)(1 / (1 + Exp(-input)));
        }

        private static double Exp(double val)
        {
            long tmp = (long)(1512775 * val + (1072693248 - 60801));
            return BitConverter.Int64BitsToDouble(tmp << 32);
        }

        public float[] GetAllWeights()
        {
            return layers.SelectMany(l => l.Neurons).SelectMany(l => l.NeuronWeights).ToArray();
        }

        private class NeuralLayer
        {
            public Neuron[] Neurons { get; private set; }
            public NeuralLayer(int nrOfNeuronsInPreviousLayer, int nrOfNeurons)
            {
                Neurons = new Neuron[nrOfNeurons];
                for (int i = 0; i < nrOfNeurons; i++)
                    Neurons[i] = new Neuron(nrOfNeuronsInPreviousLayer);
            }
        }

        private struct Neuron
        {

            private float[] neuronWeights;// { get; private set; }

            public float[] NeuronWeights { get { return neuronWeights; } }

            public Neuron(int nrOfInputs)
            {
                neuronWeights = new float[nrOfInputs];
            }

            public float Sum(float bias, float[] input)
            {
                float sum = 0;
                for (int i = 0; i < input.Length; i++)
                    sum += input[i] * neuronWeights[i];

                sum += bias * neuronWeights[neuronWeights.Length - 1];

                return sum;
            }
        }
    }
}
