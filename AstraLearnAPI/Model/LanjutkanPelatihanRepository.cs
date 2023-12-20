﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearnAPI.Model
{
    public class LanjutkanPelatihanRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public LanjutkanPelatihanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<LanjutkanPelatihanModel> GetLanjutkanPelatihan(int idPengguna)
        {
            List<LanjutkanPelatihanModel> pelatihanList = new List<LanjutkanPelatihanModel>();
            try
            {
                string query = @"SELECT 
                                    tb_pelatihan.id_pelatihan,
                                    tb_pelatihan.nama_pelatihan, 
                                    tb_pelatihan.deskripsi_pelatihan,
                                    tb_klasifikasi_pelatihan.nama_klasifikasi,
                                    tb_pelatihan.jumlah_peserta, 
                                    tb_pelatihan.nilai
                                FROM 
                                    tb_mengikuti_pelatihan
                                    JOIN tb_pelatihan ON tb_mengikuti_pelatihan.id_pelatihan = tb_pelatihan.id_pelatihan
                                    JOIN tb_klasifikasi_pelatihan ON tb_pelatihan.id_klasifikasi = tb_klasifikasi_pelatihan.id_klasifikasi
                                WHERE 
                                    tb_mengikuti_pelatihan.id_pengguna = @p1";

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", idPengguna);
                    _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LanjutkanPelatihanModel pelatihan = new LanjutkanPelatihanModel
                            {
                                id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                                nama_pelatihan = Convert.ToString(reader["nama_pelatihan"]),
                                deksripsi_pelatihan = Convert.ToString(reader["deskripsi_pelatihan"]),
                                nama_klasifikasi = Convert.ToString(reader["nama_klasifikasi"]),
                                jumlah_peserta = Convert.ToInt32(reader["jumlah_peserta"]),
                                nilai = Convert.ToInt32(reader["nilai"])
                            };
                            pelatihanList.Add(pelatihan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return pelatihanList;
        }
    }
}
