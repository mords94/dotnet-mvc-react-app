
using System.ComponentModel.DataAnnotations;

public partial class HibernateSequence
{
    [Key]
    public long NextVal { get; set; }

    public HibernateSequence(long nextVal)
    {
        NextVal = nextVal;
    }
}