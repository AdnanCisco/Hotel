using System;
using System.Collections.Generic;

namespace Hotel.Models;

public partial class Reservation
{
    public int IdRes { get; set; }

    public DateOnly DateDebut { get; set; }

    public DateOnly DateFin { get; set; }

    public int IdClient { get; set; }

    public int IdChambre { get; set; }

    public string Etat { get; set; } = null!;

    public decimal? Reduction { get; set; }

    public decimal? Total { get; set; }

    public virtual Chambre IdChambreNavigation { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual ICollection<ServiceReservation> ServiceReservations { get; set; } = new List<ServiceReservation>();
}
