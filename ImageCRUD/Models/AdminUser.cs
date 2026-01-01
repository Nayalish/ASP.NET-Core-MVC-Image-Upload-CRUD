using System;
using System.Collections.Generic;

namespace ImageCRUD.Models;

public partial class AdminUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
