# 03 · Weapons & ammunition

Use a data-driven `WeaponDefinition` (ScriptableObject) plus authoritative `WeaponRuntimeState`. Hitscan and projectile fire sit behind one `IShotResolver`.

## Fire pipeline

1. Weapon is `Ready`.
2. Fire input active and `tick >= NextFireTick`.
3. Magazine has ammo.
4. Build aim ray from submitted origin/direction.
5. Add deterministic spread from an authority-owned seed and shot sequence.
6. Resolve via `HitscanResolver` or `ProjectileSpawner`.
7. Deduct ammo only after the authority accepts the shot.
8. Advance `NextFireTick`.
9. Emit shot, recoil, animation and audio events.

## Reload

| Condition | Result |
|---|---|
| Magazine full | Reject |
| No reserve ammo | Reject |
| Firing / switching / disabled | Reject or queue (explicit design choice) |
| Accepted | Enter `Reloading`; movement reduced via `CombatMovementModifier` |
| Completed | Transfer legal amount reserve to magazine |
| Interrupted | Preserve or cancel per explicit rule |

## Recoil and spread

- Recoil is a local camera impulse for feel; the authority uses only the legal submitted direction.
- Spread is deterministic per accepted shot sequence.
- Visual recoil recovery is independent of hit resolution.
- The client never submits a chosen hit target or damage value.

## Wire contract

| Direction | Payload |
|---|---|
| Client to authority | `FireCommand(tick, weaponId, origin, direction, shotSequence)`, `ReloadCommand(tick, weaponId)` |
| Authority to clients | `WeaponSnapshot(ammo, actionState, nextFireTick)`, `ShotAccepted`, `ShotRejected`, `ProjectileSpawned`, `HitConfirmed` |
