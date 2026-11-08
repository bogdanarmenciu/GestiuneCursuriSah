-- Adauga intrarea "Jucatori" in meniu (lista centrala CU_Jucatori)
-- Ruleaza DUPA create_cu_jucatori.sql

IF NOT EXISTS (
    SELECT 1 FROM MB_Meniu WHERE MBME_Aria = '1CU' AND MBME_Nume = N'Jucatori'
)
BEGIN
    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', 0, N'Jucatori', 46, 0);

    DECLARE @idJucatori INT = SCOPE_IDENTITY();

    -- Acorda drepturi tuturor utilizatorilor activi
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @idJucatori, MBRU_MBUT_Id, 1, MBRU_MBDV_Id
      FROM MB_RoluriUtilizatori
     INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
     WHERE MBUT_InUz = 1;

    -- NOTA: noteaza ID-ul si actualizeaza case-ul din FrmMeniuPrincipal.cs (CreeazaForm)
    SELECT @idJucatori AS [MBME_Id Jucatori - pune in switch C#];
END
ELSE
    PRINT N'Meniu Jucatori exista deja.';
