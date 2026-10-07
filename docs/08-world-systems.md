# 08 · World systems

| System | Design |
|---|---|
| Player state | `PlayerEntity` owns identity, team, movement, combat, inventory, lifecycle; components talk via typed domain events |
| Match | One authority-owned `MatchDirector` advances explicit phases: waiting, warmup, active, resolving, results |
| Safe zone | `ZoneDirector` publishes center/radius snapshots from the match clock, an authored curve and playable bounds; emits out-of-zone damage events |
| Spawning | `SpawnDirector` selects valid `SpawnPoint`s for `SpawnRequest`s |
| Inventory | Stable item IDs and stack rules (`ItemDefinition`, `ItemStack`, `InventoryComponent`); equipment is a constrained view over slots; pickups resolve only on authority |
| Vehicles | `VehicleEntity` owns simulation, health, seats, authority transfer (`VehicleController`, `VehicleSeat`, `VehicleInputCommand`, `VehicleSnapshot`) |
| Camera | Local-only `CameraRig` produces an aim ray; gameplay receives quantized aim input |
| UI | Presenters (`HudPresenter`, `InventoryPresenter`, `MatchPresenter`, `SettingsPresenter`) subscribe to read-only view models |
| Audio | Typed `GameEvent`/`AudioEvent` stream; mixer groups for ambience, SFX, UI, voice, music |
| Voice | Optional `IVoiceTransport`, disabled or mocked until a lawful backend, explicit consent and moderation exist |
| Persistence | `SettingsRepository`, `LocalSaveRepository`, `GameConfig` with versioned migrations; local store holds only non-sensitive settings; remote match config validated on authority |

Dependencies for vehicles: input, physics, player controller, health, networking, audio.
