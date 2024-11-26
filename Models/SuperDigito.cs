using System;
using System.Collections.Generic;

namespace Blazor_Server_App_Login.Models;

public partial class SuperDigito
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public long? Number { get; set; }

    public int? Result { get; set; }

    public DateTime? Date { get; set; }

    public virtual AspNetUser? User { get; set; }
}
