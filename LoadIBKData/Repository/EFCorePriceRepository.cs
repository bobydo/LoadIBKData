using LoadIBKData.Data;
using LoadIBKData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadIBKData.Repository
{
    public class EFCorePriceRepository : EfCoreRepository<Price, APIDbContext>
    {
        public EFCorePriceRepository(APIDbContext context) : base(context)
        {

        }
    }
}
