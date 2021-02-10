using MrHrumsHomeEdition.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrHrumsHomeEdition.OtherClasses
{
    /// <summary>
    /// This class responsible for storage database connection instance
    /// </summary>
    public static class DataBaseConnection
    {
        public static MainDataBaseEntity DB = new MainDataBaseEntity();
    }
}
