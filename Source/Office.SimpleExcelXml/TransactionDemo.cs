using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NetVortex.Office.SimpleExcelXml
{
    class TransactionDemo
    {
        public TransactionDemo()
        {
            SqlConnectionStringBuilder connStrBuilder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "TryHard",
                DataSource = "172.27.1.111",
                UserID = "tester",
                Password = "test123",
            };

            using (SqlConnection sqlCon = new SqlConnection(connStrBuilder.ToString()))
            using (SqlCommand sqlCmd = sqlCon.CreateCommand())
            {
                sqlCon.Open();

                sqlCmd.CommandText = "IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TryTransaction') SELECT 1 ELSE SELECT 0";
                int result = (int)sqlCmd.ExecuteScalar();

                if (result == 1)
                {
                    sqlCmd.CommandText = "DROP TABLE TryTransaction";
                    sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.CommandText = @"CREATE TABLE TryTransaction (ID int NOT NULL, Description nvarchar(max) NOT NULL)";
                sqlCmd.ExecuteNonQuery();
            }

            // Testing multi nested transactions
            using (TransactionScope transOuterScope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 0, 4, 20)))
            {
                using (TransactionScope transInnerScope = new TransactionScope(TransactionScopeOption.Required))
                using (SqlConnection sqlCon = new SqlConnection(connStrBuilder.ToString()))
                using (SqlCommand sqlCmd = sqlCon.CreateCommand())
                {
                    sqlCon.Open();
                    sqlCmd.CommandText = "INSERT INTO TryTransaction (ID, Description) VALUES (1, 'First entry')";
                    sqlCmd.ExecuteNonQuery();

                    try
                    {
                        throw new Exception("test exception that leaves transaction corrupted... or not?");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Transaction state: {0}", Transaction.Current.TransactionInformation.Status);
                    }

                    transInnerScope.Complete();
                }

                transOuterScope.Complete();
            }

            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}
