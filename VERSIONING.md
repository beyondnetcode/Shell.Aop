# Versioning Strategy

BeyondNet.Aop uses **Semantic Versioning (SemVer)** for clear, predictable version management.

## Version Format

```
{major}.{minor}.{patch}[-{prerelease}]
```

| Component | Description | When to Increment |
|-----------|-------------|-------------------|
| **major** | Breaking changes | Public API changes that break backward compatibility |
| **minor** | New features | New functionality added in a backward-compatible manner |
| **patch** | Bug fixes | Backward-compatible bug fixes |
| **prerelease** | Pre-release | Alpha/beta versions for testing (e.g., `beta`, `rc1`) |

### Examples

| Version | Meaning |
|---------|---------|
| `1.0.0` | First stable release |
| `1.1.0` | New features, backward compatible |
| `1.1.1` | Bug fix, backward compatible |
| `2.0.0` | Breaking changes |
| `2.0.0-beta` | Pre-release for testing |
| `2.0.0-rc.1` | Release candidate |

## Version Assignment

### Tags (Official Releases)
- Tags on `main` branch trigger official releases
- Tag format: `v{version}` (e.g., `v1.0.0`, `v2.1.0-beta`)
- Only tags on `main` publish to NuGet

### Automated Versioning
- `develop` branch builds get pre-release version: `0.0.0-develop+{hash}`
- Feature branch builds get: `0.0.0-{branch}+{hash}`
- These are for CI/CD only and not published to NuGet

## Release Cadence

| Type | Frequency | Target |
|------|-----------|--------|
| **Major** | As needed | Breaking changes |
| **Minor** | Monthly | New features |
| **Patch** | As needed | Critical fixes |

## Package Versioning

Each NuGet package is versioned independently:

| Package | Version | Notes |
|---------|---------|-------|
| `BeyondNetCode.Shell.Aop` | 1.0.0 | Core library |
| `BeyondNetCode.Shell.Aop.Aspects` | 1.0.0 | Pre-built aspects |
| `BeyondNetCode.Shell.Aop.Aspects.Logger` | 1.0.0 | Common.Logging integration |
| `BeyondNetCode.Shell.Aop.Aspects.Logger.Serilog` | 1.0.0 | Serilog integration |
| `BeyondNetCode.Shell.Aop.DispatchProxy` | 1.0.0 | Native proxy implementation |
| `BeyondNetCode.Shell.Aop.DI` | 1.0.0 | DI extension methods |

## Deprecation Policy

- Old package versions remain available on NuGet
- Deprecation notice given 6 months before removal
- Migration guides provided for major version changes

## Version Compatibility

| Package | Target Framework | Notes |
|---------|-----------------|-------|
| All packages | `net10.0` | Current focus |

## CI/CD Versioning

The GitHub Actions pipeline handles versioning automatically:

```yaml
# On tag push to main:
# 1. Extract version from tag
# 2. Build all projects
# 3. Run tests
# 4. Pack with version
# 5. Publish to NuGet
# 6. Create GitHub Release

# On push to develop:
# 1. Build all projects
# 2. Run tests
# 3. Pack with auto-generated pre-release version
# 4. Artifacts available for testing
```

## Migration Guides

When a major version is released, a migration guide will be provided in the release notes.

For version 2.0.0 migration from 1.x, see the release notes.