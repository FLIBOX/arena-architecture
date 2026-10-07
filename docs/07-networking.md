# 07 · Networking

Server-authoritative: clients submit commands, the authority simulates and publishes snapshots and events.

## Replication split

| Kind | Contents |
|---|---|
| Commands | Movement, aim, interaction, fire, reload, vehicle input |
| Snapshots | Players, vehicles, zone, match phase, loot containers |
| Reliable events | Spawn, elimination, pickup result, inventory transaction |
| Client-only | Camera, UI animation, cosmetic effects |

## Core types

`ITransport`, `NetworkSession`, `CommandBuffer`, `SnapshotReplicator`, `NetworkObjectRegistry`.

## Approach

1. Build a fixed-tick simulation with command IDs, snapshots and event contracts.
2. Run everything through a local loopback transport first and test replication there.
3. Only then choose a supported production transport.

## Anti-desync checklist

- Sequence numbers and bounded command age.
- Validate speed, acceleration and aim change per tick.
- Authoritative collision and hitbox history.
- Reject implausible origins or stale view state.
- Never accept client-supplied damage, hit target or ammo counts.
