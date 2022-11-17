using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _appointment = String.Empty;
        private string _size = String.Empty;
        private string _phone = String.Empty;
        private string _service = String.Empty;

        public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string Size{
            get => _size;
            set => _size = value;
        }
        public string Service{
            get => _service;
            set => _service = value;
        }
        public string Appointment{
            get => _appointment;
            set => _appointment = value;
        }
       

        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET appointment = $appointment, service = $service, SIZE = 'NULL'
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$appointment", Appointment);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                commandUpdate.Parameters.AddWithValue("$service", Service);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(appointment, phone, service, size)
            VALUES($appointment, $phone, $service, 'NULL')
        ";
                    commandInsert.Parameters.AddWithValue("$appointment", Appointment);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    commandInsert.Parameters.AddWithValue("$service", Service);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
