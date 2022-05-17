using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class CompareHelper
    {
        public static bool Comparator(string actTitle, string expectedTitle)
        {
            bool compar = actTitle.Contains(expectedTitle);

            return compar;
        }
    }
}
