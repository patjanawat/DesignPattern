using System;
using System.Collections.Generic;
using System.Text;

namespace State
{
    public class RactangleTool : ITool
    {
        public void MouseDown()
        {
            Console.WriteLine("Select Ractangle Icon");

        }

        public void MouseUp()
        {
            Console.WriteLine("Draw Ractangle");

        }
    }
}
