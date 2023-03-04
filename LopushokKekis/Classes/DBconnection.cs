using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LopushokKekis.Model;

namespace LopushokKekis.Classes
{
    internal class DBconnection
    {
        public static LopushokKekusEntities connect = new LopushokKekusEntities();
    }
}
