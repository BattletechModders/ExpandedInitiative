using BattleTech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedInitiative.Helper {
    public static class UnitHelper {
        public static int InitFromTonnage(this Mech mech) {
            if (mech.tonnage < 20) { return 10; } else
            if (mech.tonnage <= 25) { return 9; } else
            if (mech.tonnage <= 35) { return 8; } else
            if (mech.tonnage <= 45) { return 7; } else
            if (mech.tonnage <= 55) { return 6; } else
            if (mech.tonnage <= 65) { return 5; } else
            if (mech.tonnage <= 75) { return 4; } else
            if (mech.tonnage <= 90) { return 3; } else
            if (mech.tonnage <= 100) { return 2; } else { return 1; }
        }

        public static int InitFromTonnage(this Vehicle vehicle) {
            if (vehicle.tonnage < 20) { return 10; } else
            if (vehicle.tonnage <= 25) { return 9; } else
            if (vehicle.tonnage <= 35) { return 8; } else
            if (vehicle.tonnage <= 45) { return 7; } else
            if (vehicle.tonnage <= 55) { return 6; } else
            if (vehicle.tonnage <= 65) { return 5; } else
            if (vehicle.tonnage <= 75) { return 4; } else
            if (vehicle.tonnage <= 90) { return 3; } else
            if (vehicle.tonnage <= 100) { return 2; } else { return 1; }
        }

        public static int InitFromTonnage(this Turret turret) {
            return 0;
        }
    }
}
