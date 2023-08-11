using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace WFC.SimpleTiles.Tests {
    [TestFixture(TestOf = typeof(InputTextureReader))]
    public class InputTextureReaderTests {
        const string PATH = "Assets/Scripts/SimpleTiles/TestAssets/";

        public record TextureRulesPair(string path, IEnumerable<(Color, Color, Direction)> rules) {
            
        }

        public static IEnumerable<(Color, Color, Direction)> Rules01 {
            get {
                yield return new(Color.red, Color.green, Direction.Right);
                yield return new(Color.green, Color.red, Direction.Left);
            }
        }

        public static IEnumerable<TextureRulesPair> RulesPairs {
            get {
                yield return new("Sprite-0001.png", Rules01);
            }
        }

        [Test]
        [TestCase("Sprite-0001.png")]
        [TestCase("Sprite-0002.png")]
        [TestCase("Sprite-0003.png")]
        [TestCase("Sprite-0004.png")]
        [TestCase("Sprite-0005.png")]
        [TestCase("Sprite-0006.png")]
        public void T00_InputTextureReader_LoadTextures(string textureName) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{textureName}");
            Assert.IsTrue(texture);
        }

        [Test]
        [TestCase("Sprite-0001.png", 2)]
        public void T01_InputTextureReader_NumberOfRules(string textureName, int expectedNumberOfRules) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{textureName}");
            Assert.IsTrue(texture);

            var sut = new InputTextureReader(texture);
            var rules = sut.GenerateRules();
            Assert.AreEqual(expectedNumberOfRules, rules.ToList().Count);
        }

        [Test]
        public void T02_GivenTexture_WhenGenerateRules_ThenReturnAllRules(
            [ValueSource(nameof(RulesPairs))] TextureRulesPair rulePairs) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{rulePairs.path}");

        }
    }
}