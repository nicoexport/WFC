using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WFC.SimpleTiles {
    public class TextureInput {
        public TextureInput(Texture2D input) => this.input = input;

        readonly Texture2D input;

        public IEnumerable<ColorRule> Read(out List<ColorState> possibleStates) {
            Dictionary<Color, int> colors = new();
            var pixels = input.GetPixels();

            int width = input.width;
            int height = input.height;

            var rules = new List<ColorRule>();

            // Iterate over all pixels
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int index = (y * width) + x;

                    // Access the current pixel
                    var pixelColor = pixels[index];

                    // Add current Color to color dictionary or increase count of color
                    if (colors.ContainsKey(pixelColor)) {
                        colors[pixelColor] += 1;
                    } else {
                        colors.Add(pixelColor, 1);
                    }

                    // Check neighboring pixels to setup rules
                    List<Color> neighbourColors = new();

                    // Check left pixel
                    if (x > 0) {
                        int leftIndex = index - 1;
                        neighbourColors.Add(pixels[leftIndex]);
                    }

                    // Check right pixel
                    if (x < width - 1) {
                        int rightIndex = index + 1;
                        neighbourColors.Add(pixels[rightIndex]);
                    }

                    // Check top pixel
                    if (y < height - 1) {
                        int topIndex = index + width;
                        neighbourColors.Add(pixels[topIndex]);
                    }

                    // Check bottom pixel
                    if (y > 0) {
                        int bottomIndex = index - width;
                        neighbourColors.Add(pixels[bottomIndex]);
                    }

                    // Add rule, if it doesnt exist already
                    foreach (var neighbour in neighbourColors) {
                        if (!rules.Any(rule => rule.Match(pixelColor, neighbour))) {
                            rules.Add(new ColorRule(pixelColor, neighbour));
                        }
                    }
                }
            }

            int totalPixels = input.width * input.height;
            
            possibleStates = new List<ColorState>();
            foreach (var entry in colors) {
                float weight = (float)entry.Value / totalPixels;
                possibleStates.Add(new ColorState(entry.Key, weight));
            }

            return rules;
        }
    }
}