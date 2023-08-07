using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace WFC.SimpleTiles.Tests {
    public class TextureInputTests {
        const string PATH = "Assets/Input/";

        [Test]
        [TestCase("Sprite-0001.png", 1, 2)]
        [TestCase("Sprite-0002.png", 2, 3)]
        [TestCase("Sprite-0003.png", 4, 4)]
        [TestCase("Sprite-0004.png", 2, 2)]
        [TestCase("Sprite-0005.png", 1, 1)]
        [TestCase("Sprite-0006.png", 3, 2)]
        public void T00_TextureInput_Read(string textureName, int expectedNumberOfRules,
            int expectedNumberOfPossibleStates) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{textureName}");
            Assert.IsTrue(texture);
            var sut = new TextureInput(texture);

            var rules = sut.Read(out var possibleStates);
            Assert.AreEqual(expectedNumberOfRules, rules.ToList().Count);
            Assert.AreEqual(expectedNumberOfPossibleStates, possibleStates.Count);
        }
    }
}