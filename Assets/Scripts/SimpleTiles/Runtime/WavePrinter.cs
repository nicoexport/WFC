using System;
using UnityEngine;
using WFC.SimpleTiles;

namespace WFC.SimpleTiles {
    public class WavePrinter : MonoBehaviour {
        [SerializeField]
        Texture2D inputTexture;

        [SerializeField] 
        Texture2D outputTexture;

        [SerializeField]
        int maxAttempts = 3;

        [ContextMenu("Run")]
        public void Run() {
            var grid = new SimpleWaveGrid(inputTexture, outputTexture.width, outputTexture.height);

            int attempts = 0;
            bool succes = false;

            while (!succes && attempts < maxAttempts) {
                attempts++;
                succes = grid.Execute();
            }

            if (succes) {
                Debug.Log("print");
                PrintTexture(grid);
            } else {
                Debug.Log($"No Succes after {maxAttempts} attempts");
            }
        }

        void PrintTexture(SimpleWaveGrid grid) {
            for (int i = 0; i < grid.wave.Length; i++) {
                var item = grid.wave[i];
                var coordinates = GetCoordinatesFromIndex(i);

                outputTexture.SetPixel(coordinates.Item1, coordinates.Item2, item.CollapsedState);
                outputTexture.Apply();
            }
        }

        (int, int) GetCoordinatesFromIndex(int index) {
            return (index % outputTexture.width, index / outputTexture.width);
        }
    }
}
