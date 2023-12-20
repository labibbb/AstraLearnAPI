﻿using Microsoft.AspNetCore.Mvc;

namespace AstraLearnAPI.Model
{
    public class LanjutkanPelatihanModel : Controller
    {
        public int id_pelatihan { get; set; }
        public string nama_pelatihan { get; set; }
        public string deksripsi_pelatihan { get; set; }
        public string nama_klasifikasi { get; set; }
        public int jumlah_peserta { get; set; }
        public int nilai { get; set; }
    }
}
