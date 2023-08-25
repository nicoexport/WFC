using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using WFC.SimpleTiles;

namespace WFC.SimpleTiles.Tests {
    [TestFixture(TestOf = typeof(SimpleWaveGrid))]
    sealed class CellTests {

        const string PATH = "Assets/Scripts/SimpleTiles/TestAssets/";

        Texture2D texture;
        InputTextureReader reader;
        IEnumerable<(Color, Color, Direction)> rules;
        IEnumerable<Color> states;


        [SetUp]
        public void SetUp() {
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}Sprite07.png");
            reader = new InputTextureReader(texture);
            rules = reader.GetRules();
            states = reader.GetWeightedStates().Select(x => x.Item1);
        }

        [Test]
        public void T00_WhenCollapse_IsCollapsedIsTrue() {
            IEnumerable<Color> colors = new[] { Color.red, Color.green, Color.blue };
            IEnumerable<(Color, Color, Direction)> rules = new[] {
                (Color.red, Color.blue, Direction.Down)
            };

            var sut = new Cell(colors, rules);
            sut.Collapse(); 

            Assert.That(sut.IsCollapsed, Is.True);
        }

        [Test]
        public void TO1_Cell_WhenMatch_OnlyValidStates() {
            var sut = new Cell(states, rules);
            var otherCell = new Cell(states.Take(3), rules);

            sut.Match(otherCell, Direction.Left);

            Assert.That(sut.Match(otherCell, Direction.Left), Is.True);
        }
    }
}