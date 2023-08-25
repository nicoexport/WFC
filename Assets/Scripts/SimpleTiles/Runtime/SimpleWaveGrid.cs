using System.Collections.Generic;
using UnityEngine;

namespace WFC.SimpleTiles {
    public class SimpleWaveGrid {
        public SimpleWaveGrid(Texture2D input, int outputSizeX, int outputSizeY) {
            this.input = input;
            this.outputSizeX = outputSizeX;
            this.outputSizeY = outputSizeY;
            cellAmount = this.outputSizeX * this.outputSizeY;
        }

        readonly Texture2D input;
        readonly int outputSizeX;
        readonly int outputSizeY;
        readonly int cellAmount;

        IEnumerable<(Color, int)> weightedStates;
        IEnumerable<(Color, Color, Direction)> rules;
        Dictionary<Color, bool>[] wave;

        public void Execute() {
            ReadInput();
            wave = new Dictionary<Color, bool>[cellAmount];
            InitializeWave();


        }

        void ReadInput() {
            var reader = new InputTextureReader(input);
            weightedStates = reader.GetWeightedStates();
            rules = reader.GetRules();
        }

        void InitializeWave() {
            // Putting each cell into the superposition
            foreach (var cell in wave) {
                foreach (var state in weightedStates) {
                    cell.Add(state.Item1, true);
                }
            }
        }

    }
}