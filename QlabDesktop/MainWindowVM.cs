using QlabDesktop.AtTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlabDesktop
{
    internal class MainWindowVM
    {
        public AtTestViewModel AtTest { get; } = new AtTestViewModel();

        private AtTestModel atTestModel;

        public MainWindowVM()
        {
            atTestModel = new AtTestModel(AtTest);
        }
    }
}
