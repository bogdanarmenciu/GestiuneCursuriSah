-- Reorganizare meniu ONIXTEIA
-- Structura noua: Solicitari | Activitate | Financiar | Situatii | Administrare

DECLARE @idActivitate INT;

-- 1. Modul nou "Activitate" (top-level)
INSERT INTO MB_Meniu (MBME_Nume, MBME_MBME_Id, MBME_Aria, MBME_OrdineAfisare,
    MBME_TipIcoana, MBME_Ascuns, MBME_OrdineAfisareArieInDrepturiUtilizator,
    MBME_DreptUtilizatorDefault, MBME_DreptUtilizatorDefaultFlagDirector,
    MBME_DreptUtilizatorDefaultFlagSecretar, MBME_NumeAria, MBME_MBSO_Id)
VALUES (N'Activitate', 0, '1CU', 20, 0, 0, 1, 1, 1, 1, N'Cursuri', 1);
SET @idActivitate = SCOPE_IDENTITY();

-- 2. Cursanti, Contracte, Antrenori -> sub Activitate
UPDATE MB_Meniu SET MBME_MBME_Id=@idActivitate, MBME_OrdineAfisare=10 WHERE MBME_Id=3; -- Cursanti
UPDATE MB_Meniu SET MBME_MBME_Id=@idActivitate, MBME_OrdineAfisare=20 WHERE MBME_Id=5; -- Contracte
UPDATE MB_Meniu SET MBME_MBME_Id=@idActivitate, MBME_OrdineAfisare=30 WHERE MBME_Id=1; -- Antrenori

-- 3. Prezente si Abonamente direct sub Activitate (scoase din Cursuri/7)
UPDATE MB_Meniu SET MBME_MBME_Id=@idActivitate, MBME_OrdineAfisare=40, MBME_Nume=N'Prezențe'   WHERE MBME_Id=9;
UPDATE MB_Meniu SET MBME_MBME_Id=@idActivitate, MBME_OrdineAfisare=50, MBME_Nume=N'Abonamente' WHERE MBME_Id=10;
UPDATE MB_Meniu SET MBME_Ascuns=1 WHERE MBME_Id=7; -- Cursuri devine ascuns (ramas gol)

-- 4. Turnee -> sub Activitate; Jucatori -> sub Turnee
UPDATE MB_Meniu SET MBME_MBME_Id=@idActivitate, MBME_OrdineAfisare=60 WHERE MBME_Id=1016; -- Turnee
UPDATE MB_Meniu SET MBME_MBME_Id=1016, MBME_OrdineAfisare=10, MBME_Nume=N'Jucători'      WHERE MBME_Id=1017;

-- 5. Solicitari -> top-level (scos din Administrare)
UPDATE MB_Meniu SET MBME_MBME_Id=0, MBME_OrdineAfisare=10 WHERE MBME_Id=19;

-- 6. Ordine module top-level
-- Solicitari=10, Activitate=20 (setat la INSERT)
UPDATE MB_Meniu SET MBME_OrdineAfisare=30 WHERE MBME_Id=1033; -- Financiar
UPDATE MB_Meniu SET MBME_OrdineAfisare=40 WHERE MBME_Id=21;   -- Situatii
UPDATE MB_Meniu SET MBME_OrdineAfisare=50 WHERE MBME_Id=16;   -- Administrare

-- Verifica rezultatul
SELECT MBME_Id, MBME_Nume, MBME_MBME_Id, MBME_OrdineAfisare, MBME_Ascuns
FROM MB_Meniu
WHERE MBME_Aria = '1CU' AND ISNULL(MBME_Ascuns,0) = 0
ORDER BY MBME_MBME_Id, MBME_OrdineAfisare;

-- Afla ID-ul noului modul Activitate
DECLARE @idActivitate INT = (SELECT MBME_Id FROM MB_Meniu WHERE MBME_Nume = N'Activitate' AND MBME_MBME_Id = 0 AND MBME_Aria = '1CU');

-- Copiaza drepturile de la un modul similar (ex. Financiar=1033) pentru toti utilizatorii din toate diviziunile
INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_MBDV_Id, MBDM_DreptFolosinta)
SELECT @idActivitate, MBDM_MBUT_Id, MBDM_MBDV_Id, MBDM_DreptFolosinta
FROM MB_DrepturiMeniu
WHERE MBDM_MBME_Id = 1033  -- Financiar ca model
  AND NOT EXISTS (
    SELECT 1 FROM MB_DrepturiMeniu
    WHERE MBDM_MBME_Id = @idActivitate
      AND MBDM_MBUT_Id = MB_DrepturiMeniu.MBDM_MBUT_Id
      AND MBDM_MBDV_Id = MB_DrepturiMeniu.MBDM_MBDV_Id
  );

SELECT @@ROWCOUNT AS [Drepturi inserate];

-- Ascunde "Cautare" sub Antrenori(1), Cursanti(3), Contracte(5) - nu mai sunt necesare vizual
UPDATE MB_Meniu SET MBME_Ascuns=1 WHERE MBME_Id IN (2, 4, 6);

-- Muta Jucatori direct sub Activitate (nu sub Turnee - evita nivelul 3)
DECLARE @idActivitate INT = (SELECT MBME_Id FROM MB_Meniu
    WHERE MBME_Nume=N'Activitate' AND MBME_MBME_Id=0 AND MBME_Aria='1CU');
UPDATE MB_Meniu SET MBME_MBME_Id=@idActivitate, MBME_OrdineAfisare=70 WHERE MBME_Id=1017;

UPDATE MB_Meniu SET MBME_Nume = N'Solicitări' WHERE MBME_Id = 19;

