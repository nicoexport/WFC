using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace WFC.SimpleTiles.Tests {
    [TestFixture(TestOf = typeof(InputTextureReader))]
    public class InputTextureReaderTests {
        const string PATH = "Assets/Scripts/SimpleTiles/TestAssets/";

        public record InputPairs(string Path, IEnumerable<(Color, Color, Direction)> Rules);
        
        #region 00 Input Pairs
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

        public static IEnumerable<InputPairs> Input {
            get {
                yield return new("Sprite01.png", Rules01);
                yield return new("Sprite02.png", Rules02);
                yield return new("Sprite03.png", Rules03);
            }
        }
        #endregion
        
        [Test]
        public void T00_InputTextureReader_LoadTextures([ValueSource(nameof(Input))] InputPairs input) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{input.Path}");
            Assert.IsTrue(texture);
        }

        [Test]
        public void T01_GivenTexture_WhenGenerateRules_ThenReturnExpectedRules(
            [ValueSource(nameof(Input))] InputPairs input) {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}{input.Path}");

            var sut = new InputTextureReader(texture);
            var actual = sut.GenerateRules();

            CollectionAssert.AreEquivalent(input.Rules, actual);
        }
    }
}