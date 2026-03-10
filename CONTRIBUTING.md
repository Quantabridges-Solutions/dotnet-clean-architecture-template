# Contributing to .NET Clean Architecture Template

Thank you for considering contributing. This project is maintained by [Qbs-Tech](https://qbs-tech.com) as a public showcase and starter for the community.

## How to contribute

- **Bug reports and feature ideas** — Open a [GitHub issue](https://github.com/Quantabridges-Solutions/dotnet-clean-architecture-template/issues). Describe the problem or idea and, for bugs, steps to reproduce.
- **Code changes** — Open a pull request from a branch (e.g. `fix/issue-description` or `feature/your-feature`). We’ll review and merge when it fits the template’s goals.

## Development setup

1. Clone the repo and open the solution.
2. Start dependencies: `docker compose up -d postgres redis`
3. Apply migrations: `dotnet ef database update --project src/Infrastructure --startup-project src/Api`
4. Run the API: `dotnet run --project src/Api`
5. Run tests: `dotnet test tests/Tests.csproj`

## Code and PR guidelines

- Follow the existing style (see [.editorconfig](.editorconfig)).
- Keep the solution building and tests passing.
- For new features, add or update tests where it makes sense.
- Keep PRs focused; prefer several small PRs over one large one.
- In the PR description, explain what changed and why.

## Security

For security-sensitive issues, see [SECURITY.md](SECURITY.md) instead of opening a public issue.

Thanks for contributing.
