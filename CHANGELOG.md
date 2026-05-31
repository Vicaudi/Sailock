# Changelog — Sailock

All notable changes to Sailock are documented here.

---

## [1.1.3] - 2026-05-30

### Added
* Added two-factor authentication (2FA) for increased login security
* Added confirmation dialog before disabling 2FA
* Master password confirmation is now required before editing any saved entry
* Added show/hide toggle for passwords in the edit window
* Added interface scale options (Small, Default, Large)
* Added light theme alongside the existing dark theme
* Added configurable visual contrast for improved readability
* Simplified the Add New password window for a faster workflow
* Edit window now shows only the relevant fields based on what has changed

### Changed
* Improved the overall password editing experience
* Optimized element rendering throughout the application

### Fixed
* Fixed an issue where the Add New window would not close correctly
* Fixed logo duplication in the sidebar
* Fixed the TOTP verification field not rendering centered
* Removed an unwanted visual line at the top of the window
* Improved logo loading reliability across all views

---

## [1.1.2] - 2026-05-28

### Added
* Added the ability to import data from a backup file
* Added the ability to export data securely as a .slock file
* Added option to permanently delete all application data
* Added auto-lock after a period of user inactivity
* Added status messages after import and export operations

### Fixed
* Fixed an issue that prevented imported data from loading correctly
* Fixed an error that occurred during export in certain cases

---

## [1.1.1] - 2026-05-27

### Added
* New Settings screen with all main options:
  * Security (2FA, master password change, auto-lock)
  * Appearance (light/dark theme, contrast, text size)
  * Data management (import and export)
  * Danger zone (full data wipe with confirmation)
* Added light/dark theme switching
* Added interface scale support
* Prepared the theming system for future visual improvements

### Changed
* Improved data persistence stability
* Login now validates against the user's real master password
* First launch now guides the user through creating their master password

### Fixed
* Fixed stability issues when saving data
* Fixed navigation errors between views

---

## [1.0.2] - 2026-05-25

### Added
* Main dashboard showing all saved password entries
* Add New window for creating entries
* Edit window for modifying and deleting existing entries
* Password generator with configurable options
* Sidebar navigation between Dashboard, Generator and Settings
* Custom window controls (minimize, maximize, close)
* Complete base UI layout
* Version number visible within the application

### Changed
* Improved navigation flow between sections
* General stability improvements

### Fixed
* Fixed the login system
* Fixed unresponsive buttons and UI elements

---

## [1.0.0] - 2026-05-23

### Added
* Login screen
* Initial project structure
* Base layout for all main screens
* Navigation system between views
* Sailock logo and initial visual style
