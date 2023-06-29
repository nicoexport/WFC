using NUnit.Framework;
using Runtime;
using UnityEditor;
using UnityEngine;

namespace Tests {
    public class TextureInputTests {
        const string PATH = "Assets/Input/";

        [Test]
        [TestCase("Sprite-0001.png", 2, 2)]
        public void T00_TextureInput_Read(string textureName, int expectedNumberOfRules,
            int expectedNumberOfPossibleStates) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{textureName}");
            Assert.IsTrue(texture);
            var sut = new TextureInput(texture);
            var rules = sut.Read(out var possibleStates);

            Assert.IsTrue(rules.Length == expectedNumberOfRules);
            Assert.IsTrue(possibleStates.Length == expectedNumberOfPossibleStates);
        }
    }
}