using System;
using System.Collections.Generic;
using UnityEngine;

namespace WFC.SimpleTiles {
    public class InputTextureReader {
        public InputTextureReader(Texture2D inputTexture) => this.inputTexture = inputTexture;
        readonly Texture2D inputTexture;

        public IEnumerable<(Color, Color, Direction)> GenerateRules() {
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