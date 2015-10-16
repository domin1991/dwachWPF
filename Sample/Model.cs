using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwachWPF.Sample
{
    public class Model
    {
        public FirstEnum Flags { get; set; }
        public SecondEnum PersonProperty { get; set; }

        public Model()
        {
            Flags = (FirstEnum.SecondFlag | FirstEnum.FifthFlag);
        }
    }
}
