using System;
using System.Collections.Generic;

namespace Hotel.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Preference { get; set; }

    public int? PointsFidel { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Sejour> Sejours { get; set; } = new List<Sejour>();
}
