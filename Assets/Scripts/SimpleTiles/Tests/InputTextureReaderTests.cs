using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace WFC.SimpleTiles.Tests {
    [TestFixture(TestOf = typeof(InputTextureReader))]
    internal sealed class InputTextureReaderTests {
        const string PATH = "Assets/Scripts/SimpleTiles/TestAssets/";

        public record RuleInputPairs(string Path, IEnumerable<(Color, Color, Direction)> Rules);

        public record WeightedStateInputPairs(string Path, IEnumerable<(Color, int)> WeightedStates);

        static IEnumerable<(Color, Color, Direction)> Rules01 {
            get {
                yield return new(Color.red, Color.green, Direction.Right);
                yield return new(Color.green, Color.red, Direction.Left);
            }
        }

        static IEnumerable<(Color, Color, Direction)> Rules02 {
            get {
                yield return new(Color.red, Color.green, Direction.Right);
                yield return new(Color.green, Color.red, Direction.Left);
                yield return new(Color.green, Color.blue, Direction.Right);
                yield return new(Color.blue, Color.green, Direction.Left);
            }
        }

        static IEnumerable<(Color, Color, Direction)> Rules03 {
            get {
                yield return new(Color.black, Color.green, Direction.Right);
                yield return new(Color.green, Color.black, Direction.Left);
                yield return new(Color.black, Color.blue, Direction.Down);
                yield return new(Color.blue, Color.black, Direction.Up);
                yield return new(Color.green, Color.red, Direction.Down);
                yield return new(Color.red, Color.green, Direction.Up);
                yield return new(Color.blue, Color.red, Direction.Right);
                yield return new(Color.red, Color.blue, Direction.Left);
            }
        }

        public static IEnumerable<RuleInputPairs> RuleInput {
            get {
                yield return new("Sprite01.png", Rules01);
                yield return new("Sprite02.png", Rules02);
                yield return new("Sprite03.png", Rules03);
            }
        }

        static IEnumerable<(Color, int)> Weights01 {
            get {
                yield return new(Color.green, 3);
                yield return new(Color.blue, 1);
            }
        }

        public static IEnumerable<WeightedStateInputPairs> WeightedStateInput {
            get {
                yield return new("Sprite04.png", Weights01);
            }
        }

        [Test]
        public void T00_InputTextureReader_LoadTextures(
            [ValueSource(nameof(RuleInput))] RuleInputPairs input) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{input.Path}");
            Assert.IsTrue(texture);
        }

        [Test]
        public void T10_GivenTexture_WhenGetWeights_ThenReturnExpectedWeightedStates(
            [ValueSource(nameof(WeightedStateInput))]
            WeightedStateInputPairs input) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{input.Path}");

            var sut = new InputTextureReader(texture);
            var actual = sut.GetWeightedStates();

            CollectionAssert.AreEquivalent(input.WeightedStates, actual);
        }

        [Test]
        public void T20_GivenTexture_WhenGetRules_ThenReturnExpectedRules(
            [ValueSource(nameof(RuleInput))] RuleInputPairs input) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{input.Path}");

            var sut = new InputTextureReader(texture);
            var actual = sut.GetRules();

            CollectionAssert.AreEquivalent(input.Rules, actual);
        }
    }
}