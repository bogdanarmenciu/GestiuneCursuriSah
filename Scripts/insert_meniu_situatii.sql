-- Adauga grupul Situatii si sub-elementul Situatie prezente in meniu
-- Ruleaza DUPA alter_cuab_ore_per_luna.sql

IF NOT EXISTS (
    SELECT 1 FROM MB_Meniu
     WHERE MBME_Aria = '1CU' AND MBME_Nume LIKE N'Situatii%'
)
BEGIN
    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', 0, N'Situatii', 50, 0);

    DECLARE @idSituatii INT = SCOPE_IDENTITY();

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', @idSituatii, N'Situatie prezente', 1, 0);

    DECLARE @idSituatiePrezente INT = SCOPE_IDENTITY();

    -- Acorda drepturi doar administratorilor (MBRU_Administrator = 1)
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @idSituatii, MBRU_MBUT_Id, 1, MBRU_MBDV_Id
      FROM MB_RoluriUtilizatori
     INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
     WHERE MBRU_Administrator = 1
       AND MBUT_InUz = 1;

    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @idSituatiePrezente, MBRU_MBUT_Id, 1, MBRU_MBDV_Id
      FROM MB_RoluriUtilizatori
     INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
     WHERE MBRU_Administrator = 1
       AND MBUT_InUz = 1;

    -- NOTA: noteaza ID-ul din rezultat si actualizeaza case-ul din FrmMeniuPrincipal.cs
    SELECT @idSituatii         AS [MBME_Id parinte Situatii],
           @idSituatiePrezente AS [MBME_Id SituatiePrezente - pune in switch C#];
END
ELSE
    PRINT N'Meniu Situatii exista deja.';
