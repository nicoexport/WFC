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
        Queue<int> cellsToPropagate = new();
        List<int> observedCells = new();

        public void Execute() {
            ReadInput();
            wave = new Cell[cellAmount];
            InitializeWave();

            //Observation
            observedCells.Clear();
            bool succes = FindLowestEnthropyCellIndex(wave, out int cellIndex);
            wave[cellIndex].Collapse();
            cellsToPropagate.Enqueue(cellIndex);

            while (cellsToPropagate.Count > 0) {
                Propagate(cellsToPropagate.Dequeue());
            }
        }

        void ReadInput() {
            var reader = new InputTextureReader(input);
            weightedStates = reader.GetWeightedStates();
            rules = reader.GetRules();
        }

        void InitializeWave() {
            // Putting each cell into the superposition
            for (int i = 0; i < wave.Length; i++) {
                wave[i] = new(weightedStates.Select(x => x.Item1), rules);
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

        void Propagate(int cellIndex) {
            //CheckNeighbours add them to the stack if they are'nt on it and sort out states that dont match the rules
            observedCells.Add(cellIndex);

            var coordinates = GetCoordinatesFromIndex(cellIndex);
            int x = coordinates.Item1;
            int y = coordinates.Item2;

            // Check left pixel
            if (x > 0) {
                int leftIndex = cellIndex - 1;
                if (!observedCells.Contains(leftIndex)) {
                    cellsToPropagate.Enqueue(leftIndex);
                    wave[leftIndex].Match(wave[cellIndex], Direction.Left);
                }
            }

            // Check right pixel
            if (x < outputSizeX - 1) {
                int rightIndex = cellIndex + 1;
                if (!observedCells.Contains(rightIndex)) {
                    cellsToPropagate.Enqueue(rightIndex);
                    wave[rightIndex].Match(wave[cellIndex], Direction.Right);
                }
            }

            // Check top pixel
            if (y < outputSizeY - 1) {
                int topIndex = cellIndex + outputSizeX;
                if (!observedCells.Contains(topIndex)) {
                    cellsToPropagate.Enqueue(topIndex);
                    wave[topIndex].Match(wave[cellIndex], Direction.Up);
                }
            }

            // Check bottom pixel
            if (y > 0) {
                int bottomIndex = cellIndex - outputSizeX;
                if (!observedCells.Contains(bottomIndex)) {
                    cellsToPropagate.Enqueue(bottomIndex);
                    wave[bottomIndex].Match(wave[cellIndex], Direction.Down);
                }
            }
        }

        (int, int) GetCoordinatesFromIndex(int index) {
            return (index / outputSizeX, index % outputSizeX);
        }
    }
}