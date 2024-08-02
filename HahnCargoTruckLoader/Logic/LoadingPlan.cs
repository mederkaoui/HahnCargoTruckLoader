using HahnCargoTruckLoader.Model;

namespace HahnCargoTruckLoader.Logic
{
    public class LoadingPlan
    {
        private readonly Dictionary<int, LoadingInstruction> _instructions;
        private readonly Truck _truck;
        private readonly List<Crate> _crates;

        public LoadingPlan(Truck truck, List<Crate> crates)
        {
            _instructions = [];
            _truck = truck;
            _crates = crates;
        }

        public Dictionary<int, LoadingInstruction> GetLoadingInstructions() {

            // Calculate total volume of all crates
            int totalCrateVolume = _crates.Sum(c => c.Width * c.Height * c.Length);

            // Calculate cargo volume of the truck
            int truckVolume = _truck.Width * _truck.Height * _truck.Length;

            // Check if total volume of crates exceeds truck volume
            if (totalCrateVolume > truckVolume)
            {
                Console.WriteLine($"The total volume of the crates: {totalCrateVolume}, exceeds the truck's cargo capacity: {truckVolume}. All crates cannot be placed.");
                //throw new Exception($"The total volume of the crates: {totalCrateVolume}, exceeds the truck's cargo capacity: {truckVolume}. All crates cannot be placed.");
            }

            var cargoSpace = new bool[_truck.Width, _truck.Height, _truck.Length];

            // Sort crates by volume in descending order
            var sortedCrates = _crates.OrderByDescending(c => c.Width * c.Height * c.Length).ToList();

            int stepNumber = 1;

            foreach (var crate in sortedCrates)
            {
                bool placed = false;

                // Try all possible orientations
                var orientations = new List<(int w, int h, int l)>
                {
                    (crate.Width, crate.Height, crate.Length), // Original
                    (crate.Length, crate.Height, crate.Width), // Rotated horizontally
                    (crate.Width, crate.Length, crate.Height)  // Rotated vertically
                };

                foreach (var (width, height, length) in orientations)
                {
                    if (placed) break;

                    placed = TryPlaceCrateAtOrientation(crate, width, height, length, cargoSpace, ref stepNumber);
                }

                if (!placed)
                {
                    Console.WriteLine($"Unable to place crate with ID {crate.CrateID}.");
                    //throw new Exception($"Unable to place crate with ID {crate.CrateID}.");
                }
            }

            return _instructions;
        }

        private bool TryPlaceCrateAtOrientation(Crate crate, int width, int height, int length, bool[,,] cargoSpace, ref int stepNumber)
        {
            for (int x = 0; x <= _truck.Width - width; x++)
            {
                for (int y = 0; y <= _truck.Height - height; y++)
                {
                    for (int z = 0; z <= _truck.Length - length; z++)
                    {
                        if (CanFit(width, height, length, x, y, z, cargoSpace))
                        {
                            // Place the crate
                            for (int dx = 0; dx < width; dx++)
                            {
                                for (int dy = 0; dy < height; dy++)
                                {
                                    for (int dz = 0; dz < length; dz++)
                                    {
                                        cargoSpace[x + dx, y + dy, z + dz] = true;
                                    }
                                }
                            }

                            // Add loading instruction
                            var instruction = new LoadingInstruction
                            {
                                LoadingStepNumber = stepNumber++,
                                CrateId = crate.CrateID,
                                TopLeftX = x,
                                TopLeftY = y,
                                TurnHorizontal = (width == crate.Length && height == crate.Height && length == crate.Width),
                                TurnVertical = (width == crate.Width && height == crate.Length && length == crate.Height)
                            };

                            _instructions[crate.CrateID] = instruction;

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CanFit(int width, int height, int length, int x, int y, int z, bool[,,] cargoSpace)
        {
            for (int dx = 0; dx < width; dx++)
            {
                for (int dy = 0; dy < height; dy++)
                {
                    for (int dz = 0; dz < length; dz++)
                    {
                        if (cargoSpace[x + dx, y + dy, z + dz])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}