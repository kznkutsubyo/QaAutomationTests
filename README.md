# QA Automation Tests (.NET / Playwright / HttpClient)

A lightweight, maintainable automated testing project built with **C#** and **.NET 8 (LTS)**.  
This repository demonstrates end-to-end UI testing using **Microsoft Playwright** (following the Page Object Model pattern) and REST API validation using **HttpClient** and **System.Text.Json**.

---

## Tech Stack

- **Language & Runtime:** C# 12 / .NET 8.0 (LTS)
- **Test Framework:** xUnit
- **UI Automation:** Microsoft Playwright for .NET (Chromium)
- **API Testing:** `System.Net.Http.HttpClient`
- **JSON Serialization:** `System.Text.Json`
- **CI/CD:** GitHub Actions

---

## Project Structure

```text
QaAutomationTests/
├── .github/
│   └── workflows/
│       └── tests.yml              # GitHub Actions CI workflow
├── Models/
│   └── Post.cs                    # Data model for REST API deserialization
├── Pages/
│   ├── LoginPage.cs               # Page Object for the SauceDemo Login page
│   └── ProductsPage.cs            # Page Object for the SauceDemo Products page
├── Tests/
│   ├── LoginTests.cs              # UI tests for authentication scenarios
│   └── ApiTests.cs                # REST API functional test
├── .gitignore
├── QaAutomationTests.csproj
└── README.md
```

---

## Implemented Test Scenarios

### 1. Successful Login (UI)
- **Target:** `https://www.saucedemo.com/`
- **Scenario:**
  1. Navigate to the login page.
  2. Enter valid username (`standard_user`) and password (`secret_sauce`).
  3. Click the **Login** button.
  4. Verify redirection to the Products page (`/inventory.html`).
  5. Assert that the page title is `"Products"` and the inventory list is visible.

### 2. Invalid Login (UI)
- **Target:** `https://www.saucedemo.com/`
- **Scenario:**
  1. Navigate to the login page.
  2. Enter invalid credentials (`invalid_user` / `wrong_password`).
  3. Click the **Login** button.
  4. Verify that the error banner is displayed with the expected message (`"Username and password do not match any user in this service"`).
  5. Assert that the user remains on the login page.

### 3. REST API Validation (API)
- **Target:** `GET https://jsonplaceholder.typicode.com/posts/1`
- **Scenario:**
  1. Send an asynchronous HTTP GET request.
  2. Assert HTTP status code `200 OK`.
  3. Verify that the response `Content-Type` header is `application/json`.
  4. Deserialize the JSON payload into the `Post` model.
  5. Assert that `id` equals `1` and both `title` and `body` are non-empty strings.

---

## Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PowerShell (built-in on Windows 11, or PowerShell 7+ on macOS/Linux)

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/kznkutsubyo/QaAutomationTests.git
   cd QaAutomationTests
   ```

2. **Build the project:**
   ```bash
   dotnet build
   ```

3. **Install the Chromium browser for Playwright:**
   - **Windows (Windows PowerShell):**
     ```powershell
     powershell -NoProfile -ExecutionPolicy Bypass -File bin\Debug\net8.0\playwright.ps1 install chromium
     ```
   - **PowerShell 7 (`pwsh`) on Windows / macOS / Linux:**
     ```bash
     pwsh bin/Debug/net8.0/playwright.ps1 install chromium
     ```

---

## Running the Tests

Run all tests (UI + API):
```bash
dotnet test --logger "console;verbosity=normal"
```

Run only UI tests:
```bash
dotnet test --filter "FullyQualifiedName~LoginTests"
```

Run only API tests:
```bash
dotnet test --filter "FullyQualifiedName~ApiTests"
```

---

## Continuous Integration (GitHub Actions)

Tests are automatically executed on every `push` and `pull_request` to `main`/`master` via [`.github/workflows/tests.yml`](.github/workflows/tests.yml). You can also trigger a test run manually from the **Actions** tab in GitHub.
