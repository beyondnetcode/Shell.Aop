# Contributing to BeyondNet.Aop

Thank you for your interest in contributing to BeyondNet.Aop!

## GitFlow Branching Strategy

We follow a strict GitFlow branching model to ensure quality and traceability.

### Branch Types

| Type | Prefix | Origin | Destination | Purpose |
|------|--------|--------|-------------|---------|
| **Main** | - | - | - | Production-ready code |
| **Develop** | - | - | main | Integration branch for features |
| **Feature** | `feature/` | develop | develop | New functionality |
| **Bugfix** | `bugfix/` | develop | develop | Bug fixes |
| **Hotfix** | `hotfix/` | main | main + develop | Urgent production fixes |
| **Release** | `release/` | develop | main + develop | Release preparation |

### Workflow

#### Feature Development
```bash
git checkout develop
git pull origin develop
git checkout -b feature/add-new-aspect
# ... make changes ...
git push -u origin feature/add-new-aspect
# Create Pull Request to develop
```

#### Release Process
```bash
git checkout develop
git pull origin develop
git checkout -b release/v1.1.0
# ... final adjustments, bump version ...
git checkout main
git merge --no-ff release/v1.1.0
git tag -a v1.1.0 -m "Release v1.1.0"
git push origin main --tags
git checkout develop
git merge --no-ff release/v1.1.0
git branch -d release/v1.1.0
```

#### Hotfix Process
```bash
git checkout main
git pull origin main
git checkout -b hotfix/v1.0.1-fix-urgent
# ... fix ...
git checkout main
git merge --no-ff hotfix/v1.0.1-fix-urgent
git tag -a v1.0.1 -m "Hotfix v1.0.1"
git push origin main --tags
git checkout develop
git merge --no-ff hotfix/v1.0.1-fix-urgent
```

### Commit Convention

We use conventional commits for clear history:

```
<type>(<scope>): <description>

feat(aspects): add retry aspect with exponential backoff
fix(core): correct cache key generation for generic methods
docs(logger): update Serilog integration guide
```

**Types:** `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

**Scopes:** `core`, `aspects`, `logger`, `serilog`, `di`, `tests`, `workflow`

### Pull Request Rules

- All PRs require at least 1 reviewer approval
- All CI checks must pass
- Squash commits before merging
- Use meaningful PR titles and descriptions
- Reference issues in PR description

### Branch Protection

| Branch | Protection Rules |
|--------|-----------------|
| `main` | PR required, 2 reviewers, status checks mandatory |
| `develop` | PR required, 1 reviewer, status checks mandatory |
| `feature/*`, `bugfix/*`, `hotfix/*`, `release/*` | PR suggested, no strict restrictions |

### Coding Standards

- Follow .NET conventions and naming guidelines
- Add XML documentation to public APIs
- Include unit tests for new functionality
- Run `dotnet build` and `dotnet test` before submitting PR
- Ensure code analysis passes with no warnings

### Testing Requirements

- All tests must pass on both Windows and Ubuntu
- Maintain or improve code coverage
- Add integration tests for DI extension methods

## Getting Help

- Open an issue for bugs or feature requests
- Join discussions in GitHub Discussions
- Review existing issues before creating new ones

## License

By contributing, you agree that your contributions will be licensed under the Apache 2.0 License.