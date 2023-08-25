﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WFC.SimpleTiles {
    class Cell {
        public bool IsCollapsed => !superposition.Select(x => x.Value).Any();
        public int Enthropy => superposition.Where(pair => pair.Value).Select(pair => pair.Key).Count();
        public Color CollapsedState { get; private set; }

        Dictionary<Color, bool> superposition;
        IEnumerable<(Color, Color, Direction)> rules;

        public Cell(IEnumerable<Color> states, IEnumerable<(Color, Color, Direction)> rules) {
            superposition = new Dictionary<Color, bool>();
            foreach (var state in states) {
                superposition.Add(state, true);
            }
            this.rules = rules;
        }

        public void Collapse() {
            var candidateColors = superposition.Where( pair => pair.Value).Select(pair => pair.Key);
            var rand = new System.Random();
            CollapsedState = candidateColors.ElementAtOrDefault(rand.Next(candidateColors.Count()));
            
            foreach (var color in candidateColors) {
                if(color != CollapsedState) {
                    superposition[color] = false;
                }
            }
        }

        public void Match(Cell other, Direction direction) {
            var otherColors = other.superposition.Where(pair => pair.Value).Select(pair => pair.Key);
            var thisColors = superposition.Where(pair => pair.Value).Select(pair => pair.Key);

            foreach (var otherColor in otherColors) {
                foreach (var thisColor in thisColors) {
                    var candidateRule = (otherColor, thisColor, direction);

                    if(!rules.Contains(candidateRule)) {
                        superposition[thisColor] = false;
                    }
                }
            }
        }
    }
}