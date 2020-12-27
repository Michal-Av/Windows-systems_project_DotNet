using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
namespace BL
{
    public class FactoryBl
    {
        static IBL bl = null;
        public static IBL GetBL()
        {
            if (bl == null)
                bl = Bl_imp.GetInstance();
            return bl;
        }
    }
}
