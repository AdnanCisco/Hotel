using System;
using System.Collections.Generic;

namespace Hotel.Models;

public partial class Chambre
{
    public int IdChambre { get; set; }

    public string Type { get; set; } = null!;

    public int Capacite { get; set; }

    public decimal Tarif { get; set; }

    public string EtatMaintenance { get; set; } = null!;

    public string Disponibilite { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Sejour> Sejours { get; set; } = new List<Sejour>();
}
