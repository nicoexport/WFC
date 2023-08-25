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
            var candidates = Superposition.Where( pair => pair.Value).Select(pair => pair.Key);
            var rand = new System.Random();
            var color = candidates.ElementAtOrDefault(rand.Next(candidates.Count()));
            CollapsedState = color;
            Superposition.Clear();
        }
    }
}
