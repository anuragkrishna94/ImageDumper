using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumperDAL.Entities
{
    public class DumperObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FileName { get; set; }
        public int ParentDumperID { get; set; }
        public int ParentDumperBinID { get; set; }
    }
}
