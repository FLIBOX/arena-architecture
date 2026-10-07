# 04 · Hitboxes

Visual meshes are never the source of combat truth.

| Layer | Responsibility |
|---|---|
| Visual rig | Animator-driven mesh and bones, cosmetic only |
| `HitboxRig` | Maps stable `HitRegion` values to colliders/primitives |
| `HitboxHistory` | Records transforms per simulation tick for rewind |
| Physics query layer | Ray/capsule/projectile queries on hitbox layers |
| Damage resolver | Turns `HitRegion` into a modifier from your tuning |

## Rules

- Child colliders on a dedicated gameplay-hitbox layer.
- Never resolve combat from render-mesh triangles.
- One collider maps to exactly one `HitRegion`.
- On shot validation, query the target's historical pose for the accepted command tick.
- Visual interpolation is independent of authoritative hitbox simulation.

## Relationship to aim assist

Aim acquisition may choose a preferred point from the rig but must use only visible points, never bypass world obstruction, never decide hit success, and never force a hit after the authority query.
