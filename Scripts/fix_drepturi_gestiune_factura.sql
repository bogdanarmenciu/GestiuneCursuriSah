-- ============================================================
-- Creaza intrarea ascunsa "Gestiune Factura" in MB_Meniu
-- si copiaza drepturile de la "Gestiune Contract" (Id=1021).
-- Ruleaza o singura data; e idempotent (IF NOT EXISTS).
-- ============================================================

IF NOT EXISTS (
    SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Factura'
)
BEGIN
    DECLARE @idFactura INT;

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', 0, N'Gestiune Factura', 0, 1);
    SET @idFactura = SCOPE_IDENTITY();

    -- Copiaza drepturile de la Gestiune Contract (MBME_Id = 1021)
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @idFactura, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
    FROM MB_DrepturiMeniu
    WHERE MBDM_MBME_Id = 1021;

    PRINT N'[OK] Gestiune Factura creata, MBME_Id = ' + CAST(@idFactura AS NVARCHAR)
        + N', ' + CAST(@@ROWCOUNT AS NVARCHAR) + N' drepturi copiate.';
END
ELSE
    PRINT N'[SKIP] Gestiune Factura exista deja.';
