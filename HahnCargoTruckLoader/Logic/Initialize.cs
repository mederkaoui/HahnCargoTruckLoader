using HahnCargoTruckLoader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnCargoTruckLoader.Logic
{
  public static class Initialize
  {
    public static Truck LoadTruck()
    {
      return new Truck { TruckType = "H-01", Height = 2, Width = 3, Length = 4 }; //24
    }

    public static List<Crate> GetCrates()
    {
      var crates = new List<Crate>();
      crates.Add(new Crate { CrateID = 0, Height = 2, Width = 1, Length = 4 }); //8
      crates.Add(new Crate { CrateID = 1, Height = 1, Width = 2, Length = 2 }); //4
      crates.Add(new Crate { CrateID = 2, Height = 2, Width = 2, Length = 1 }); //4
      crates.Add(new Crate { CrateID = 3, Height = 1, Width = 1, Length = 2 }); //2
      crates.Add(new Crate { CrateID = 4, Height = 1, Width = 2, Length = 1 }); //2
      crates.Add(new Crate { CrateID = 5, Height = 1, Width = 1, Length = 1 }); //1
      crates.Add(new Crate { CrateID = 6, Height = 1, Width = 1, Length = 1 }); //1
      crates.Add(new Crate { CrateID = 7, Height = 1, Width = 2, Length = 2 }); //2

      return crates;
    }

  }
}
