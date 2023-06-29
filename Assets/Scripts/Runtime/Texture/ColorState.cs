using UnityEngine;

namespace Runtime.Texture {
    public class ColorState {
        public ColorState(Color color) => Color = color;

        public Color Color { get; }
    }
}