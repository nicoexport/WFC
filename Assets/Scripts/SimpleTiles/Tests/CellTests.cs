using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using WFC.SimpleTiles;

namespace WFC.SimpleTiles.Tests {
    [TestFixture(TestOf = typeof(SimpleWaveGrid))]
    sealed class CellTests {

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
    }
}