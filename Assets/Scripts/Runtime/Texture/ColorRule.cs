using System;
using UnityEngine;

namespace Runtime.Texture {
    public class ColorRule {
        public ColorRule(Color firstColor, Color secondColor) {
            FirstColor = firstColor;
            SecondColor = secondColor;
        }

        public Color FirstColor { get; }
        public Color SecondColor { get; }

        public bool Test(Color firstInput, Color secondInput) {
            bool firstMatches = (firstInput == FirstColor) || (firstInput == SecondColor);
            bool secondMatches = (secondInput == FirstColor) || (secondInput == SecondColor);

            return firstMatches && secondMatches;
        }
    }
}