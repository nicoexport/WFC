using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace WFC.SimpleTiles.Tests {
    [TestFixture(TestOf = typeof(SimpleWaveGrid))]
    sealed class SimpleWaveGridTests {
        const string PATH = "Assets/Scripts/SimpleTiles/TestAssets/";

        //[Test]
        //public void T00_SimpleWaveGrid_LoadTexture() {
        //    var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}Sprite07.png");
        //    var reader = new InputTextureReader(texture);
        //    var weightedStates = reader.GetWeightedStates();
        //    var rules = reader.GetRules();
        //    var sut = new SimpleWaveGrid(10, 10, weightedStates, rules);

        //    Assert.IsNotNull(sut);
        //}

        //[Test]
        //public void T01_SimpleWaveGrid_WhenExecute() {
        //    var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PATH}Sprite07.png");
        //    var reader = new InputTextureReader(texture);
        //    var weightedStates = reader.GetWeightedStates();
        //    var rules = reader.GetRules();
        //    var sut = new SimpleWaveGrid(3, 3, weightedStates, rules);

        //    Debug.Log(sut.Execute());
        //    Debug.Log("-----------------");
        //    foreach (var cell in sut.wave) {
        //        Debug.Log(cell.IsCollapsed);
        //        Debug.Log(cell.CollapsedState);
        //    }
        //}
    }
}