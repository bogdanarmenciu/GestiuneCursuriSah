# Baza de Date

## Server și baze de date

- **Motor:** SQL Server
- **Server implicit:** `192.168.1.233`, port `1433` (TCP)
- **Producție:** `Onixteia`
- **Test:** `OnixteiaTest`

Dacă denumirea bazei de date conține `test` sau `Test`, titlul ferestrei principale afișează prefixul `TEST---` pentru a evita confuzia cu producția.

---

## Connection String

```
Data Source=tcp:<server>,1433
;Initial Catalog=<db>
;User ID=<user>
;Password=<pass>
;Connect Timeout=30
;Encrypt=False
;TrustServerCertificate=True
;MultipleActiveResultSets=True
```

Serverul implicit poate fi suprascris prin cheia `Server` din [fișierul de configurare](configurare.md).

---

## Tabele principale

Convenție de denumire: prefix pe 4 litere (ex. `MBUT`) urmat de `_` și PascalCase pentru toate coloanele. Cheile primare urmează schema `[PREFIX]_Id`.

| Tabel | Prefix coloane | Scop |
|-------|---------------|------|
| `MB_Utilizatori` | `MBUT_` | Conturi utilizatori, login, parolă, stare activă |
| `MB_Societate` | `MBSO_` | Societăți (companii), server DB, IP |
| `MB_Diviziune` | `MBDV_` | Diviziuni per societate (ex: Sibiu) |
| `MB_RoluriUtilizatori` | `MBRU_` | Roluri per utilizator per diviziune |
| `MB_UtilizatoriIstoricConectare` | `MBUT_` | Audit log conectări: PC, dată, versiune EXE |
| `CU_Formatori` | `CUFO_` | Formatori/instructori |
| `CU_Cursanti` | `CUCU_` | Cursanți înregistrați |
| `CU_Contracte` | — | Contracte cursanți |
| `CU_Abonamente` | `CUAC_` | Abonamente active/expirate |
| `CU_Prezente` | `CUPR_` | Prezențe cursanți la cursuri |
| `CU_IncasariAbonamente` | — | Plăți abonamente |
| `AD_SecventaNumarIntern` | `ADSN_` | Contor global numere interne (1 rând) |

---

## Audit conectări

La fiecare autentificare reușită (în producție), aplicația inserează un rând în `MB_UtilizatoriIstoricConectare` cu:

- data conectării
- ID utilizator și diviziune
- numele PC-ului (`Environment.MachineName`)
- data ultimei modificări a executabilului (pentru detectarea versiunilor expirate)

La deconectare, câmpul `MBUT_DataDeconectarii` se completează. Dacă aplicația se închide brutal, rândul rămâne deschis și este corectat la următoarea conectare de pe același PC.
