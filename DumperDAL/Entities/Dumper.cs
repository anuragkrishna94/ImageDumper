using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumperDAL.Entities
{
    public class Dumper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string UniqueTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDestroyed { get; set; }
        public ICollection<DumperBin> DumperBins { get; set; }

    }
}
