using System;
using System.Collections.Generic;

namespace Hotel.Models;

public partial class Employee
{
    public int IdEmp { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telephone { get; set; }

    public string Poste { get; set; } = null!;
}
