# 05 · Damage & elimination

Damage is authority-owned, immutable, attributable and event-driven. UI and audio consume outcomes; they never compute health.

## Resolution order

1. Validate source, target, weapon, shot result.
2. Take weapon base damage.
3. Apply hit-region multiplier.
4. Apply armor/resistance/shield rules.
5. Clamp applied damage.
6. Update authoritative health.
7. Transition lifecycle if health reaches zero.
8. Record attribution.
9. Emit replicated events.
10. UI/audio/VFX react to events.

```
D_applied = max(0, D_base * M_region * M_weapon * M_resistance - A_mitigation)
```

## Lifecycle

| State | Meaning |
|---|---|
| Active | Can move, aim, interact, take damage |
| Downed | Optional mode-defined state with restricted interaction |
| Eliminated | No gameplay input; may spectate |
| Respawning | Server-selected spawn, reset pending |
| Invulnerable | Optional short, explicitly configured spawn protection |

## Kill attribution

Keep an authority-owned combat ledger: last valid damaging source, damage timestamps, direct eliminator, optional assist contribution, environmental/self damage, weapon and hit-region metadata.

## Replication

Clients send no damage commands, only fire/input. Authority sends `DamageApplied`, `HealthChanged`, `PlayerDowned`, `PlayerEliminated`, `KillFeedEvent`. Clients may predict impact feedback only and must correct to authoritative results.
