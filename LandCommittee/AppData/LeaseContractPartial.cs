using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandCommittee.AppData
{
    public partial class LeaseContract
    {
        public string CadastNumber
        {
            get
            {
                string s = "Кадастровый номер: " + LandPlot.cadastralNumber;
                return $"{s}";
            }
        }

        public string Start
        {
            get
            {
                string s = "Дата начала договора: " + startContract.ToString("dd.MM.yyyy");
                return $"{s}";
            }
        }

        public string End
        {
            get
            {
                string s = "Дата окончания договора: " + endContract.ToString("dd.MM.yyyy");
                return $"{s}";
            }
        }

        public string Tenat
        {
            get
            {
                string s = "Арендатор: " + Owner.FIO;
                return $"{s}";
            }
        }

        public string Use
        {
            get
            {
                string s = "Назначение: " + actualUse;
                return $"{s}";
            }
        }

        public string Fixing
        {
            get
            {
                string s = "Фактическое использование: " + fixing;
                return $"{s}";
            }
        }

        public string Cost
        {
            get
            {
                string s = "Сумма аренды в месяц: " + sumCost;
                return $"{s}";
            }
        }
    }
}
