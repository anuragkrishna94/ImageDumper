using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumperDAL.Entities
{
    public class DumperBin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string DumperBinTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dumper Dumper { get; set; }
    }
}
