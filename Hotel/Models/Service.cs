using System;
using System.Collections.Generic;

namespace Hotel.Models;

public partial class Service
{
    public int IdServ { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Tarif { get; set; }

    public int Capacite { get; set; }

    public int DureeSession { get; set; }

    public string Disponibilite { get; set; } = null!;

    public virtual ICollection<ServiceReservation> ServiceReservations { get; set; } = new List<ServiceReservation>();
}
