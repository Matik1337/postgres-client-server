using Npgsql;
using System;
using System.Data;

namespace monitoring_postgre_database
{
    internal class NpgsqlHolder
    {
        private string _connectionString;
        private NpgsqlConnection _connection;

        public NpgsqlHolder(string connectionString) 
        {
            _connectionString = connectionString;
            _connection = new NpgsqlConnection(_connectionString);

            _connection.Open();
        }

        public void ExecuteCommand(NpgsqlCommand command)
        {
            command.Connection = _connection;
            command.ExecuteNonQuery();
            
        }

        public DataTable ExecuteCommandCallback(NpgsqlCommand command)
        {
            DataTable dataTable = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);

            command.Connection = _connection;
            adapter.Fill(dataTable);

            return dataTable;
        }

        #region BaseTables

        /// <summary>
        /// это методы для работы с базовыми таблицами
        /// emoloyee - работник
        /// loader - погрузчик
        /// tipper - самосвал
        /// </summary>

        public string GetEmployeeFirstName(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from employee", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string employeeName = reader.GetString(1);

                if(currentId == id)
                {
                    return employeeName;
                }
            }

            return null;
        }

        public string GetEmployeeSecondName(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from employee", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string employeeName = reader.GetString(2);

                if (currentId == id)
                {
                    return employeeName;
                }
            }

            return null;
        }

        public string GetEmployeeFullName(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from employee", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string employeeName1 = reader.GetString(1);
                string employeeName2 = reader.GetString(2);

                if (currentId == id)
                {
                    return employeeName1 + " " + employeeName2;
                }
            }

            return null;
        }

        public int GetEmployeeIDByFirstName(string employeeName)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from employee", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string currentName = reader.GetString(1);

                if (currentName == employeeName)
                {
                    return currentId;
                }
            }

            return -1;
        }

        public int GetEmployeeIDBySecondName(string employeeName)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from employee", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string currentName = reader.GetString(2);

                if (currentName == employeeName)
                {
                    return currentId;
                }
            }

            return -1;
        }

        public string GetLoaderName(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from loader", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string loaderName = reader.GetString(1);

                if (currentId == id)
                {
                    return loaderName;
                }
            }

            return null;
        }

        public int GetLoaderID(string loaderName)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from loader", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string currentName = reader.GetString(1);

                if (currentName == loaderName)
                {
                    return currentId;
                }
            }

            return -1;
        }

        public string GetTipperName(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from tipper", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string tipperName = reader.GetString(1);

                if (currentId == id)
                {
                    return tipperName;
                }
            }

            return null;
        }

        public int GetTipperID(string tipperName)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from tipper", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string currentName = reader.GetString(1);

                if (currentName == tipperName)
                {
                    return currentId;
                }
            }

            return -1;
        }

        public int GetTipperCapacity(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("Select * from tipper", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int currentId = reader.GetInt32(0);
                string currentName = reader.GetString(1);
                int capacity = reader.GetInt32(2);

                if (id == currentId)
                {
                    return capacity;
                }
            }

            return -1;
        }

        public DataTable GetTippersTable()
        {
            DataTable dt = new DataTable();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM tipper", _connection);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);

            dataAdapter.Fill(dt);

            return dt;  
        }

        public DataTable GetLoadersTable()
        {
            DataTable dt = new DataTable();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM loader", _connection);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);

            dataAdapter.Fill(dt);

            return dt;
        }

        public DataTable GetEmployeeTable()
        {
            DataTable dt = new DataTable();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM employee", _connection);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);

            dataAdapter.Fill(dt);

            return dt;
        }

        public void AddEmployee(string firstname, string lastname)
        {
            string sql = "INSERT INTO employee (id, first_name, last_name) VALUES (@id, @name1, @name2)";
            NpgsqlCommand command = new NpgsqlCommand(sql, _connection);
            
            command.Parameters.AddWithValue("@id", GetEmployeeCount() + 1);
            command.Parameters.AddWithValue("@name1", firstname);
            command.Parameters.AddWithValue("@name2", lastname);

            command.ExecuteNonQuery();
        }

        public void AddTipper(string name, int payload)
        {
            string sql = "INSERT INTO tipper (id, machine_name, payload) VALUES (@id, @name, @payload)";
            NpgsqlCommand command = new NpgsqlCommand(sql, _connection);

            command.Parameters.AddWithValue("@id", GetTippersCount() + 1);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@payload", payload);

            command.ExecuteNonQuery();
        }

        public void AddLoader(string name)
        {
            string sql = "INSERT INTO loader (id, machine_name) VALUES (@id, @name)";
            NpgsqlCommand command = new NpgsqlCommand(sql, _connection);

            command.Parameters.AddWithValue("@id", GetTippersCount() + 1);
            command.Parameters.AddWithValue("@name", name);

            command.ExecuteNonQuery();
        }

        public int GetEmployeeCount()
        {
            string countQuery = "SELECT COUNT(*) FROM employee";
            NpgsqlCommand countCmd = new NpgsqlCommand(countQuery, _connection);
            return Convert.ToInt32(countCmd.ExecuteScalar());
        }

        public int GetLoadersCount()
        {
            string countQuery = "SELECT COUNT(*) FROM loader";
            NpgsqlCommand countCmd = new NpgsqlCommand(countQuery, _connection);
            return Convert.ToInt32(countCmd.ExecuteScalar());
        }

        public int GetTippersCount()
        {
            string countQuery = "SELECT COUNT(*) FROM tipper";
            NpgsqlCommand countCmd = new NpgsqlCommand(countQuery, _connection);
            return Convert.ToInt32(countCmd.ExecuteScalar());
        }

        public void DeleteEmployee(int id)
        {
            string sql = "DELETE FROM employee WHERE id = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);
            
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteTipper(int id)
        {
            string sql = "DELETE FROM tipper WHERE id = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteLoader(int id)
        {
            string sql = "DELETE FROM loader WHERE id = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        #endregion

        #region ShiftTables

        

        public DataTable GetEmployeeShiftTable()
        {
            DataTable dataTable = new DataTable();

            string sql = "SELECT * FROM shift_employee";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            return dataTable;
        }

        public DataTable GetLoaderShiftTable()
        {
            DataTable dataTable = new DataTable();

            string sql = "SELECT * FROM shift_loader";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            return dataTable;
        }

        public DataTable GetTipperShiftTable()
        {
            DataTable dataTable = new DataTable();

            string sql = "SELECT * FROM shift_tipper";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            return dataTable;
        }

        public int GetEmployeeShiftCount()
        {
            string countQuery = "SELECT COUNT(*) FROM shift_employee";
            NpgsqlCommand countCmd = new NpgsqlCommand(countQuery, _connection);
            return Convert.ToInt32(countCmd.ExecuteScalar());
        }

        public int GetLoaderShiftCount()
        {
            string countQuery = "SELECT COUNT(*) FROM shift_loader";
            NpgsqlCommand countCmd = new NpgsqlCommand(countQuery, _connection);
            return Convert.ToInt32(countCmd.ExecuteScalar());
        }

        public int GetTipperShiftCount()
        {
            string countQuery = "SELECT COUNT(*) FROM shift_tipper";
            NpgsqlCommand countCmd = new NpgsqlCommand(countQuery, _connection);
            return Convert.ToInt32(countCmd.ExecuteScalar());
        }

        public void AddEmployeeShiftRow(int employeeId, float posX, float posY, DateTime moment)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "INSERT INTO shift_employee (id, employee_id, pos_x, pos_y, moment) VALUES (@id, @employeeId, @posX, @posY, @moment)";

            cmd.Parameters.AddWithValue("@id", GetEmployeeShiftCount());
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.Parameters.AddWithValue("@posX", posX);
            cmd.Parameters.AddWithValue("@posY", posY);
            cmd.Parameters.AddWithValue("@moment", moment);

            cmd.ExecuteNonQuery();
        }

        public void AddLoaderShiftRow(int employeeId, int loaderId, bool isLoading, float posX, float posY, DateTime moment)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "INSERT INTO shift_loader (id, employee_id, loader_id, is_loading, pos_x, pos_y, moment) VALUES (@id, @employeeId, @loaderId, @isLoading, @posX, @posY, @moment)";

            cmd.Parameters.AddWithValue("@id", GetLoaderShiftCount());
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.Parameters.AddWithValue("@loaderId", loaderId);
            cmd.Parameters.AddWithValue("@isLoading", isLoading);
            cmd.Parameters.AddWithValue("@posX", posX);
            cmd.Parameters.AddWithValue("@posY", posY);
            cmd.Parameters.AddWithValue("@moment", moment);

            cmd.ExecuteNonQuery();
        }

        public void AddTipperShiftRow(int employeeId, int tipperId, string oreType, int oreCount, float posX, float posY, DateTime moment)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "INSERT INTO shift_tipper (id, employee_id, tipper_id, ore_type, ore_count, pos_x, pos_y, moment) VALUES (@id, @employeeId, @tipperId, @oreType, @oreCount, @posX, @posY, @moment)";

            cmd.Parameters.AddWithValue("@id", GetTipperShiftCount());
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.Parameters.AddWithValue("@tipperId", tipperId);
            cmd.Parameters.AddWithValue("@oreType", oreType);
            cmd.Parameters.AddWithValue("@oreCount", oreCount);
            cmd.Parameters.AddWithValue("@posX", posX);
            cmd.Parameters.AddWithValue("@posY", posY);
            cmd.Parameters.AddWithValue("@moment", moment);

            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployeeShift(int id)
        {
            string sql = "DELETE FROM shift_employee WHERE id = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteLoaderShift(int id)
        {
            string sql = "DELETE FROM shift_loader WHERE id = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void DeleteTipperShift(int id)
        {
            string sql = "DELETE FROM shift_tipper WHERE id = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        #endregion
    }
}
