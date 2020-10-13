# Expanded Initiative
This is a mod for the [HBS BattleTech](http://battletechgame.com/) game that divides each round of play into 10 initiatives phases instead of the normal 5. Units are organized into brackets by their tonnage, with every 10 tons being a new step in the bracket. Thus units will go in phase:

| Phase | Tonnage | Notes |
| ----- | ------- | -- |
| 10    | < 20    | |
| 9     | 20-25   | |
| 8     | 30-35   | Light Turrets |
| 7     | 40-45   | |
| 6     | 50-55   | Medium Turrets |
| 5     | 60-65   | |
| 4     | 70-75   | Heavy Turrets |
| 3     | 80-90   | |
| 2     | 95-100  | Default |
| 1     | 100+    | |

Light, medium, and heavy turrets are identified by the `unit_light`, `unit_medium`, and `unit_heavy` tags on turrets.

This mod requires [https://github.com/iceraptor/IRBTModUtils/]. Grab the latest release of __IRBTModUtils__ and extract it in your Mods/ directory alongside of this mod.

This mod uses assets from [https://game-icons.net/], which are licensed through a CC BY 3.0 license. I've modified these icons by making them transparent, or changing the color of the icon, but otherwise they are unmodified. 

## Pilot Coloration

The pilot icons in the HUD and the initiative diamonds on all units will display new custom colors. You can modify these values by editing the values in `mod.json`. You must use RGBA float values (NOT HEX VALUES). You'll also find several localizable strings that you are free to change, so long as you keep any values like `{0}` or `{1}` in your modified string.