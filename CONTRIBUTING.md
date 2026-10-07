# Contributing

Thanks for helping. Ground rules:

1. **Original work only.** Do not submit code, assets, tuning values, protocol details or decompiled material from any commercial game.
2. **Authority first.** Any gameplay change must say what the client owns, what the authority owns, and what crosses the wire.
3. **Docs and code move together.** If you change a contract in `Assets/Scripts/Core`, update the matching doc.
4. **Small PRs.** One system per pull request, with a short description of the failure case it handles.

## Workflow

- Fork, branch (`feature/<system>`), open a PR against `main`.
- Use the issue templates for bugs and proposals.
- C# style: PascalCase types/methods, `_camelCase` private fields, no logic in `MonoBehaviour` that can live in plain classes (testable without Unity).
