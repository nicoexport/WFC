using System;
using UnityEngine;

namespace Runtime {
    public class TextureInput : IInput {
        public TextureInput(Texture2D input) => this.input = input;

        Texture2D input;

        public IRule[] Read(out IState[] possibleStates) => throw new NotImplementedException();
    }
}