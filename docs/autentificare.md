# Autentificare și Roluri

## Flux de autentificare

```
Argumente CLI         →  Validare conexiune SQL  →  Verificare user
(soc/div/user/pass)      (ConnectValidator)          (MB_Utilizatori)
        ↓
Verificare diviziune  →  Audit conectare         →  FrmMeniuPrincipal
(MB_Diviziune)           (MB_UtilizatoriIstoricConectare)
```

Pașii în detaliu:

1. **Construire connection string** din configurare + câmpurile din formular
2. **Validare conexiune** — `ConnectValidator.TryToConnect()` deschide și închide imediat o conexiune de test; returnează eroarea SQL reală dacă eșuează
3. **Verificare credențiale** — căutare în `MB_Utilizatori` după `MBUT_Login` și `MBUT_Pass` (parolă plain-text în DB)
4. **Verificare apartenență la diviziune** — utilizatorul trebuie asociat cu diviziunea selectată
5. **Verificare unicitate sesiune** — se detectează dacă există deja o sesiune deschisă de același utilizator pe același PC în aceeași zi
6. **Înregistrare audit** — INSERT în `MB_UtilizatoriIstoricConectare`
7. **Verificare serial HDD** — doar pentru utilizatorii non-Director (vezi mai jos)

---

## Roluri utilizator

Rolurile sunt stocate în `MB_RoluriUtilizatori` per utilizator per diviziune.

| Rol | Flag DB | Privilegii |
|-----|---------|-----------|
| Administrator | `MBRU_Administrator` | Acces complet la toate modulele și setările |
| Director General | `MBRU_FlagDirector` | Ocolire verificare serial HDD la login |
| Secretariat | `MBRU_FlagSecretariat` | Operațiuni curente: cursanți, prezențe, abonamente |

Un utilizator poate cumula mai multe roluri pe aceeași diviziune.

---

## Verificare hardware (licențiere)

La login, pentru utilizatorii care **nu** au rolul Director General, aplicația citește serialul HDD-ului intern via WMI:

```csharp
// Win32_DiskDrive — modele HDD-uri interne (fără USB)
ManagementObjectSearcher searcher = new("SELECT * FROM Win32_DiskDrive WHERE InterfaceType<>'USB'");

// Win32_PhysicalMedia — seriale
searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
```

Necesită pachetul `System.Management` (10.0.2). Mecanismul de blocare pe baza serialului există în cod, dar logica de refuz a accesului este **comentată** în versiunea curentă.

---

## Lansare cu argumente CLI

Aplicația acceptă parametri în linia de comandă pentru login automat (integrare cu alte sisteme):

```
# Login simplu
GestiunePrezente.exe <societate> <diviziune> <user> <parola>

# Login + deschidere document direct
GestiunePrezente.exe <societate> <diviziune> <user> <parola> <tipDocument> <documentId>
```

Exemplu: `GestiunePrezente.exe Onixteia Sibiu admin parola`

Dacă argumentele lipsesc, se folosesc valorile din [configurare](configurare.md) și formularul de login.

---

## Timer de inactivitate

`CManagerDeProiect` include un timer static. La expirare, aplicația se închide și se repornește automat:

```csharp
// La expirare timer
Application.Exit();
System.Diagnostics.Process.Start(Application.ExecutablePath);
```

Intervalul se citește din `TimpInactivitate` (minute). Dacă valoarea este `0`, timer-ul nu pornește.
