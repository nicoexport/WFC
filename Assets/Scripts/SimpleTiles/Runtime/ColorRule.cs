using UnityEngine;

namespace WFC.SimpleTiles {
    public class ColorRule {
        public ColorRule(Color firstColor, Color secondColor) {
            FirstColor = firstColor;
            SecondColor = secondColor;
        }

        public Color FirstColor { get; }
        public Color SecondColor { get; }

        public bool Match(Color firstInput, Color secondInput) {
            if (firstInput == secondInput) {
                return firstInput == FirstColor && firstInput == SecondColor;
            }

            bool firstMatches = firstInput == FirstColor || firstInput == SecondColor;
            bool secondMatches = secondInput == FirstColor || secondInput == SecondColor;

            return firstMatches && secondMatches;
        }
    }
}