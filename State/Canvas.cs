using System;
using System.Collections.Generic;
using System.Text;

namespace State
{
    public class Canvas:ITool
    {
        private readonly ITool _tool;

        public Canvas(ITool tool)
        {
            _tool = tool;
        }

        public void MouseDown()
        {
            _tool.MouseDown();
        }

        public void MouseUp()
        {
            _tool.MouseUp();
        }
    }
}
