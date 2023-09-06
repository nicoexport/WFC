﻿using System.Collections.Generic;
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

        public bool Execute() {
            ReadInput();
            InitializeWave();

            bool success;
            do {
                success = Observe(out int collapsedCellIndex);

                if (success) {
                    Propagate(collapsedCellIndex);
                }
            } while (success);

            // check output for validity
            return FullyCollapsed();
        }

        void ReadInput() {
            var reader = new InputTextureReader(input);
            weightedStates = reader.GetWeightedStates();
            rules = reader.GetRules();
        }

        void InitializeWave() {
            wave = new Cell[cellAmount];
            // Putting each cell into the superposition
            for (int i = 0; i < wave.Length; i++) {
                wave[i] = new(weightedStates.Select(x => x.Item1), rules);
            }
        }

        bool Observe(out int collapsedCellIndex) {
            int lowest = int.MaxValue;
            collapsedCellIndex = -1;

            for (int i = 0; i < wave.Length; i++) {
                var cell = wave[i];

                if (!cell.IsCollapsed && cell.Enthropy > 0 && cell.Enthropy < lowest) {
                    lowest = cell.Enthropy;
                    collapsedCellIndex = i;
                }
            }

            if (collapsedCellIndex == -1) {
                return false;
            }

            wave[collapsedCellIndex].Collapse();

            return true;
        }

        void Propagate(int collapsedCellIndex) {
            Queue<int> cellsToPropagate = new();
            cellsToPropagate.Enqueue(collapsedCellIndex);
            List<int> propagatedCells = new();

            while (cellsToPropagate.Count > 0) {
                int current = cellsToPropagate.Dequeue();
                propagatedCells.Add(current);

                var coordinates = GetCoordinatesFromIndex(current);
                int x = coordinates.Item1;
                int y = coordinates.Item2;

                // Check left pixel
                if (x > 0) {
                    int leftIndex = current - 1;
                    if (!cellsToPropagate.Contains(leftIndex) && !propagatedCells.Contains(leftIndex)) {
                        cellsToPropagate.Enqueue(leftIndex);
                        wave[leftIndex].Match(wave[current], Direction.Left);
                    }
                }

                // Check right pixel
                if (x < outputSizeX - 1) {
                    int rightIndex = current + 1;
                    if (!cellsToPropagate.Contains(rightIndex) && !propagatedCells.Contains(rightIndex)) {
                        cellsToPropagate.Enqueue(rightIndex);
                        wave[rightIndex].Match(wave[current], Direction.Right);
                    }
                }

                // Check top pixel
                if (y < outputSizeY - 1) {
                    int topIndex = current + outputSizeX;
                    if (cellsToPropagate.Contains(topIndex) && !propagatedCells.Contains(topIndex)) {
                        cellsToPropagate.Enqueue(topIndex);
                        wave[topIndex].Match(wave[current], Direction.Up);
                    }
                }

                // Check bottom pixel
                if (y > 0) {
                    int bottomIndex = current - outputSizeX;
                    if (!cellsToPropagate.Contains(bottomIndex) && !propagatedCells.Contains(bottomIndex)) {
                        cellsToPropagate.Enqueue(bottomIndex);
                        wave[bottomIndex].Match(wave[current], Direction.Down);
                    }
                }
            }
        }

        (int, int) GetCoordinatesFromIndex(int index) {
            return (index % outputSizeX, index / outputSizeX);
        }

        bool FullyCollapsed() {
            return wave.Any(cell => cell.IsContradictory);
        }
    }
}