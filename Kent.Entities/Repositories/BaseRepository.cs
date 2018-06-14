using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Repositories
{
    public class BaseRepository
    {
        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Data Source=DESKTOP-UBBU8PA;Initial Catalog=KentDatabase;Integrated Security=True");
            }
        }
    }
}
