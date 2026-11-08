# Configurare

## Fișierul de configurare

Configurarea se salvează într-un fișier XML cu același nume ca executabilul plus extensia `.xml`, plasat în același director:

```
GestiunePrezente.exe
GestiunePrezente.exe.xml   ← configurare
```

Dacă fișierul nu există, este creat automat cu valorile implicite la primul acces.

### Structura fișierului

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <add key="Server"                value="192.168.1.233" />
  <add key="DataBase"              value="Onixteia" />
  <add key="User"                  value="" />
  <add key="Pass"                  value="" />
  <add key="UseIntegratedSecurity" value="1" />
  <add key="SocietateDefault"      value="Onixteia" />
  <add key="DiviziuneDefault"      value="Sibiu" />
  <add key="Utilizator"            value="" />
</configuration>
```

### Chei disponibile

| Cheie | Valoare implicită | Descriere |
|-------|------------------|-----------|
| `Server` | `192.168.1.233` | IP sau hostname SQL Server |
| `DataBase` | `Onixteia` | Baza de date implicită |
| `User` | — | Utilizator SQL (dacă nu se folosește Windows Auth) |
| `Pass` | — | Parolă SQL |
| `UseIntegratedSecurity` | `1` | Autentificare Windows integrată |
| `SocietateDefault` | `Onixteia` | Societatea preselectată în formularul de login |
| `DiviziuneDefault` | `Sibiu` | Diviziunea preselectată în formularul de login |
| `Utilizator` | — | Utilizatorul preselectat în formularul de login |

---

## API configurare

```csharp
// Citire
string server = AppConfig.Get("Server");

// Scriere (creează cheia dacă nu există)
AppConfig.Set("Server", "192.168.1.100");
```

---

## Note de securitate

> ⚠ Parola SQL (`Pass`) este stocată **în clar** în fișierul XML. Pe medii de producție se recomandă:
> - Folosirea autentificării integrate Windows (`UseIntegratedSecurity=1`) — nu necesită parolă stocată
> - Restricționarea accesului la fișier prin ACL-uri NTFS dacă autentificarea SQL este obligatorie
