using System;
using UnityEngine;
using UnityEngine.UI;

namespace WFC.SimpleTiles {
    public class InputTextureReader {
        public InputTextureReader(Texture2D inputTexture) => this.inputTexture = inputTexture;
        readonly Texture2D inputTexture;

        public (Color, Color, Direction) Read() {
            throw new NotImplementedException();
        }
    }

    public enum Direction {
        Left, 
        Right, 
        Up, 
        Down
    }
}