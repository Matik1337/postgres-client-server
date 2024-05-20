using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Net.Sockets;
using System.Text;

namespace monnitoring_postgre_client
{
    internal class Program
    {

        /// <summary>
        /// В методе Main придевен пример взаимодействия с бд. 
        /// Реализованных методов в регионах должно хватить для полноценной работы
        /// но ты можешь написать свои xd
        /// </summary>

        //just for example
        static void Main(string[] args)
        {
            try
            {
                //получение таблици работников
                DataTable employee = SendWithCallback(GetLoaderShiftTable());
                //получение id последнего работника
                PrintDataTable(employee);
                int lastID = GetLastID(employee);
                //добавление нового работника
                Send(AddLoader(lastID + 1, "firstname"));
                //получение и вывод измененной таблицы
                PrintDataTable(SendWithCallback(GetLoaderTable()));
                //удаление работника, которого только что добавили
                Send(DeleteLoader(lastID + 1));
                //получение и вывод измененной таблицы
                PrintDataTable(SendWithCallback(GetLoaderTable()));

            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }

            Console.ReadLine();
        }

        #region Network

        public static void Send(Query query)
        {
            TcpClient client = new TcpClient("127.0.0.1", 13000);

            // Получаем поток для чтения и записи данных
            NetworkStream stream = client.GetStream();

            // Отправляем данные серверу
            byte[] msg = Encoding.UTF8.GetBytes(Serialize(query));
            stream.Write(msg, 0, msg.Length);
            stream.Flush();
        }

        public static DataTable SendWithCallback(Query query)
        {
            TcpClient client = new TcpClient("127.0.0.1", 13000);

            // Получаем поток для чтения и записи данных
            NetworkStream stream = client.GetStream();

            // Отправляем данные серверу
            byte[] msg = Encoding.UTF8.GetBytes(Serialize(query));
            stream.Write(msg, 0, msg.Length);
            stream.Flush();

            // Создаем буфер для хранения данных
            byte[] data = new byte[131072];

            // Читаем данные от сервера
            int bytesRead = stream.Read(data, 0, data.Length);
            string responseData = Encoding.UTF8.GetString(data, 0, bytesRead);
            Console.WriteLine("Получено сообщение от сервера: ");
            return Deserialize(responseData);
        }

        #endregion

        #region Getters
        private static Query GetTable(string tableName)
        {
            Query query = new Query("SELECT", $"SELECT * FROM {tableName}", new Dictionary<string, object>());

            return query;
        }

        private static Query GetTipperTable()
        {
            Query query = new Query("SELECT", $"SELECT * FROM tipper", new Dictionary<string, object>());

            return query;
        }

        private static Query GetEmployeeTable()
        {
            Query query = new Query("SELECT", "SELECT * FROM employee", new Dictionary<string, object>());
            return query;
        }

        private static Query GetLoaderTable()
        {
            Query query = new Query("SELECT", "SELECT * FROM loader", new Dictionary<string, object>());
            return query;
        }

        private static Query GetTipperShiftTable()
        {
            Query query = new Query("SELECT", "SELECT * FROM shift_tipper", new Dictionary<string, object>());
            return query;
        }

        private static Query GetEmployeeShiftTable()
        {
            Query query = new Query("SELECT", "SELECT * FROM shift_employee", new Dictionary<string, object>());
            return query;
        }

        private static Query GetLoaderShiftTable()
        {
            Query query = new Query("SELECT", "SELECT * FROM shift_loader", new Dictionary<string, object>());
            return query;
        }
        #endregion

        #region Adders
        public static Query AddEmployee(int id, string firstname, string lastname)
        {
            string sql = "INSERT INTO employee (id, first_name, last_name) VALUES (@id, @name1, @name2)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@id", id);
            parameters.Add("@name1", firstname);
            parameters.Add("@name2", lastname);

            Query query = new Query("INSERT", sql, parameters);

            return query;
        }

        public static Query AddTipper(int id, string name, int payload)
        {
            string sql = "INSERT INTO tipper (id, machine_name, payload) VALUES (@id, @name, @payload)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@payload", payload);

            Query query = new Query("INSERT", sql, parameters);

            return query;
        }

        public static Query AddLoader(int id, string name)
        {
            string sql = "INSERT INTO loader (id, machine_name) VALUES (@id, @name)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@id", id);
            parameters.Add("@name", name);

            Query query = new Query("INSERT", sql, parameters);

            return query;
        }

        public static Query AddEmployeeShiftRow(int id, int employeeId, float posX, float posY, DateTime moment)
        {
            string sql = "INSERT INTO shift_employee (id, employee_id, pos_x, pos_y, moment) VALUES (@id, @employeeId, @posX, @posY, @moment)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@id", id);
            parameters.Add("@employeeId", employeeId);
            parameters.Add("@posX", posX);
            parameters.Add("@posY", posY);
            parameters.Add("@moment", moment);

            Query query = new Query("INSERT", sql, parameters);

            return query;
        }

        public static Query AddLoaderShiftRow(int id, int employeeId, int loaderId, bool isLoading, float posX, float posY, DateTime moment)
        {
            string sql = "INSERT INTO shift_loader (id, employee_id, loader_id, is_loading, pos_x, pos_y, moment) VALUES (@id, @employeeId, @loaderId, @isLoading, @posX, @posY, @moment)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@id", id);
            parameters.Add("@employeeId", employeeId);
            parameters.Add("@loaderId", loaderId);
            parameters.Add("@isLoading", isLoading);
            parameters.Add("@posX", posX);
            parameters.Add("@posY", posY);
            parameters.Add("@moment", moment);

            Query query = new Query("INSERT", sql, parameters);

            return query;
        }

        public static Query AddTipperShiftRow(int id, int employeeId, int tipperId, string oreType, int oreCount, float posX, float posY, DateTime moment)
        {
            string sql = "INSERT INTO shift_tipper (id, employee_id, tipper_id, ore_type, ore_count, pos_x, pos_y, moment) VALUES (@id, @employeeId, @tipperId, @oreType, @oreCount, @posX, @posY, @moment)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@id", id);
            parameters.Add("@employeeId", employeeId);
            parameters.Add("@tipperId", tipperId);
            parameters.Add("@oreType", oreType);
            parameters.Add("@oreCount", oreCount);
            parameters.Add("@posX", posX);
            parameters.Add("@posY", posY);
            parameters.Add("@moment", moment);

            Query query = new Query("INSERT", sql, parameters);

            return query;
        }

        #endregion

        #region Removers

        public static Query DeleteEmployee(int id)
        {
            string sql = "DELETE FROM employee WHERE id = @id";
            Dictionary<string, object> dic = new Dictionary<string, object>(); 
            dic.Add("@id", id);
            Query query = new Query("DELETE", sql, dic);

            NpgsqlCommand cmd = new NpgsqlCommand(sql);

            cmd.Parameters.AddWithValue("@id", id);
            return query;
        }

        public static Query DeleteTipper(int id)
        {
            string sql = "DELETE FROM tipper WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            Query query = new Query("DELETE", sql, parameters);
            return query;
        }

        public static Query DeleteLoader(int id)
        {
            string sql = "DELETE FROM loader WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            Query query = new Query("DELETE", sql, parameters);
            return query;
        }

        public static Query DeleteEmployeeShift(int id)
        {
            string sql = "DELETE FROM shift_employee WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            Query query = new Query("DELETE", sql, parameters);
            return query;
        }

        public static Query DeleteLoaderShift(int id)
        {
            string sql = "DELETE FROM shift_loader WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            Query query = new Query("DELETE", sql, parameters);
            return query;
        }

        public static Query DeleteTipperShift(int id)
        {
            string sql = "DELETE FROM shift_tipper WHERE id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            Query query = new Query("DELETE", sql, parameters);
            return query;
        }


        #endregion

        #region Counters

        private static int GetLastID(DataTable table)
        {
            // Проверяем, что таблица содержит хотя бы одну строку
            if (table.Rows.Count > 0)
            {
                // Получаем последнюю строку таблицы
                DataRow lastRow = table.Rows[table.Rows.Count - 1];

                return Convert.ToInt32(lastRow[0]);
            }

            // Если таблица пуста или в первой колонке нет значений, возвращаем 0
            return 0;
        }

        #endregion

        #region Serialization

        private static string Serialize(Query query)
        {
            return JsonConvert.SerializeObject(query);
        }
        
        private static DataTable Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<DataTable>(message);
        }

        #endregion

        //just for example
        private static void PrintDataTable(DataTable table)
        {
            // Проверяем, что таблица существует и содержит хотя бы одну строку
            if (table != null && table.Rows.Count > 0)
            {
                // Получаем количество столбцов в таблице
                int columnCount = table.Columns.Count;

                // Выводим заголовки столбцов
                for (int i = 0; i < columnCount; i++)
                {
                    Console.Write(table.Columns[i].ColumnName + "\t");
                }
                Console.WriteLine();

                // Выводим данные из каждой строки
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < columnCount; i++)
                    {
                        Console.Write(row[i].ToString() + "\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Таблица пуста или не существует.");
            }
        }
    }

    class Query
    {
        public string Type { get; set; } // Тип запроса (например, "SELECT", "INSERT", "DELETE" и т.д.)
        public string Statement { get; set; } // SQL-запрос
        public Dictionary<string, object> Parameters { get; set; } // Параметры запроса

        public Query(string type, string statement, Dictionary<string, object> parameters)
        {
            Type = type;
            Statement = statement;
            Parameters = parameters;
        }
    }
}
