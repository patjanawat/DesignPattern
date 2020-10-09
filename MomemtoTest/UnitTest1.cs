using System.Reflection;
using NUnit.Framework;
using Momemto;
using System.Collections;
using Moq;
using System.Linq;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace MomemtoTest
{
    public class Tests
    {
        CareTaker<Editor> manager;

        [SetUp]
        public void Setup()
        {            
            manager = new CareTaker<Editor>();            
        }

        [Test, TestCaseSource(typeof(ItemTestCaseProvider))]
        public void Push_State_Should_Be_Pass(List<Editor> sources, int expectedIndex, string content)
        {
            sources.ForEach(d =>
            {
                var editorState = new EditorState<Editor>(d);
                manager.PushState(editorState);
            });


            int lastIndex = manager.Index;
            var states = manager.States;
            var expectedText = manager.States[expectedIndex].Object.ToString();
            var actualText = manager.States[lastIndex].Object.ToString();

            bool equal = expectedText == actualText;

            Assert.IsTrue(lastIndex == expectedIndex);
            Assert.IsTrue(equal);
        }
    }

    internal class ItemTestCaseProvider : IEnumerable<ITestCaseData>
    {
        public IEnumerator<ITestCaseData> GetEnumerator()
        {
            var editor1 = new Editor("T1", "Text 1", 14, "TH SarabunPSK");
            var editor2 = new Editor("T2", "Text 2", 15, "TH SarabunPSK");
            var editor3 = new Editor("T3", "Text 3", 15, "TH SarabunPSK");

            yield return new TestCaseData(new List<Editor>() { editor1 }, 0, $"{editor1.Title} {editor1.Text} {editor1.FontFace} {editor1.FontSize}").SetName("Case1: Push_One_State_Should_Be_Pass");
            yield return new TestCaseData(new List<Editor>()
            {
                editor1,
                editor2,
            }, 1, $"{editor2.Title} {editor2.Text} {editor2.FontFace} {editor2.FontSize}").SetName("Case2: Push_Two_State_Should_Be_Pass");

            yield return new TestCaseData(new List<Editor>()
            {
               editor1,
                editor2,
                editor3
            }, 2, $"{editor3.Title} {editor3.Text} {editor3.FontFace} {editor3.FontSize}").SetName("Case3: Push_Tree_State_Should_Be_Pass");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}