using System;
using System.Collections.Generic;
using System.Text;

namespace State
{
    public class EraseTool : ITool
    {
        public void MouseDown()
        {
            Console.WriteLine("Select Erase Icon");
        }

        public void MouseUp()
        {
            Console.WriteLine("Delete somthing");

        }
    }
}
