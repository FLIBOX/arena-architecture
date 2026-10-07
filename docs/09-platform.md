# 09 · Platform integration (Android)

Unity games on Android usually talk to the host activity through string-based messaging. Replace that with typed events and adapters.

| Concern | Approach |
|---|---|
| Host bridge | `IPlatformEvents`, `PlatformEventBus`, `AndroidPlatformAdapter` injected into UI/session systems |
| Launch / deep links | Parse into an immutable `LaunchContext`, emit `LaunchContextReceived`; route only allowlisted gameplay-independent commands |
| Device state | `IDeviceInfo`, `IPermissionService`, `IClipboardService`; surface only what UI/settings need |
| Voice / audio capture | Separate gameplay audio (`AudioService`) from optional voice (`IVoiceTransport`) |

Keep platform code out of gameplay assemblies so simulation stays testable without a device.
