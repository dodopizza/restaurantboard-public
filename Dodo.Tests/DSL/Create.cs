using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.Tests.DSL
{
  public static class Create
    {
        public static PizzeriaOrderBuilder PizzeriaOrder()
        {
            return new PizzeriaOrderBuilder();
        }

        public static BoardBuilder Board()
        {
            return new BoardBuilder();
        }
    }
}
