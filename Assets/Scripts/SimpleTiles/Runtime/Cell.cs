using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WFC.SimpleTiles {
    public class Cell {
        public bool IsCollapsed;
        public bool IsContradictory => superposition.Where(pair => pair.Value).Count() == 0;
        public int Enthropy => superposition.Where(pair => pair.Value).Select(pair => pair.Key).Count();
        public Color CollapsedState { get; private set; }

        public Dictionary<Color, bool> superposition;
        IEnumerable<(Color, Color, Direction)> rules;

        public Cell(IEnumerable<Color> states, IEnumerable<(Color, Color, Direction)> rules) {
            superposition = new Dictionary<Color, bool>();
            foreach (var state in states) {
                superposition.Add(state, true);
            }
            this.rules = rules;
        }

        public void Collapse() {
            var candidateColors = superposition.Where(pair => pair.Value).Select(pair => pair.Key).ToList();
            var rand = new System.Random();
            CollapsedState = candidateColors.ElementAtOrDefault(rand.Next(candidateColors.Count()));

            foreach (var color in candidateColors) {
                if (color != CollapsedState) {
                    superposition[color] = false;
                }
            }
            IsCollapsed = true;
        }

        public bool Match(Cell other, Direction direction) {
            bool wasMatching = true;
            var otherColors = other.superposition.Where(pair => pair.Value).Select(pair => pair.Key).ToList();
            var thisColors = superposition.Where(pair => pair.Value).Select(pair => pair.Key).ToList();

            for (int i = 0; i < otherColors.Count; i++) {
                var otherColor = otherColors[i];
                for (int j = 0; j < thisColors.Count; j++) {
                    var thisColor = thisColors[j];
                    var candidateRule = (otherColor, thisColor, direction);

                    if (!rules.Contains(candidateRule)) {
                        superposition[thisColor] = false;
                        wasMatching = false;
                    }
                }
            }

            return wasMatching;
        }
    }
}
