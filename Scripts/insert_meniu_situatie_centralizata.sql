-- Adauga "Situatie centralizata" sub grupul Situatii (1CU)
-- Ruleaza DUPA insert_meniu_situatii.sql

IF NOT EXISTS (
    SELECT 1 FROM MB_Meniu WHERE MBME_Aria = '1CU' AND MBME_Nume = N'Sit'
)
BEGIN
    DECLARE @idSituatii INT = (
        SELECT MBME_Id FROM MB_Meniu
         WHERE MBME_Aria = '1CU' AND MBME_Nume LIKE N'Sit%'
           AND MBME_MBME_Id = 0
    );

    IF @idSituatii IS NULL
    BEGIN
        PRINT N'EROARE: Grupul Situatii nu exista. Ruleaza mai intai insert_meniu_situatii.sql.';
        RETURN;
    END

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', @idSituatii, N'Situatie centralizata', 2, 0);

    DECLARE @idSit INT = SCOPE_IDENTITY();

    -- Acorda drepturi tuturor utilizatorilor activi
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @idSit, MBRU_MBUT_Id, 1, MBRU_MBDV_Id
      FROM MB_RoluriUtilizatori
     INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
     WHERE MBUT_InUz = 1;

    -- NOTA: noteaza ID-ul si actualizeaza case-ul din FrmMeniuPrincipal.cs (CreeazaForm)
    SELECT @idSit AS [MBME_Id SituatieCentralizata - pune in switch C#];
END
ELSE
    PRINT N'Meniu Situatie centralizata exista deja.';
