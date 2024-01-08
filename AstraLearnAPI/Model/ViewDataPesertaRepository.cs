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
                                    p.id_pengguna,
                                    COUNT(*) AS jumlah_pelatihan,
                                    SUM(jumlah_peserta) AS jumlah_peserta,
                                    SUM(CASE WHEN riwayat_section < jumlah_section THEN 1 ELSE 0 END) AS pelatihan_berjalan,
                                    SUM(CASE WHEN riwayat_section = jumlah_section THEN 1 ELSE 0 END) AS pelatihan_selesai
                                FROM 
                                    [AstraLearn].[dbo].[tb_pelatihan] p
                                JOIN
                                    [AstraLearn].[dbo].[tb_mengikuti_pelatihan] mengikuti ON p.id_pelatihan = mengikuti.id_pelatihan
                                WHERE
                                    p.id_pengguna = @p1 
                                GROUP BY 
                                    p.id_pengguna";

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
