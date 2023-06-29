using System;
using UnityEngine;

namespace Runtime.Texture {
    public class ColorRule {
        public ColorRule(Color secondColor, Color firstColor) {
            FirstColor = firstColor;
            SecondColor = secondColor;
        }

        public Color FirstColor { get; }
        public Color SecondColor { get; }

        public bool Test(Color firstInput, Color secondInput) {
            throw new NotImplementedException();
            /*if (firstInput != FirstColor || firstInput != SecondColor) {
                return false;
            }
            
            if (secondInput != FirstColor || secondInput != SecondColor) {
                return false;
            }

            return true;*/
        }
    }
}