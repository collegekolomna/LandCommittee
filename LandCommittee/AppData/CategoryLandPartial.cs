using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandCommittee.AppData
{
    public partial class CategoryLand
    {
        public string CatLand
        {
            get
            {
                string s = categoryLand1+" - "+description;
                return s;
            }
        }
    }
}
