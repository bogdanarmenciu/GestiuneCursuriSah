# GestiunePrezente — Documentație Tehnică

Aplicație desktop Windows Forms (.NET 10) pentru gestiunea cursanților, contractelor, abonamentelor, prezenței și încasărilor — utilizată intern de Onixteia.

## Cuprins

| Fișier | Conținut |
|--------|----------|
| [arhitectura.md](arhitectura.md) | Structura soluției, module, componente Common |
| [baza-de-date.md](baza-de-date.md) | Tabele, convenții, connection string |
| [autentificare.md](autentificare.md) | Login, roluri utilizatori, verificare HDD |
| [configurare.md](configurare.md) | Fișier de configurare XML |
| [module.md](module.md) | Module funcționale: Cursanți, Configurare, Administrare |
| [dependente.md](dependente.md) | Pachete NuGet, licențe, note de implementare |

## Date rapide

| | |
|-|--|
| **Target Framework** | `net10.0-windows` |
| **UI** | Windows Forms |
| **Baza de date** | SQL Server (Microsoft.Data.SqlClient 6.1.4) |
| **Output** | `WinExe` |
| **Soluție** | `GestiunePrezente.slnx` |
