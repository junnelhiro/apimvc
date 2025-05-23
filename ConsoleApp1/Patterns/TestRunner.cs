using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Patterns
{
    public class TestRunner
    {
        public TestRunner()
        {
            MementoClientApp app= new MementoClientApp();
            app.run();
        }
    }
}
