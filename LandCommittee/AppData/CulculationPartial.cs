using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandCommittee.AppData
{
     public partial class Culculation
    {
        public string Culc
        {
            get
            {
                string s =  description+ " - "+percentage+" %";
                return s;
            }
        }
      
    }
}
