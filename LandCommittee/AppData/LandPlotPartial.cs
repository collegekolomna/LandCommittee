using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandCommittee.AppData
{
    public partial class LandPlot
    {
        public string Square
        {
            get
            {
                string s = "Площадь участка: " + square + "; площадь застройки: " + builtSquare;
                    return $"{s}";
            }
        }
        public string Cadastral
        {
            get
            {
                string s = "Кадастровый номер: " + cadastralNumber;
                return $"{s}";
            }
        }
        public string Cost
        {
            get
            {
                string s = "Нормированная стоимость: " + cost;
                return $"{s}";
            }
        }
        public string Adress
        {
            get
            {
                string s = "Адрес: " + adress;
                return $"{s}";
            }
        }
    }
}
