using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace WFC.SimpleTiles.Tests {
    [TestFixture(TestOf = typeof(SimpleWaveGrid))]
    internal sealed class SimpleWaveGridTests {
        const string PATH = "Assets/Scripts/SimpleTiles/TestAssets/";

        [Test]
        public void T00_TestSimpleWaveGrid() {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}Sprite07.png");
            var sut = new SimpleWaveGrid(texture, 10, 10);

            Assert.IsNotNull(sut);
        }

        [Test]
        public void TestSimpleWaveGrid() {
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}Sprite07.png");
            var sut = new SimpleWaveGrid(texture, 10, 10);

            sut.Execute();
            Assert.IsNotNull(sut);
        }
    } 
}