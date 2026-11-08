# Dependențe și Note de Implementare

## Pachete NuGet

| Pachet | Versiune | Licență | Utilizare |
|--------|----------|---------|-----------|
| `Microsoft.Data.SqlClient` | 6.1.4 | MIT | Driver ADO.NET pentru SQL Server |
| `QuestPDF` | 2025.12.3 | Community / Commercial | Generare rapoarte PDF fluent |
| `QuestPDF.Barcodes` | 2024.10.3 | Community / Commercial | Coduri de bare în documente PDF |
| `DocumentFormat.OpenXml` | 3.4.1 | MIT | Citire/scriere fișiere `.xlsx`, `.docx` |
| `System.Management` | 10.0.2 | MIT | WMI: informații hardware (serial HDD) |

> ⚠ **QuestPDF** necesită o licență comercială pentru utilizare în producție dacă venitul companiei depășește limita Community. Verificați termenii la fiecare actualizare de versiune majoră.

---

## Resurse text localizate

Toate mesajele UI sunt centralizate în `StringTable.resx`. Accesul se face prin:

```csharp
CFormatareDate.GetString("IDS_...")
// → StringTable.ResourceManager.GetString(id, CultureInfo.CurrentUICulture)
```

Fișierul `StringTable.Designer.cs` este generat automat de `ResXFileCodeGenerator` — nu editați manual.

---

## Fișiere temporare în rădăcina soluției

Fișierele de mai jos **nu sunt incluse în proiecte** și reprezintă scripturi/date de lucru temporare:

- `tempSQL.cs`, `tempSQL2.cs`, `TempSql3.cs`
- `StergeCursantiSQL.cs`
- `date_import.csv`

Pot fi arhivate sau eliminate în siguranță.

---

## Generare resurse

`StringTable.resx` și `Properties/Resources.resx` au generatorul `ResXFileCodeGenerator` configurat. Fișierele `.Designer.cs` corespunzătoare se regenerează automat la build — nu editați manual.

---

## Mediu TEST vs PRODUCȚIE

Dacă denumirea bazei de date conține `test` sau `Test`, titlul ferestrei principale afișează prefixul `TEST---`:

```csharp
if (sDB.Contains("test") || sDB.Contains("Test"))
    this.Text = "TEST---..." + ...;
```

Baza de date de test se numește `OnixteiaTest`.

---

## Comportament în DEBUG vs RELEASE

| Comportament | DEBUG | RELEASE |
|-------------|-------|---------|
| Parolă pre-completată în login | Da (`"Instructor"`) | Nu |
| Insert audit conectare | Comentat (nu se inserează) | Activ |
| Validare versiune EXE | Dezactivată | Activă (parțial comentată) |
