# 01 · Movement

## Components

| Component | Responsibility |
|---|---|
| `PlayerInputSource` | Samples input and builds `MoveCommand` |
| `PlayerMotor` | Velocity, gravity, collision, slopes, state transitions |
| `GroundProbe` | Capsule/sphere queries for grounded and slope state |
| `MovementConfig` | ScriptableObject: speeds, acceleration, gravity, slope limit |
| `PlayerAnimatorBridge` | Maps simulated state to animator parameters |
| `MovementReplicator` | Sends commands, applies authoritative snapshots |

**Inputs:** desired move vector, look delta, sprint/crouch/jump intent, ground normal, contacts, combat movement modifiers.
**Outputs:** position, velocity, yaw, grounded flag, stance/state, animation parameters, network snapshot.

## Behavior rules

| Behavior | Requirement |
|---|---|
| Idle | Zero input decelerates horizontal velocity gradually |
| Walk | Project planar input onto the ground plane |
| Sprint | Only if grounded, moving forward enough, not crouched, not combat-restricted |
| Crouch | Shorter collision profile only if clearance check passes |
| Jump | One vertical impulse when accepted |
| Fall | No valid ground after grace time, or vertical velocity < 0 |
| Land | Valid floor found: clear downward velocity, emit event |
| Strafe | Keep local-space lateral input; animator gets signed lateral velocity |
| Acceleration | Move planar velocity toward desired using per-state acceleration |
| Air control | Scale steering acceleration; never overwrite horizontal velocity |
| Slopes | Project onto contact plane; slide or reject beyond walkable threshold |
| Collision | Bounded sweep-and-slide iterations; never teleport through geometry |

## Replication

- Client to authority: `MoveCommand` per simulation tick.
- Authority to clients: snapshot with tick, position, velocity, yaw, stance, state.
- Prediction: local player simulates unacknowledged commands.
- Reconciliation: rewind to the acknowledged state, replay remaining commands.
- Anti-desync: sequence numbers, bounded command age, speed/acceleration validation, authoritative collision.
