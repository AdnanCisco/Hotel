using System;
using System.Collections.Generic;

namespace Hotel.Models;

public partial class Sejour
{
    public int IdSejour { get; set; }

    public DateOnly DateDebut { get; set; }

    public DateOnly DateFin { get; set; }

    public int IdChambre { get; set; }

    public int IdClient { get; set; }

    public virtual Chambre IdChambreNavigation { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;
}
