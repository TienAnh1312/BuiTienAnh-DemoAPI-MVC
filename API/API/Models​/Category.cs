﻿using System;
using System.Collections.Generic;

namespace API.Models​;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
}
