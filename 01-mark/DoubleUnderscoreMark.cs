using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_mark
{
    class DoubleUnderscoreMark : UnderscoreMark
    {
        protected override string GetRegex()
        {
            return @"\\{0,1}__[^_](.*)\\{0,1}__";
        }

        protected override string GetTag()
        {
            return "__"; 
        }

        protected override string GetTagPattern()
        {
            return "<strong>{0}</strong>";
        }
    }
}
