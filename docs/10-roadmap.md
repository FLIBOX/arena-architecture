# 10 · Roadmap

- [ ] **M1 Offline vertical slice:** player motor, camera, simple health, one original weapon, one pickup, HUD.
- [ ] **M2 Match loop:** phases, spawning, elimination, respawn, shrinking zone.
- [ ] **M3 Stateful gameplay:** inventory/equipment, loot containers, weapon switching and reload.
- [ ] **M4 Vehicles and audio:** authority-safe vehicle interactions, event-driven audio.
- [ ] **M5 Networking:** command/snapshot transport, reconciliation, replication tests.
- [ ] **M6 Platform integration:** typed Android adapter for launch context, permissions, settings.

## Build order inside the combat loop

1. Fixed-tick simulation, command IDs, snapshots, event contracts.
2. `PlayerMotor` with authoritative collision and reconciliation.
3. Camera and unassisted aim.
4. Weapon runtime state, cadence, reload, authoritative ammo.
5. Hitscan hitbox queries and damage resolution.
6. Health, elimination, kill attribution, HUD events.
7. Optional local aim assist.
8. Projectiles, armor/shields, vehicles, downed/respawn modes.
