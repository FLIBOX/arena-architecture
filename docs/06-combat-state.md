# 06 · Combat state & animation sync

Combat action state is separate from locomotion state; each can restrict the other explicitly.

| Combat state | Movement effect | Animation effect |
|---|---|---|
| Ready | Normal | Locomotion blend tree |
| Aiming | Optional reduced turn/move | Aim layer / upper-body pose |
| Firing | Short lock only if the weapon requires it | Fire trigger, recoil pose |
| Reloading | Configured reduction; jump policy explicit | Reload state with event marker |
| Switching | Optional input lock | Holster/equip transition |
| Disabled | Motor input disabled | Downed/death/spectator |

## Sync rule

Transitions happen on the authority first. Animation receives state and normalized action progress as presentation data. Animation events may request cosmetic timing but must never grant ammo, damage or weapon readiness.
