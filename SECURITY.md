# Security Policy

## Supported Versions
Only the latest release of Sailock receives security fixes. Please update to the most recent version before reporting a vulnerability.

| Version | Supported |
|---|---|
| Latest (1.x) | ✅ Yes |
| Older versions | ❌ No |

---

## Reporting a Vulnerability
**Please do not report security vulnerabilities as public GitHub issues.** Doing so could expose users before a fix is available.

To report a security issue, please use one of the following private channels:

- **GitHub Security Advisories:** Go to the [Security tab](../../security/advisories/new) of this repository and open a private advisory.
- **Email:** If you prefer, contact me directly through the email address listed on my GitHub profile.

### What to include in your report
To help us triage and reproduce the issue, please provide:
- A clear description of the vulnerability and its potential impact.
- The affected version(s) of Sailock.
- Step-by-step instructions to reproduce the issue.
- Any proof-of-concept code, files, or screenshots (shared privately).

### What to expect
- You will receive an acknowledgement within **5 business days**.
- We aim to release a fix within **30 days** for critical vulnerabilities.
- We will keep you informed throughout the process and credit you in the release notes if you wish.

---

## Security Design Notes
Understanding how Sailock stores data may help contextualize vulnerability reports:

- All data is stored locally in `%APPDATA%\Sailock\data.slock`.
- The vault file is encrypted with **AES-256-CBC** using a key derived from the master password via **PBKDF2-SHA256** (100,000 iterations, random 128-bit salt per save).
- The master password is **never stored** — it only exists in memory while the session is active.
- TOTP secrets are stored encrypted inside the vault, protected by the same master password.
- Sailock makes **no network connections** of any kind.

If you find a weakness in any of these mechanisms, we want to hear about it.
