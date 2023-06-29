using NUnit.Framework;
using Runtime.Texture;
using UnityEngine;

namespace Tests.Texture {
    public class ColorRuleTests {

        Color ruleColor1;
        Color ruleColor2;
        Color testColor1;
        Color testColor2;
        
        [Test]
        [TestCase("#ff0000", "#00ff00", "#ff0000", "#00ff00", true)]
        public void T00_ColorRule_Test(string ruleColorHex1, string ruleColorHex2, string testColorHex1, string testColorHex2, bool expectedResult) {
            Assert.IsTrue(ColorUtility.TryParseHtmlString(ruleColorHex1, out ruleColor1));
            Assert.IsTrue(ColorUtility.TryParseHtmlString(ruleColorHex2, out ruleColor2));
            Assert.IsTrue(ColorUtility.TryParseHtmlString(testColorHex1, out testColor1));
            Assert.IsTrue(ColorUtility.TryParseHtmlString(testColorHex2, out testColor2));

            var sut = new ColorRule(ruleColor1, ruleColor2);
            
            Assert.IsNotNull(sut);

            bool result = sut.Test(testColor1, testColor2);
            
            Assert.AreEqual(expectedResult, result);
        }
    }
}