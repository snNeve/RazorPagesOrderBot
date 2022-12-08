using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _appointment = String.Empty;
        
        private string _phone = String.Empty;
        private string _service = String.Empty;

        private string _datetime = String.Empty;

        public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string Service{
            get => _service;
            set => _service = value;
        }
        public string Appointment{
            get => _appointment;
            set => _appointment = value;
        }

        public string Datetime{
            get => _datetime;
            set => _datetime = value;
        }

        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET appointment = $appointment, service = $service, SIZE = 'NULL', datetime = $datetime
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$appointment", Appointment);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                commandUpdate.Parameters.AddWithValue("$service", Service);
                commandUpdate.Parameters.AddWithValue("$datetime", Datetime);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(appointment, phone, service, size, datetime)
            VALUES($appointment, $phone, $service, 'NULL', $datetime)
        ";
                    commandInsert.Parameters.AddWithValue("$appointment", Appointment);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    commandInsert.Parameters.AddWithValue("$service", Service);
                    commandInsert.Parameters.AddWithValue("$datetime", Datetime);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
