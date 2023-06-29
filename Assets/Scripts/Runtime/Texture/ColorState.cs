using UnityEngine;

namespace Runtime.Texture {
    public class ColorState {
        public ColorState(Color color, float weight) {
            Color = color;
            Weight = weight;
        }

        public Color Color { get; }
        public float Weight { get; }
    }
}