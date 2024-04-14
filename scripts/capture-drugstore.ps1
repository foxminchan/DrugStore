PowerShell.exe -ExecutionPolicy Bypass -File ./tests/BackOffice.EndToEnd/bin/Debug/net8.0/playwright.ps1 codegen https://localhost:7050
PowerShell.exe -ExecutionPolicy Bypass -File ./tests/StoreFront.EndToEnd/bin/Debug/net8.0/playwright.ps1 codegen https://localhost:7060
