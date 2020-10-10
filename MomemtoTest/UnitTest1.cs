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

        [Test,TestCaseSource(typeof(UndoItemCaseProvider))]
        public void Undo_State_Should_be_Pass(List<Editor> source,int expectedIndex,string stateText)
        {
            source.ForEach(d =>
            {
                var editorState = new EditorState<Editor>(d);
                manager.PushState(editorState);
            });

            var state = manager.Undo();
            var equalStateText  = stateText == state.Object.ToString();
            var equalStateIndex = manager.Index == expectedIndex;

            Assert.IsTrue(state != null);
            Assert.IsTrue(equalStateText,"State text not equal");
            Assert.IsTrue(equalStateText);
            Assert.IsTrue(equalStateIndex);
        }

        [Test]
        public void Case5_Push_4State_1Undo_1Redo_Index_ShouldBe_3()
        {            
            var editor1 = new Editor("T1", "Text 1", 14, "TH SarabunPSK");
            var editor2 = new Editor("T2", "Text 2", 15, "TH SarabunPSK");
            var editor3 = new Editor("T3", "Text 3", 16, "TH SarabunPSK");
            var editor4 = new Editor("T4", "Text 4", 17, "TH SarabunPSK");

            var stateText = $"{editor4.Title} {editor4.Text} {editor4.FontFace} {editor4.FontSize}";

            var source = new List<Editor>() { editor1, editor2, editor3, editor4 };

            source.ForEach(d =>
            {
                var editorState = new EditorState<Editor>(d);
                manager.PushState(editorState);
            });

            manager.Undo();
            var state = manager.Redo();

            var equalStateText = stateText == state.Object.ToString();
            var equalStateIndex = manager.Index == 3;

            Assert.IsTrue(state != null);
            Assert.IsTrue(equalStateText, "State text not equal");
            Assert.IsTrue(equalStateText);
            Assert.IsTrue(equalStateIndex);
        }

        [Test]
        public void Case6_Push_4State_2Undo_1Redo_Index_ShouldBe_3()
        {
            int expectedIndex = 2;
            var editor1 = new Editor("T1", "Text 1", 14, "TH SarabunPSK");
            var editor2 = new Editor("T2", "Text 2", 15, "TH SarabunPSK");
            var editor3 = new Editor("T3", "Text 3", 16, "TH SarabunPSK");
            var editor4 = new Editor("T4", "Text 4", 17, "TH SarabunPSK");

            var stateText = $"{editor3.Title} {editor3.Text} {editor3.FontFace} {editor3.FontSize}";

            var source = new List<Editor>() { editor1, editor2, editor3, editor4 };

            source.ForEach(d =>
            {
                var editorState = new EditorState<Editor>(d);
                manager.PushState(editorState);
            });

            manager.Undo();
            manager.Undo();
            var state = manager.Redo();

            var equalStateText = stateText == state.Object.ToString();
            var equalStateIndex = manager.Index == expectedIndex;

            Assert.IsTrue(state != null);
            Assert.IsTrue(equalStateText, "State text not equal");
            Assert.IsTrue(equalStateText);
            Assert.IsTrue(equalStateIndex);
        }

        [Test]
        public void Reset_History_Index_ShouldBe_Pass()
        {
            manager.Reset();

            Assert.IsTrue(manager.States.Count == 0);
            Assert.IsTrue(manager.Index == -1);
        }
    }

    internal class UndoItemCaseProvider : IEnumerable<ITestCaseData>
    {
        public IEnumerator<ITestCaseData> GetEnumerator()
        { 
            var editor1 = new Editor("T1", "Text 1", 14, "TH SarabunPSK");
            var editor2 = new Editor("T2", "Text 2", 15, "TH SarabunPSK");
            var editor3 = new Editor("T3", "Text 3", 16, "TH SarabunPSK");
            var editor4 = new Editor("T4", "Text 4", 17, "TH SarabunPSK");

            yield return new TestCaseData(new List<Editor>() { editor1, editor2, editor3 }, 1, $"{editor2.Title} {editor2.Text} {editor2.FontFace} {editor2.FontSize}").SetName("Case4: Push_3State_1Redo_Index_ShouldBe_1");            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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