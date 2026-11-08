-- ============================================================
-- Creaza grupul "Financiar" in meniu si muta/adauga sub el:
--   - Facturi (intrare noua)
--   - Incasari Abonamente (mutata din grupul Cursanti)
-- Ruleaza o singura data; e idempotent (IF NOT EXISTS / IF EXISTS).
-- ============================================================

-- ============================================================
-- 1. Creeaza grupul "Financiar" (nivel 0, frate cu "Cursanti")
-- ============================================================
IF NOT EXISTS (
    SELECT 1 FROM MB_Meniu
    WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0
      AND MBME_Ascuns = 0 AND MBME_Nume = N'Financiar'
)
BEGIN
    DECLARE @idFinanciar INT;

    -- Ordinea: dupa grupul Cursanti
    DECLARE @ordineCursanti INT;
    SET @ordineCursanti = ISNULL((
        SELECT MBME_OrdineAfisare FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0
          AND MBME_Ascuns = 0 AND MBME_Nume = N'Cursanti'
    ), 0);

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', 0, N'Financiar', @ordineCursanti + 1, 0);
    SET @idFinanciar = SCOPE_IDENTITY();

    -- Copiaza drepturile de la grupul Cursanti (acelasi set de utilizatori)
    DECLARE @idCursanti INT;
    SET @idCursanti = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0
          AND MBME_Ascuns = 0 AND MBME_Nume = N'Cursanti'
    );
    IF @idCursanti IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @idFinanciar, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @idCursanti;

    PRINT N'[OK] Grup Financiar creat, MBME_Id = ' + CAST(@idFinanciar AS NVARCHAR);
END
ELSE
    PRINT N'[SKIP] Grup Financiar exista deja.';

GO

-- ============================================================
-- 2. Adauga "Facturi" sub grupul "Financiar"
-- ============================================================
IF NOT EXISTS (
    SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 0 AND MBME_Nume = N'Facturi'
)
BEGIN
    DECLARE @idFinanciar2 INT, @idFacturi INT, @sursaFacturi INT;

    SET @idFinanciar2 = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0
          AND MBME_Ascuns = 0 AND MBME_Nume = N'Financiar'
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@idFinanciar2, 0), N'Facturi', 1, 0);
    SET @idFacturi = SCOPE_IDENTITY();

    -- Copiaza drepturile de la "Contracte" (cel mai apropiat frate semantic)
    SET @sursaFacturi = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_Ascuns = 0
          AND MBME_Nume = N'Contracte'
    );
    IF @sursaFacturi IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @idFacturi, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @sursaFacturi;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @idFacturi, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Facturi adaugat, MBME_Id = ' + CAST(@idFacturi AS NVARCHAR);
END
ELSE
    PRINT N'[SKIP] Facturi exista deja.';

GO

-- ============================================================
-- 3. Muta "Incasari Abonamente" din Cursanti in Financiar
-- ============================================================
DECLARE @idFinanciar3 INT, @idCursanti3 INT, @idIncasari INT;

SET @idFinanciar3 = (
    SELECT MBME_Id FROM MB_Meniu
    WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0
      AND MBME_Ascuns = 0 AND MBME_Nume = N'Financiar'
);
SET @idCursanti3 = (
    SELECT MBME_Id FROM MB_Meniu
    WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0
      AND MBME_Ascuns = 0 AND MBME_Nume = N'Cursanti'
);
SET @idIncasari = (
    SELECT MBME_Id FROM MB_Meniu
    WHERE MBME_MBME_Id = @idCursanti3 AND MBME_Ascuns = 0
      AND MBME_Nume = N'Incasari Abonamente'
);

IF @idFinanciar3 IS NOT NULL AND @idIncasari IS NOT NULL
    AND (SELECT MBME_MBME_Id FROM MB_Meniu WHERE MBME_Id = @idIncasari) <> @idFinanciar3
BEGIN
    UPDATE MB_Meniu
    SET MBME_MBME_Id = @idFinanciar3, MBME_OrdineAfisare = 2
    WHERE MBME_Id = @idIncasari;
    PRINT N'[OK] Incasari Abonamente mutat in Financiar.';
END
ELSE
    PRINT N'[SKIP] Incasari Abonamente deja in Financiar sau nu a fost gasit.';

-- Muta Facturi sub Financiar
UPDATE MB_Meniu SET MBME_MBME_Id = 1033, MBME_OrdineAfisare = 1
WHERE MBME_Id = 1032;

-- Muta Incasari abonamente sub Financiar
UPDATE MB_Meniu SET MBME_MBME_Id = 1033, MBME_OrdineAfisare = 2
WHERE MBME_Id = 12;
