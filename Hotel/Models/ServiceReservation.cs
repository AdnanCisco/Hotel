using System;
using System.Collections.Generic;

namespace Hotel.Models;

public partial class ServiceReservation
{
    public int IdServRes { get; set; }

    public int? Sessions { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Heure { get; set; }

    public int Participants { get; set; }

    public int IdRes { get; set; }

    public int IdServ { get; set; }

    public decimal? Total { get; set; }

    public virtual Reservation IdResNavigation { get; set; } = null!;

    public virtual Service IdServNavigation { get; set; } = null!;
}
