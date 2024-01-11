using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearnAPI.Model
{
    public class ViewPelatihRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public ViewPelatihRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<ViewPelatihModel> GetAllData(int id)
        {
            List<ViewPelatihModel> dataList = new List<ViewPelatihModel>();
            try
            {
                string query = @"SELECT 
                                    p.id_pengguna,
                                    (SELECT COUNT(*) FROM [AstraLearn].[dbo].[tb_pelatihan] WHERE id_pengguna = p.id_pengguna) AS jumlah_pelatihan,
                                    COUNT(p.jumlah_peserta) AS jumlah_peserta,
                                    SUM(CASE WHEN mengikuti.riwayat_section < p.jumlah_section THEN 1 ELSE 0 END) AS pelatihan_berjalan,
                                    SUM(CASE WHEN mengikuti.riwayat_section = p.jumlah_section THEN 1 ELSE 0 END) AS pelatihan_selesai
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
                    ViewPelatihModel data = new ViewPelatihModel
                    {
                        jumlah_pelatihan = Convert.ToInt32(reader["jumlah_pelatihan"]),
                        jumlah_peserta = Convert.ToInt32(reader["jumlah_peserta"]),
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
