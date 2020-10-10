using System;
using System.Collections.Generic;
using System.Text;

namespace State
{
    public class EraseTool : ITool
    {
        public void MouseDown()
        {
            Console.WriteLine("MouseDown");
        }

        public void MouseUp()
        {
            Console.WriteLine("MouseUp");

        }
    }
}
