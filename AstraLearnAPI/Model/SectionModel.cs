namespace AstraLearnAPI.Model
{
    public class SectionModel
    {
        public int id_section { get; set; }
        public int id_pelatihan { get; set; }
        public string nama_section { get; set; }
        public string video_pembelajaran { get; set; }
        public string modul_pembelajaran { get; set; }
        public string deskripsi { get; set; }
        public byte[] isi_modul { get; set; }
    }
}
