using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandCommittee.AppData
{
    public partial class Owner
    {
        public string FIO
        {
            get
            {
                if (name != "0" && patronomic != "0")
                {
                    string fio = surname + " " + name + " " + patronomic + " ";
                    return $"{fio}";
                }
                else return $"{surname}";
            }
        }

        public string Phone
        {
            get
            {
                string s = "Телефон: " + phone;
                return s;
            }
        }

        public string Adress
        {
            get
            {
                string s = "Адрес: " + adress;
                return s;
            }
        }
    }
}
