using System.Collections.Generic;
using UnityEngine;

namespace WFC.SimpleTiles {
    internal sealed class InputTextureReader {
        readonly Texture2D inputTexture;
        public InputTextureReader(Texture2D inputTexture) => this.inputTexture = inputTexture;

        public IEnumerable<(Color, int)> GetWeightedStates() {
            var pixels = inputTexture.GetPixels();
            Dictionary<Color, int> pairs = new();

            foreach (var pixel in pixels) {
                if (pairs.ContainsKey(pixel)) {
                    pairs[pixel] += 1;
                } else {
                    pairs.Add(pixel, 1);
                }
            }

            foreach (var pair in pairs) {
                yield return (pair.Key, pair.Value);
            }
        }

        public IEnumerable<(Color, Color, Direction)> GetRules() {
            var pixels = inputTexture.GetPixels();
            int width = inputTexture.width;
            int height = inputTexture.height;
            var rules = new List<(Color, Color, Direction)>();

            // Iterate over all pixels
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int index = (y * width) + x;

                    // Access the current pixel
                    var currentPixelColor = pixels[index];
                    // Check neighboring pixels to setup rules

                    // Check left pixel
                    if (x > 0) {
                        int leftIndex = index - 1;
                        var candidateRule = (currentPixelColor, pixels[leftIndex], Direction.Left);
                        if (!rules.Contains(candidateRule)) {
                            rules.Add(candidateRule);
                            yield return candidateRule;
                        }
                    }

                    // Check right pixel
                    if (x < width - 1) {
                        int rightIndex = index + 1;
                        var candidateRule = (currentPixelColor, pixels[rightIndex], Direction.Right);
                        if (!rules.Contains(candidateRule)) {
                            rules.Add(candidateRule);
                            yield return candidateRule;
                        }
                    }

                    // Check top pixel
                    if (y < height - 1) {
                        int topIndex = index + width;
                        var candidateRule = (currentPixelColor, pixels[topIndex], Direction.Up);
                        if (!rules.Contains(candidateRule)) {
                            rules.Add(candidateRule);
                            yield return candidateRule;
                        }
                    }

                    // Check bottom pixel
                    if (y > 0) {
                        int bottomIndex = index - width;
                        var candidateRule = (currentPixelColor, pixels[bottomIndex], Direction.Down);
                        if (!rules.Contains(candidateRule)) {
                            rules.Add(candidateRule);
                            yield return candidateRule;
                        }
                    }
                }
            }
        }
    }
}