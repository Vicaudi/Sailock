# Contributing to Sailock

Thank you for your interest in contributing! Sailock is a small open-source project and every contribution — bug reports, feature ideas, code, or documentation — is genuinely appreciated.

---

## Getting Started

### Requirements
- Windows 10 or 11
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022 (recommended) or any editor that supports WPF / C#

### Setting up the project
1. Fork this repository and clone your fork:
   ```
   git clone https://github.com/YOUR_USERNAME/sailock.git
   ```
2. Open `Sailock.sln` in Visual Studio 2022.
3. Restore NuGet packages (Visual Studio does this automatically on first build).
4. Press **F5** to build and run.

---

## How to Contribute

### Reporting a Bug
Open a [Bug Report issue](.github/ISSUE_TEMPLATE/bug_report.md) and fill in all the requested fields. The more detail you provide, the faster it can be diagnosed.

### Suggesting a Feature
Open a [Feature Request issue](.github/ISSUE_TEMPLATE/feature_request.md). Explain the problem you are trying to solve, not just the solution you have in mind — this helps us find the best approach together.

### Submitting a Pull Request
1. Find an open issue to work on, or open one first to discuss your idea before writing code.
2. Create a branch from `main` with a descriptive name:
   ```
   git checkout -b feature/url-field-in-entries
   git checkout -b fix/clipboard-not-cleared
   ```
3. Make your changes. See the code style notes below.
4. Test your changes manually on Windows before submitting.
5. Open a Pull Request using the provided template. Link it to the relevant issue with `Closes #<number>`.

---

## Code Style
Sailock follows standard C# conventions. Please keep these in mind:
- Use `PascalCase` for public members, `_camelCase` for private fields.
- Properties in ViewModels should use `SetProperty()` for change notification — do not raise `PropertyChanged` manually.
- All user-visible strings must be added to **both** `Localization/Strings.en.xaml` and `Localization/Strings.es.xaml`. Do not hardcode strings in XAML or C# code.
- New settings that need to persist must be added to `AppSettings` in `Models/AppData.cs` and loaded in `SettingsViewModel`'s constructor.
- For security-sensitive code (encryption, random generation), always use `System.Security.Cryptography` — never `System.Random`.
- Keep XAML styles in `App.xaml` to maintain visual consistency. Avoid inline styles when a reusable style already exists.

---

## Project Structure (quick reference)
| Folder | Purpose |
|---|---|
| `Models/` | Plain data classes (`PasswordEntry`, `AppData`, `AppSettings`) |
| `Services/` | Business logic (crypto, storage, TOTP, auto-lock, theme, localization) |
| `ViewModels/` | MVVM ViewModels — no direct UI references |
| `Views/` | XAML views and their code-behind (minimal logic) |
| `Helpers/` | Converters, `RelayCommand`, `ViewModelBase` |
| `Localization/` | String resource files for EN and ES |
| `Resources/` | Icons and logo assets |

---

## Issues Labeled `good first issue`
If you are new to the project, look for issues labeled [`good first issue`](../../labels/good%20first%20issue). 
These are well-scoped tasks with clear requirements and limited surface area.

---

## Questions
If you are unsure about something before starting, open a Discussion or leave a comment on the relevant issue. 
It is always better to align first than to build something in the wrong direction.
