using MySql.Data.MySqlClient;
using PatikaWebApp.Models;

namespace PatikaWebApp.Services
{
    public class GyogyszerService
    {
        public string PostCustomer(Gyogyszer gyogyszer)
        {
            try
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= patika;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "INSERT INTO medicine(Nev, Hatoanyag, Venykoteles, Kepnev) VALUES (@nev, @hatoanyag, @venykoteles, @kepnev)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nev", (gyogyszer).Nev);
                cmd.Parameters.AddWithValue("@hatoanyag", (gyogyszer).Hatoanyag);
                cmd.Parameters.AddWithValue("@venykoteles", (gyogyszer).Venykoteles);
                cmd.Parameters.AddWithValue("@kepnev", (gyogyszer).Kepnev);
                int sorokszama = cmd.ExecuteNonQuery();
                conn.Close();
                if (sorokszama > 0)
                {
                    return "Sikeres beszúrás!";
                }
                else
                {
                    return "Sikertelen beszúrás!";
                }
            }
            catch (Exception ex)
            {
                return "Hiba történt az adatok beszúrása során: " + ex.Message;
            }

        }


        public List<Gyogyszer> GetAllGyogyszer()
        {
            List<Gyogyszer> gyogyszerek = new List<Gyogyszer>();
            try
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= patika;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "SELECT * FROM gyogyszer";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //A beolvasott adatok feldolgozása
                    Gyogyszer gyogyszer = new Gyogyszer();
                    gyogyszer.Id = reader.GetInt32("Id");
                    gyogyszer.Nev = reader.GetString("Nev");
                    gyogyszer.Hatoanyag = reader.GetString("Hatoanyag");
                    gyogyszer.Venykoteles = reader.GetBoolean("Venykoteles");
                    gyogyszer.Kepnev = reader.GetString("Kepnev");
                    gyogyszerek.Add(gyogyszer);

                }
                conn.Close();
                return gyogyszerek;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt az adatok lekérdezése során: " + ex.Message);
                return gyogyszerek;
            }
        }

        public Gyogyszer GetById(int id)
        {
            Gyogyszer result = new Gyogyszer();
            try
            {
                string connectionString = "SERVER = localhost;" +
                              "DATABASE= patika;" +
                              "UID = root;" +
                              "PASSWORD =;";
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = "SELECT * FROM gyogyszer WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //A beolvasott adatok feldolgozása
                    result.Id = reader.GetInt32("Id");
                    result.Nev = reader.GetString("Nev");
                    result.Hatoanyag = reader.GetString("Hatoanyag");
                    result.Venykoteles = reader.GetBoolean("Venykoteles");
                    result.Kepnev = reader.GetString("Kepnev");

                }
                else
                {
                    Console.WriteLine("Nincs ilyen azonosítóval rendelkező gyógyszer!");
                }

                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt az adatok lekérdezése során: " + ex.Message);
                return result;
            }
        }

        internal string PostGyogyszer(Gyogyszer gyogyszer)
        {
            throw new NotImplementedException();
        }
    }
}