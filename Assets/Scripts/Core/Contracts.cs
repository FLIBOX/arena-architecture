using UnityEngine;

namespace ArenaArchitecture.Core
{
    // ---- Commands (client -> authority) -------------------------------------
    public struct MoveCommand
    {
        public uint Sequence;
        public uint Tick;
        public Vector2 Move;
        public float Yaw;
        public bool Sprint, Crouch, Jump;
    }

    public struct FireCommand
    {
        public uint Tick;
        public int WeaponId;
        public Vector3 Origin;
        public Vector3 Direction;
        public uint ShotSequence;
    }

    public struct ReloadCommand
    {
        public uint Tick;
        public int WeaponId;
    }

    // ---- Snapshots (authority -> clients) ------------------------------------
    public enum MovementState : byte { Grounded, Crouched, Jumping, Falling }

    public struct PlayerSnapshot
    {
        public uint Tick;
        public int PlayerId;
        public Vector3 Position;
        public Vector3 Velocity;
        public float Yaw;
        public MovementState State;
    }

    public enum WeaponAction : byte { Ready, Firing, Reloading, Switching, Disabled }

    public struct WeaponSnapshot
    {
        public int WeaponId;
        public int Magazine;
        public int Reserve;
        public WeaponAction Action;
        public uint NextFireTick;
    }

    // ---- Damage ---------------------------------------------------------------
    public enum HitRegion : byte { Head, Torso, Limb }
    public enum DamageType : byte { Bullet, Explosion, Zone, Fall, Vehicle }

    public readonly struct DamageRequest
    {
        public readonly int SourceId, TargetId, WeaponId;
        public readonly HitRegion Region;
        public readonly DamageType Type;
        public DamageRequest(int source, int target, int weapon, HitRegion region, DamageType type)
        { SourceId = source; TargetId = target; WeaponId = weapon; Region = region; Type = type; }
    }

    public readonly struct DamageResult
    {
        public readonly float Applied;
        public readonly float HealthAfter;
        public readonly bool Eliminated;
        public DamageResult(float applied, float healthAfter, bool eliminated)
        { Applied = applied; HealthAfter = healthAfter; Eliminated = eliminated; }
    }

    // ---- Events (reliable) ---------------------------------------------------
    public readonly struct PlayerEliminated
    {
        public readonly int VictimId, KillerId, WeaponId;
        public PlayerEliminated(int victim, int killer, int weapon)
        { VictimId = victim; KillerId = killer; WeaponId = weapon; }
    }

    // ---- Seams ----------------------------------------------------------------
    public interface IShotResolver
    {
        bool TryResolve(in FireCommand cmd, uint seed, out DamageRequest request);
    }

    public interface ITransport
    {
        void Send<T>(int peerId, in T message, bool reliable) where T : struct;
        event System.Action<int, object> MessageReceived;
    }

    public interface IWorldPhysics
    {
        bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, out RaycastHit hit);
    }

    public sealed class LaunchContext
    {
        public string OpenedUrl { get; }
        public System.Collections.Generic.IReadOnlyDictionary<string, string> Params { get; }
        public LaunchContext(string url, System.Collections.Generic.IReadOnlyDictionary<string, string> p)
        { OpenedUrl = url; Params = p; }
    }
}
