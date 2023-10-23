﻿using System;
using System.Collections.Generic;

namespace EFCoreIntroductionDemo.Models;

public partial class Town
{
    public int TownId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
