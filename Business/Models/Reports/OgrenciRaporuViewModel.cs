#nullable disable

using N4CoreLite.Models;

namespace Business.Models.Reports
{
    public class OgrenciRaporuViewModel : PageOrderModel
    {
        public List<OgrenciRaporuQueryModel> List { get; set; }
        public OgrenciRaporuCommandModel Command { get; set; }
        public Dictionary<string, string> MezunDictionary { get; set; }
        public Dictionary<string, string> SinifDictionary { get; set; }
        public Dictionary<string, string> UyrukDictionary { get; set; }
        public Dictionary<string, string> DersDictionary { get; set; }
        public string Message { get; set; }
    }
}
