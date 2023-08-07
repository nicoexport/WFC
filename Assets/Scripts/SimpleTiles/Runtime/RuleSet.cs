using System;
using System.Collections.Generic;
using UnityEngine;

namespace WFC.SimpleTiles {
    public class RuleSet {
        public RuleSet(IEnumerable<(Color, Color, Direction)> rules) {
            this.rules = rules;
        }

        readonly IEnumerable<(Color, Color, Direction)> rules;

        public bool Match() {
            throw new NotImplementedException();
        }
    }
}