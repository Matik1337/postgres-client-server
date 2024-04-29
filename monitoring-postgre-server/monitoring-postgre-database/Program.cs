using Npgsql;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;

namespace monitoring_postgre_database
{
    /// <summary>
    /// Язык взаимодействия:
    /// 
    /// cg GetEmployeeFirstName 12
    /// cg - команда
    /// GetEmployeeFirstName - название метода
    /// 12 - аргумент
    /// 
    /// если получение обратного значения не требуется, пишется 'c'
    /// </summary>

    internal class Program
    {
        static NpgsqlHolder _databaseHolder;

        static void Main(string[] args)
        {
            string connectionString = "Host=localhost; Port = 5432; Database = monitoring; User Id = postgres; Password = 1234; ";
            _databaseHolder = new NpgsqlHolder(connectionString);

            RunServer();

            Console.ReadLine();
        }

        static Query Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<Query>(message);
        }

        static string Serialize(DataTable table)
        {

            PrintDataTable(table);
            string json = JsonConvert.SerializeObject(table, Formatting.Indented);

            Console.WriteLine(json);
            return json;
        }

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

        static void RunServer()
        {
            TcpListener server = null;

            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);

                server.Start();

                while (true)
                {
                    Console.WriteLine("Ожидание подключения... ");
                    TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("Подключен клиент! ");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[131072];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    //обработка сообщения
                    try
                    {
                        Query query = Deserialize(data);

                        if (query.Type == "SELECT" || query.Type == "Select" || query.Type == "select")
                        {
                            NpgsqlCommand command = new NpgsqlCommand(query.Statement);

                            DataTable table = _databaseHolder.ExecuteCommandCallback(command);
                            byte[] message = Encoding.UTF8.GetBytes(Serialize(table));
                            stream.Write(message, 0, message.Length);
                            stream.Flush();
                        }
                        else
                        {
                            NpgsqlCommand command = new NpgsqlCommand(query.Statement);

                            foreach (var param in query.Parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }

                            _databaseHolder.ExecuteCommand(command);
                        }

                        Console.WriteLine("Получено сообщение от клиента: {0}", query.Statement);
                        client.Close();
                    }
                    catch
                    {
                        Console.WriteLine("Сообщение не может быть прочитано");
                    }

                    continue;
                    #region old
                    if (data[0] == 'c')
                    {
                        if (data[1] == 'g')
                        {
                            var separated = data.Split(' ');

                            if (separated[2] == "GetEmployeeFirstName")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetEmployeeFirstName(Convert.ToInt32(separated[3])));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeSecondName")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetEmployeeSecondName(Convert.ToInt32(separated[3])));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeFullName")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetEmployeeFullName(Convert.ToInt32(separated[3])));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeIDByFirstName")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetEmployeeIDByFirstName(separated[3]).ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeIDBySecondName")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetEmployeeIDBySecondName(separated[3]).ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetLoaderName")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetLoaderName(Convert.ToInt32(separated[3])));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetLoaderID")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetLoaderID(separated[3]).ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetTipperName")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetTipperName(Convert.ToInt32(separated[3])));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetTipperID")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetTipperID(separated[3]).ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetTipperCapacity")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetTipperCapacity(Convert.ToInt32(separated[3])).ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeCount")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetEmployeeCount().ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetLoadersCount")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetLoadersCount().ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetTippersCount")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetTippersCount().ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeShiftCount")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetEmployeeShiftCount().ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetLoaderShiftCount")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetLoaderShiftCount().ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetTipperShiftCount")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(_databaseHolder.GetTipperShiftCount().ToString());
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetTippersTable")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(JsonDataSerializer.Serialize(_databaseHolder.GetTippersTable()));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetLoadersTable")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(JsonDataSerializer.Serialize(_databaseHolder.GetLoadersTable()));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeTable")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(JsonDataSerializer.Serialize(_databaseHolder.GetEmployeeTable()));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetEmployeeShiftTable")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(JsonDataSerializer.Serialize(_databaseHolder.GetEmployeeShiftTable()));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetLoaderShiftTable")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(JsonDataSerializer.Serialize(_databaseHolder.GetLoaderShiftTable()));
                                stream.Write(msg, 0, msg.Length);
                            }
                            else if (separated[2] == "GetTipperShiftTable")
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(JsonDataSerializer.Serialize(_databaseHolder.GetTipperShiftTable()));
                                stream.Write(msg, 0, msg.Length);
                            }

                        }
                        else
                        {
                            var separated = data.Split(' ');

                            if (separated[2] == "AddEmployee")
                            {
                                _databaseHolder.AddEmployee(separated[3], separated[4]);
                            }
                            else if (separated[2] == "AddTipper")
                            {
                                _databaseHolder.AddTipper(separated[3], Convert.ToInt32(separated[4]));
                            }
                            else if (separated[2] == "AddLoader")
                            {
                                _databaseHolder.AddLoader(separated[3]);
                            }
                            else if (separated[2] == "DeleteTipper")
                            {
                                _databaseHolder.DeleteTipper(Convert.ToInt32(separated[3]));
                            }
                            else if (separated[2] == "DeleteEmployee")
                            {
                                _databaseHolder.DeleteEmployee(Convert.ToInt32(separated[3]));
                            }
                            else if (separated[2] == "DeleteLoader")
                            {
                                _databaseHolder.DeleteLoader(Convert.ToInt32(separated[3]));
                            }
                            else if (separated[2] == "AddEmployeeShiftRow")
                            {
                                _databaseHolder.AddEmployeeShiftRow(Convert.ToInt32(separated[3]), Convert.ToSingle(separated[4]), Convert.ToSingle(separated[5]), DateTime.Parse(separated[6]));
                            }
                            else if (separated[2] == "AddLoaderShiftRow")
                            {
                                _databaseHolder.AddLoaderShiftRow(Convert.ToInt32(separated[3]), Convert.ToInt32(separated[4]), Convert.ToBoolean(separated[5]), Convert.ToSingle(separated[6]), Convert.ToSingle(separated[7]), DateTime.Parse(separated[8]));
                            }
                            else if (separated[2] == "AddTipperShiftRow")
                            {
                                _databaseHolder.AddTipperShiftRow(Convert.ToInt32(separated[3]), Convert.ToInt32(separated[4]), separated[5], Convert.ToInt32(separated[6]), Convert.ToSingle(separated[7]), Convert.ToSingle(separated[8]), DateTime.Parse(separated[9]));
                            }
                            else if (separated[2] == "DeleteEmployeeShift")
                            {
                                _databaseHolder.DeleteEmployeeShift(Convert.ToInt32(separated[3]));
                            }
                            else if (separated[2] == "DeleteLoaderShift")
                            {
                                _databaseHolder.DeleteLoaderShift(Convert.ToInt32(separated[3]));
                            }
                            else if (separated[2] == "DeleteTipperShift")
                            {
                                _databaseHolder.DeleteTipperShift(Convert.ToInt32(separated[3]));
                            }

                        }
                    }
                    #endregion
                    //конец обработки сообщения

                    

                    //byte[] msg = Encoding.UTF8.GetBytes("Сообщение получено успешно.");
                    //stream.Write(msg, 0, msg.Length);
                    //Console.WriteLine("Ответ отправлен клиенту.");

                    
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Останавливаем прослушивание
                server.Stop();
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
