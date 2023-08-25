using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WFC.SimpleTiles {
    class Cell {
        public bool IsCollapsed => !Superposition.Select(x => x.Value).Any();
        public int Enthropy => Superposition.Where(pair => pair.Value).Select(pair => pair.Key).Count();
        public Color CollapsedState { get; private set; }

        Dictionary<Color, bool> Superposition;

        public Cell(IEnumerable<Color> states) {
            Superposition = new Dictionary<Color, bool>();
            foreach (var state in states) {
                Superposition.Add(state, true);
            }
        }

        public void Collapse() {
            var candidateColors = Superposition.Where( pair => pair.Value).Select(pair => pair.Key);
            var rand = new System.Random();
            CollapsedState = candidateColors.ElementAtOrDefault(rand.Next(candidateColors.Count()));
            
            foreach (var color in candidateColors) {
                if(color != CollapsedState) {
                    Superposition[color] = false;
                }
            }
        }
    }
}
