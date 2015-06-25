using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Office.SimpleExcelXml
{
    class Linq
    {
        public void JoinInto()
        {
            var customerSet = DataSources.GetCustomerDataSet();
            var purchasesSet = DataSources.GetPurchasesDataSet();

            var resultSet = from p in purchasesSet.Tables[0].AsEnumerable()
                            join c in customerSet.Tables[0].AsEnumerable()
                            on p.Field<int>("CustomerId") equals c.Field<int>("ID") into lj
                            from j in lj.DefaultIfEmpty()
                            select new
                            {
                                Name = j.Field<string>("Name"),
                                Item = p.Field<string>("Item")
                            };

            foreach (var result in resultSet)
                Console.WriteLine("{0,-20} {1,-20}", result.Name, result.Item);

            Console.ReadLine();
        }
    }
}
