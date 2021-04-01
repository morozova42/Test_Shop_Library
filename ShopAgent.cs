using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ShopLib
{
    public class ShopAgent
    {
        private string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        public delegate void ReserveStateHandler(string mes);
        ReserveStateHandler stateHandler;
        public void RegisterDelegate(ReserveStateHandler sH)
        {
            stateHandler = sH;
        }

        public int Reserve(int user_id, int product_id, int quantity)
        {
            string expression = $"dbo.Create_reserve";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(expression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр пользователя
                SqlParameter userParam = new SqlParameter
                {
                    ParameterName = "@user_id",
                    Value = user_id
                };
                command.Parameters.Add(userParam);
                // параметр продукта
                SqlParameter prodParam = new SqlParameter
                {
                    ParameterName = "@product_id",
                    Value = product_id
                };
                command.Parameters.Add(prodParam);
                // параметр количества
                SqlParameter qtyParam = new SqlParameter
                {
                    ParameterName = "@reserve_qty",
                    Value = quantity
                };
                command.Parameters.Add(qtyParam);
                // параметр количества товара в наличии
                SqlParameter product_qty_Param = new SqlParameter
                {
                    ParameterName = "@product_qty",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(product_qty_Param);
                SqlDataReader reader = command.ExecuteReader();
                string log1 = "";
                string log2 = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateTime time = (DateTime)reader["reserve_date"];
                        log1 = $"Бронь № {reader["reserve_id"]} поступила в {time.ToString("HH:mm:ss:fffffff")}. " +
                            $"Запрошено {reader["reserve_qty"]} единиц. В наличии имелось ";
                        log2 = $". Обработано с кодом {reader["reserve_succeed"]}.";
                    }
                }
                reader.Close();
                if (stateHandler != null)
                {
                    stateHandler(log1 + (int)product_qty_Param.Value + log2);
                }
                return 1;
            }
        }
    }
}
