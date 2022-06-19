using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandCommittee.AppData
{
    public partial class ContractOfSale
    {
        public string CadastNumber
        {
            get
            {
                string s = "Кадастровый номер: " + LandPlot.cadastralNumber;
                return $"{s}";
            }
        }

        public string Data
        {
            get
            {
                string s = "Дата заключения договора: " + data.ToString("dd.MM.yyyy");
                return $"{s}";
            }
        }

        public string Owner_
        {
            get
            {
                string s = "Покупатель: " + Owner.FIO;
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


        public string Cost
        {
            get
            {
                string s = "Сумма оплаты: " + sumCost;
                return $"{s}";
            }
        }
    }
}
