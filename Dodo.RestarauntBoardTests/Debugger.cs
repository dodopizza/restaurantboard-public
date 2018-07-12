using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.RestarauntBoardTests
{
    public class Debugger
    {
        public static void Main()
        {
            var test = new TrackerClientTests();
            test.GetOrdersShoudInvokeOnce_WhenGetOrders();
        }        
    }
}
