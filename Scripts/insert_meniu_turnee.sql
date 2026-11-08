-- Adauga intrarea "Turnee" in meniu
-- Ruleaza DUPA create_cu_turnee.sql

IF NOT EXISTS (
    SELECT 1 FROM MB_Meniu WHERE MBME_Aria = '1CU' AND MBME_Nume = N'Turnee'
)
BEGIN
    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', 0, N'Turnee', 45, 0);

    DECLARE @idTurnee INT = SCOPE_IDENTITY();

    -- Acorda drepturi tuturor utilizatorilor activi
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @idTurnee, MBRU_MBUT_Id, 1, MBRU_MBDV_Id
      FROM MB_RoluriUtilizatori
     INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
     WHERE MBUT_InUz = 1;

    -- NOTA: noteaza ID-ul si actualizeaza case-ul din FrmMeniuPrincipal.cs (CreeazaForm)
    SELECT @idTurnee AS [MBME_Id Turnee - pune in switch C#];
END
ELSE
    PRINT N'Meniu Turnee exista deja.';
