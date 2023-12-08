﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AstraLearnAPI.Model;
using Microsoft.Extensions.Configuration;

namespace AstraLearnAPI.Model
{
    public class KlasifikasiPelatihanRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public KlasifikasiPelatihanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<KlasifikasiPelatihanModel> GetAllData()
        {
            List<KlasifikasiPelatihanModel> dataList = new List<KlasifikasiPelatihanModel>();
            try
            {
                string query = "SELECT * FROM tb_klasifikasi_pelatihan";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KlasifikasiPelatihanModel data = new KlasifikasiPelatihanModel
                    {
                        id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]),
                        nama_klasifikasi = reader["nama_klasifikasi"].ToString(),
                        jumlah_pelatihan = Convert.ToInt32(reader["jumlah_pelatihan"]),
                        deskripsi = reader["deskripsi"].ToString()
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

        public KlasifikasiPelatihanModel GetData(int id)
        {
            KlasifikasiPelatihanModel data = new KlasifikasiPelatihanModel();
            try
            {
                string query = "SELECT * FROM tb_klasifikasi_pelatihan WHERE id_klasifikasi = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]);
                    data.nama_klasifikasi = reader["nama_klasifikasi"].ToString();
                    data.jumlah_pelatihan = Convert.ToInt32(reader["jumlah_pelatihan"]);
                    data.deskripsi = reader["deskripsi"].ToString();
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
            return data;
        }

        public void InsertData(KlasifikasiPelatihanModel data)
        {
            try
            {
                string query = "INSERT INTO tb_klasifikasi_pelatihan (nama_klasifikasi, jumlah_pelatihan, deskripsi) VALUES (@p1, @p2, @p3)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.nama_klasifikasi);
                command.Parameters.AddWithValue("@p2", data.jumlah_pelatihan);
                command.Parameters.AddWithValue("@p3", data.deskripsi);
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateData(KlasifikasiPelatihanModel data)
        {
            try
            {
                string query = "UPDATE tb_klasifikasi_pelatihan " +
                               "SET nama_klasifikasi = @p2, deskripsi = @p3 " +
                               "WHERE id_klasifikasi = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_klasifikasi);
                command.Parameters.AddWithValue("@p2", data.nama_klasifikasi);
                command.Parameters.AddWithValue("@p3", data.deskripsi);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteData(int id)
        {
            try
            {
                string query = "DELETE FROM tb_klasifikasi_pelatihan WHERE id_klasifikasi = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
