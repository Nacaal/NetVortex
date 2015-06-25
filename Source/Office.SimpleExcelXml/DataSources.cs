using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Office.SimpleExcelXml
{
    class DataSources
    {
        public static DataSet GetCustomerDataSet()
        {
            DataSet resultSet = new DataSet("CustomerDataSet");
            resultSet.Tables.Add("Customers");

            resultSet.Tables["Customers"].Columns.Add("ID", typeof(int));
            resultSet.Tables["Customers"].Columns.Add("Name", typeof(string));
            resultSet.Tables["Customers"].Columns.Add("Age", typeof(int));

            resultSet.Tables["Customers"].Rows.Add(1, "Roxanne Villareal", 23);
            resultSet.Tables["Customers"].Rows.Add(2, "Jan Saunder", 52);
            resultSet.Tables["Customers"].Rows.Add(3, "Janett Guillen", 16);
            resultSet.Tables["Customers"].Rows.Add(4, "Yanira Coller", 62);
            resultSet.Tables["Customers"].Rows.Add(5, "Long Triplett", 32);
            resultSet.Tables["Customers"].Rows.Add(6, "Milford Laskey", 44);

            return resultSet;
        }

        public static DataSet GetPurchasesDataSet()
        {
            DataSet resultSet = new DataSet("PurchasesDataSet");
            resultSet.Tables.Add("Purchases");

            resultSet.Tables["Purchases"].Columns.Add("ID", typeof(int));
            resultSet.Tables["Purchases"].Columns.Add("CustomerId", typeof(int));
            resultSet.Tables["Purchases"].Columns.Add("Item", typeof(string));

            resultSet.Tables["Purchases"].Rows.Add(1, 3, "Dimethicone");
            resultSet.Tables["Purchases"].Rows.Add(2, 2, "Valacyclovir Hydrochloride");
            resultSet.Tables["Purchases"].Rows.Add(3, 5, "Guaifenesin");
            resultSet.Tables["Purchases"].Rows.Add(4, 4, "Ramipril");
            resultSet.Tables["Purchases"].Rows.Add(5, 6, "Calcium Carbonate");
            resultSet.Tables["Purchases"].Rows.Add(6, 1, "Serotonin");
            resultSet.Tables["Purchases"].Rows.Add(7, 6, "Ibuprofen");
            resultSet.Tables["Purchases"].Rows.Add(8, 7, "Aluminum Zirconium");
            resultSet.Tables["Purchases"].Rows.Add(9, 6, "Amoxicillin");

            return resultSet;
        }
    }
}
