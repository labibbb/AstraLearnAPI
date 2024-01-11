using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearnAPI.Model
{
    public class ViewDataPesertaRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public ViewDataPesertaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<ViewDataPesertaModel> GetAllData(int id)
        {
            List<ViewDataPesertaModel> dataList = new List<ViewDataPesertaModel>();
            try
            {
                string query = @"SELECT
                                    COUNT(mp.id_pelatihan) AS jumlah_pelatihan,
                                    COUNT(CASE WHEN p.jumlah_section > mp.riwayat_section THEN 1 END) AS pelatihan_berjalan,
                                    COUNT(CASE WHEN p.jumlah_section = mp.riwayat_section THEN 1 END) AS pelatihan_selesai
                                FROM
                                    tb_mengikuti_pelatihan mp
                                JOIN
                                    tb_pelatihan p ON mp.id_pelatihan = p.id_pelatihan
                                WHERE
                                    mp.id_pengguna = @p1";

                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ViewDataPesertaModel data = new ViewDataPesertaModel
                    {
                        jumlah_pelatihan = Convert.ToInt32(reader["jumlah_pelatihan"]),
                        pelatihan_berjalan = Convert.ToInt32(reader["pelatihan_berjalan"]),
                        pelatihan_selesai = Convert.ToInt32(reader["pelatihan_selesai"]),
                    };
                    dataList.Add(data);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataList;
        }
    }
}
