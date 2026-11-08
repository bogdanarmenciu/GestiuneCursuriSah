# Module funcționale

Fiecare modul urmează un tipar consistent: un formular de **căutare/listare** (`FrmCautare*`) și un formular de **editare** (`FrmGestiune*`), ambele primind `CManagerDeProiect` prin constructor.

---

## Modulul Cursanți (`MeniuCursanti`)

Cel mai amplu modul al aplicației. Gestionează întregul ciclu de viață al unui cursant.

### Cursanți — `FrmGestiuneCursant` / `FrmCautareCursanti`

Fișă completă cursant:
- Date personale: nume, prenume, CNP, dată naștere, localitate și județ naștere
- Act identitate: serie CI, număr CI
- Adresă: stradă, număr, localitate, județ
- Export fișă PDF via QuestPDF

### Contracte — `FrmGestiuneContract` / `FrmCautareContracte`

Contracte încheiate cu cursanții. Generare document contract PDF.

### Abonamente — `FrmGestiuneAbonament` / `FrmCautareAbonamente`

Abonamente active sau expirate per cursant, cu număr de ședințe incluse.

### Prezențe — `FrmGestiunePrezenta` / `FrmCautarePrezente`

Pontaj prezențe cu suport pentru **barcode scanner**:

- Formularul interceptează tastatura global (`KeyPreview = true`)
- Un cod de bare este recunoscut ca un șir de caractere tastat rapid (interval < 150ms între taste)
- Buffer-ul se resetează după timeout, diferențiind scanarea de tastare manuală
- Formatorul este selectabil dintr-un `CustomCombo` populat din `CU_Formatori`

```csharp
private string _barcodeBuffer = "";
private DateTime _lastBarcodeKeyTime = DateTime.MinValue;
private readonly TimeSpan _barcodeTimeout = TimeSpan.FromMilliseconds(150);
```

### Modificare retroactivă prezențe — `FrmModificaNumarPrezenteRetroactiv`

Permite corecții administrative ale numărului de prezențe pentru perioade trecute. Acces restricționat prin rol.

### Formatori — `FrmGestiuneFormator` / `FrmCautareFormatori`

Gestionarea instructorilor/formatorilor atribuiți cursurilor.

### Încasări abonamente — `FrmGestiuneIncasariAbonament` / `FrmCautareIncasariAbonamente`

Înregistrarea și vizualizarea plăților pentru abonamente.

---

## Modulul Configurare (`MeniuConfigurare`)

### Societăți — `FrmGestiuneSocietate` / `FrmCautareSocietate`

CRUD pentru `MB_Societate`: denumire, baza de date asociată, IP server.

### Diviziuni — `FrmGestiuneDiviziune` / `FrmCautareDiviziuni`

CRUD pentru `MB_Diviziune`: cod scurt (ex. `SB`), denumire, societate părinte.

---

## Modulul Administrare (`MeniuAdministrare`)

### Registru Numere Interne — `FrmGestiuneRegistruNumereInterne` / `FrmCautareRegistruNumereInterne`

Vizualizare și gestionare a secvenței de numere interne din `AD_SecventaNumarIntern`. Numerele se generează atomic cu `CGeneratorNumarIntern.Next()` în cadrul tranzacției de salvare — fără goluri la rollback.

---

## Formulare ajutor (Common)

Formulare dialog reutilizate în mai multe module:

| Formular | Scop |
|----------|------|
| `FrmAjutorCursanti` | Selector cursant (popup căutare) |
| `FrmAjutorCursantiPrezenta` | Selector cursant în contextul prezenței |
| `FrmAjutorJudetLocalitate` | Selector județ și localitate |
| `FrmErr` | Afișare erori SQL (tip, query, mesaj) |
