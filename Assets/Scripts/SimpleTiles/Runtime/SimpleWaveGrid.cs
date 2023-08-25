using System.Collections.Generic;
using System.Linq;
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
        Cell[] wave;

        public void Execute() {
            ReadInput();
            wave = new Cell[cellAmount];
            InitializeWave();

            //Observation
            bool succes = FindLowestEnthropyCellIndex(wave, out int cellIndex);
        }

        void ReadInput() {
            var reader = new InputTextureReader(input);
            weightedStates = reader.GetWeightedStates();
            rules = reader.GetRules();
        }

        void InitializeWave() {
            // Putting each cell into the superposition
            for (int i = 0; i < wave.Length; i++) {
                wave[i] = new(weightedStates.Select( x => x.Item1));
            }
        }

        bool FindLowestEnthropyCellIndex(Cell[] wave, out int cellIndex) {
            int lowest = int.MaxValue;
            cellIndex = -1;

            for (int i = 0; i < wave.Length; i++) {
                var cell = wave[i];

                if (cell.Enthropy < lowest) {
                    lowest = cell.Enthropy;
                    cellIndex = i;
                }
            }

            return cellIndex == -1;
        }
    }
}