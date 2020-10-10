using NUnit.Framework;
using State;
using System;

namespace StateTest
{
    public class Tests
    {
        Canvas canvas;
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Select_BrushTool_Should_Be_Draw_Line()
        {
            ITool brush = new BrushTool();
            canvas = new Canvas(brush);
            canvas.MouseDown();
            canvas.MouseUp();
            Assert.Pass();            
        }

        [Test]
        public void Select_EraseTool_Should_Be_Draw_Eraser()
        {
            ITool eraser = new EraseTool();
            canvas = new Canvas(eraser);
            canvas.MouseDown();
            canvas.MouseUp();

            Assert.Pass();
        }

        [Test]
        public void Select_RactangleTool_Should_Be_Draw_Ractangle()
        {
            ITool ractangle = new RactangleTool();
            canvas = new Canvas(ractangle);
            canvas.MouseDown();
            canvas.MouseUp();

            Assert.Pass();
        }
    }
}