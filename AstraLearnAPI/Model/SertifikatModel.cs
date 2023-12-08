namespace AstraLearnAPI.Model
{
    public class SertifikatModel
    {
        public int id_sertifikat { get; set; }
        public int id_pelatihan { get; set; }
        public string nama_peserta { get; set; }
        public int nilai_exam { get; set; }
        public string tanggal_selesai { get; set; }
    }
}
