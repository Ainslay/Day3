using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    // Chciałem z tego korzystać w taki sposób : Utilities.IsNull(obiekt)
    // i rzucać tutaj wyjątki ale wtedy nie było widoczne co i gdzie
    // się zepsuło. Z drugiej strony null checki nadal nie są tak czyste jakbym chciał :/

    class Utilities
    {
        public static bool IsNull(Object obj)
        {
            if (obj == null)
            {
                return true;
            }
            else if (obj.GetType() == typeof(string))
            {
                string str = (string)obj;
                if(string.IsNullOrWhiteSpace(str))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
