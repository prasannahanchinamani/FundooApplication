using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Exceptions
{
 public class LabelNotFoundException:Exception
    {
        public LabelNotFoundException(string msg):base(msg) { }
    }
}
