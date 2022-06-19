using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandCommittee.AppData
{
     public partial class categoryOwner
    {
        public string CatOwner
        {
            get
            {
                string s = categoryOwner1 + " - "+description;
                return s;
            }
        }
    }
}
