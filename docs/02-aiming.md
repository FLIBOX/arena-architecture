# 02 · Aiming & aim assist

Aim assist is a **local input-feel layer**. It may dampen or nudge rotation, but the authority validates the final shot ray independently and never trusts assist output.

## Target acquisition pipeline

1. Gather nearby entities from a spatial index.
2. Reject self, eliminated, invalid-team and out-of-range entities.
3. Reject entities outside the view cone.
4. Pick an aim point from the target's hit-region component.
5. Line-of-sight query against world obstruction layers.
6. Score remaining candidates.
7. Keep the current target unless a challenger beats it by a switch threshold.
8. Apply optional slowdown or bounded correction locally.

## Filters

| Filter | Rule |
|---|---|
| Identity | Reject local player and despawned entities |
| Team | Reject allies in hostile-fire modes |
| State | Reject eliminated, invulnerable, hidden, non-targetable |
| Distance | `d <= d_max` |
| Cone | `arccos(f · d) <= theta_max` (f = view forward, d = direction to target) |
| Visibility | Raycast from camera/weapon origin to aim point |
| Occlusion | Reject if first blocking hit is not the target's hitbox |

## Scoring

```
score = w_a * (1 - theta/theta_max)
      + w_d * (1 - d/d_max)
      + w_v * visible
      + w_s * stickiness
      - w_o * obstructionPenalty
```

The score selects a target; it does not guarantee a hit. Weights are your own tuning.

## Correction rules

| Feature | Behavior |
|---|---|
| Slowdown | Reduce look sensitivity inside a cone around the current target |
| Magnetic correction | Rotate toward target by a capped angular delta per tick |
| Smoothing | Critically damped or bounded angular speed, never instant snap |
| Max correction | Ignore candidates beyond a configurable angle |
| Switching | Hysteresis plus a short lock duration |
| Shooting | Final corrected view direction goes into `FireCommand`; authority re-validates |

## Replication

Client owns camera and candidate selection. Authority owns whether a shot intersects a valid target. Clamp per-tick direction changes and reject implausible aim origins or stale view state.
