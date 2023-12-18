namespace AstraLearnAPI.Model
{
    public class MengikutiPelatihanModel
    {
        public int id_mengikuti { get; set; }
        public int id_pengguna { get; set; }
        public int id_pelatihan { get; set; }
        public int riwayat_section { get; set; }
        public int riwayat_nilai { get; set; }
        public float rating { get; set; }
    }
}
