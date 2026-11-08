# Arhitectură

## Proiecte

Soluția conține două proiecte:

**`GestiunePrezente`** — proiectul principal (`WinExe`, `net10.0-windows`, `UseWindowsForms`). Conține toată logica de business, formularele și configurarea.

**`ControalePersonalizate`** — librărie cu controale UI reutilizabile, referită din proiectul principal via `ProjectReference`.

---

## Structura folderelor

```
GestiunePrezente/
├── AppConfig.cs                      ← citire/scriere configurație XML
├── StringTable.resx                  ← resurse text localizate (română)
├── Common/
│   ├── CInterogareSQL.cs             ← wrapper SQL cu parametri și tranzacții
│   ├── CManagerDeProiect.cs          ← sesiune: user, conexiune, roluri, societate
│   ├── CFormatareDate.cs             ← formatare date, string SQL, mesaje
│   ├── CGeneratorNumarIntern.cs      ← secvență număr intern atomic
│   ├── CConversieNumereInLitere.cs
│   └── FrmAjutor*.cs / FrmErr.cs    ← formulare dialog ajutor/eroare
├── MeniuPrincipal/
│   ├── Program.cs                    ← entry point [STAThread]
│   ├── FrmLogin.cs                   ← autentificare + validare HDD
│   └── FrmMeniuPrincipal.cs          ← fereastra principală, navigare sidebar
├── MeniuCursanti/                    ← modulul principal de business
│   ├── FrmGestiuneCursant.*
│   ├── FrmGestiuneContract.*
│   ├── FrmGestiuneAbonament.*
│   ├── FrmGestiunePrezenta.*
│   ├── FrmGestiuneFormator.*
│   ├── FrmGestiuneIncasariAbonament.*
│   ├── FrmModificaNumarPrezenteRetroactiv.*
│   └── FrmCautare*.cs               ← formulare de căutare/listare
├── MeniuConfigurare/
│   ├── FrmGestiuneSocietate.*
│   └── FrmGestiuneDiviziune.*
├── MeniuAdministrare/
│   └── FrmGestiuneRegistruNumereInterne.*
└── Properties/ Resources/

ControalePersonalizate/
├── CustomGrid.cs                     ← DataGridView extins cu SQL
└── CustomCombo.cs                    ← ComboBox cu populare SQL
```

---

## Componente Common

### CInterogareSQL

Wrapper peste `SqlConnection` care expune un API consistent pentru interogări SQL. Suportă tranzacții, parametri, citire rând-cu-rând și încărcare `DataTable`.

| Metodă | Scop |
|--------|------|
| `ExecuteNonQueryP(sql, params)` | INSERT / UPDATE / DELETE parametrizat |
| `ExecuteToIntP / ToStringP / ToDecimalP` | Scalar parametrizat, tipat |
| `OpenP / LoadTableP` | SELECT parametrizat — reader / DataTable |
| `NextRow / GetStringValue(pos)` | Iterare reader cu acces pozițional |
| `BeginTransaction / Commit / Rollback` | Tranzacție explicită pe conexiune |
| `ExecuteStatement / ExecuteToString` | Metode legacy fără parametri (**deprecated**) |

> ⚠ **Atenție:** Metodele legacy fără parametri (`Open`, `ExecuteStatement`, `ExecuteToString`) sunt vulnerabile la SQL Injection. Codul nou trebuie să folosească exclusiv variantele `*P` cu parametri tipați.

### CManagerDeProiect

Obiect de sesiune injectat în toate formularele la construcție. Menține starea conexiunii SQL, datele utilizatorului autentificat, rolurile, societatea și diviziunea active.

| Metodă | Tip | Descriere |
|--------|-----|-----------|
| `GetConnection()` | `SqlConnection` | Conexiunea SQL activă (aruncă dacă deconectat) |
| `GetUserId()` | `int` | ID utilizator autentificat |
| `GetUserNumePrenume()` | `string` | Nume și prenume afișat în titlu |
| `GetSocietate() / GetSocietateNume()` | `int / string` | Societatea curentă |
| `GetDiviziune() / GetCodDiviziune()` | `int / string` | Diviziunea curentă |
| `GetRolUser_Administrator/DirectorGeneral/Secretariat` | `int` | 1 dacă utilizatorul are rolul, 0 altfel |
| `IsValid()` | `bool` | True dacă autentificarea a reușit (`userId > 0`) |

### CFormatareDate

Utilitar static pentru formatare. Metodele de conversie spre SQL produc sintaxa `CONVERT(datetime, '...', 102)` pentru a evita ambiguitatea regională. Toate mesajele UI trec prin `GetString(id)` care citește din `StringTable.resx`.

### CGeneratorNumarIntern

Generator atomic de numere interne consecutive. Folosește `UPDATE ... OUTPUT INSERTED` cu `ROWLOCK, HOLDLOCK` pentru a garanta unicitate fără goluri la rollback:

```sql
UPDATE AD_SecventaNumarIntern WITH (ROWLOCK, HOLDLOCK)
   SET ADSN_UltimulNr = ADSN_UltimulNr + 1
OUTPUT INSERTED.ADSN_UltimulNr
 WHERE ADSN_Id = 1;
```

Se apelează **după** `BeginTransaction()` și **înainte** de INSERT. La rollback, contorul revine automat.

---

## Controale personalizate

### CustomGrid

Extinde `DataGridView` cu încărcare directă din SQL. Expune evenimentul `SelChange` pentru notificarea formularului părinte la schimbarea selecției.

```csharp
CustomGrid.SetConnection(conn);   // static, o singură dată per fereastră
grid.SetSql("SELECT ...");        // populare
```

### CustomCombo

ComboBox cu populare din SQL prin `SetSql(query)`. Conexiunea se injectează static, la fel ca la `CustomGrid`.

> Conexiunea SQL pentru aceste controale este stocată în câmpuri statice, deci partajată la nivel de proces. Apelați `SetConnection()` la deschiderea fiecărei ferestre care le folosește.
