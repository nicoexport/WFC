using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WFC.SimpleTiles {
    public class TextureWaveFunctionGrid : MonoBehaviour {
        [SerializeField] Texture2D inputTexture;
        [SerializeField] int outputSizeX;
        [SerializeField] int outputSizeY;

        TextureInput input;
        List<ColorRule> rules;
        List<ColorState> states;

        protected void Start() {
            input = new(inputTexture);
            rules = input.Read(out states).ToList();

            Debug.Log("Possible States:");
            foreach (var state in states) {
                Debug.Log(state.Color + "WEIGHT: " + state.Weight);
            }

            Debug.Log("Rules:");
            foreach (var rule in rules) {
                Debug.Log(rule.FirstColor + " + " + rule.SecondColor);
            }
        }
    }
}