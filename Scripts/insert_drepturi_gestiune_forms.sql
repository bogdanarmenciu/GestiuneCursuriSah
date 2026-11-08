-- ============================================================
-- Adauga intrari ASCUNSE in MB_Meniu pentru toate FrmGestiune*
-- Scopul: gestiunea drepturilor per-utilizator, independent de
--         calea de acces la forma.
-- Ruleaza o singura data; e idempotent (IF NOT EXISTS).
-- ============================================================

-- ============================================================
-- 1. FrmGestiuneCursant  (1CU / Cursanti)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Cursant')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Cursanti'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
        ORDER BY MBME_OrdineAfisare
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Cursant', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Cursant adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Cursant exista deja.';
GO

-- ============================================================
-- 2. FrmGestiuneFormator  (1CU / Formatori)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Formator')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Formatori'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
        ORDER BY MBME_OrdineAfisare
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Formator', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Formator adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Formator exista deja.';
GO

-- ============================================================
-- 3. FrmGestiuneContract  (1CU / Contracte)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Contract')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Contracte'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
        ORDER BY MBME_OrdineAfisare
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Contract', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Contract adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Contract exista deja.';
GO

-- ============================================================
-- 4. FrmGestiuneAbonament  (1CU / Cursuri)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Abonament')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Cursuri'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0 AND MBME_Nume LIKE N'%bona%'
        ORDER BY MBME_OrdineAfisare
    );
    -- fallback: orice copil vizibil
    IF @source IS NULL
        SET @source = (
            SELECT TOP 1 MBME_Id FROM MB_Meniu
            WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
            ORDER BY MBME_OrdineAfisare
        );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Abonament', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Abonament adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Abonament exista deja.';
GO

-- ============================================================
-- 5. FrmGestiuneIncasariAbonament  (1CU / Cursuri)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Incasari Abonament')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Cursuri'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0 AND MBME_Nume LIKE N'%ncas%'
        ORDER BY MBME_OrdineAfisare
    );
    IF @source IS NULL
        SET @source = (
            SELECT TOP 1 MBME_Id FROM MB_Meniu
            WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
            ORDER BY MBME_OrdineAfisare
        );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Incasari Abonament', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Incasari Abonament adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Incasari Abonament exista deja.';
GO

-- ============================================================
-- 6. FrmGestiunePrezenta  (1CU / Cursuri)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Prezenta')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Cursuri'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0 AND MBME_Nume LIKE N'%rezent%'
        ORDER BY MBME_OrdineAfisare
    );
    IF @source IS NULL
        SET @source = (
            SELECT TOP 1 MBME_Id FROM MB_Meniu
            WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
            ORDER BY MBME_OrdineAfisare
        );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Prezenta', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Prezenta adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Prezenta exista deja.';
GO

-- ============================================================
-- 7. FrmGestiuneFactura  (1CU / Cursuri — admin initial)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Factura')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0
          AND (MBME_Nume = N'Cursuri' OR MBME_Nume LIKE N'%actur%')
        ORDER BY (CASE WHEN MBME_Nume LIKE N'%actur%' THEN 0 ELSE 1 END)
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
        ORDER BY MBME_OrdineAfisare
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Factura', 99, 1);
    SET @id = SCOPE_IDENTITY();

    -- Factura: initial doar admin
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
    INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
    WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Factura adaugat (admin only), MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Factura exista deja.';
GO

-- ============================================================
-- 8. FrmGestiuneTurnee  (1CU / Turnee)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Turnee')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Turnee'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
        ORDER BY MBME_OrdineAfisare
    );
    -- fallback: grupul Turnee insusi are drepturi
    IF @source IS NULL SET @source = @parent;

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Turnee', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Turnee adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Turnee exista deja.';
GO

-- ============================================================
-- 9. FrmGestiunePartener  (1CU / Administrare)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Partener')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Administrare'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0 AND MBME_Nume LIKE N'%artener%'
        ORDER BY MBME_OrdineAfisare
    );
    IF @source IS NULL
        SET @source = (
            SELECT TOP 1 MBME_Id FROM MB_Meniu
            WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
            ORDER BY MBME_OrdineAfisare
        );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Partener', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Partener adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Partener exista deja.';
GO

-- ============================================================
-- 10. FrmGestiuneSolicitare  (1CU / Administrare)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Solicitare')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Administrare'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0 AND MBME_Nume LIKE N'%olicit%'
        ORDER BY MBME_OrdineAfisare
    );
    IF @source IS NULL
        SET @source = (
            SELECT TOP 1 MBME_Id FROM MB_Meniu
            WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
            ORDER BY MBME_OrdineAfisare
        );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Solicitare', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Solicitare adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Solicitare exista deja.';
GO

-- ============================================================
-- 11. FrmGestiuneRegistruNumereInterne  (1CU / Administrare)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Registru')
BEGIN
    DECLARE @parent INT, @source INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Administrare'
    );
    SET @source = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0 AND MBME_Nume LIKE N'%egistru%'
        ORDER BY MBME_OrdineAfisare
    );
    IF @source IS NULL
        SET @source = (
            SELECT TOP 1 MBME_Id FROM MB_Meniu
            WHERE MBME_MBME_Id = @parent AND MBME_Ascuns = 0
            ORDER BY MBME_OrdineAfisare
        );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Registru', 99, 1);
    SET @id = SCOPE_IDENTITY();

    IF @source IS NOT NULL
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id
        FROM MB_DrepturiMeniu WHERE MBDM_MBME_Id = @source;
    ELSE
        INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
        SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
        INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
        WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Registru adaugat, MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Registru exista deja.';
GO

-- ============================================================
-- 12. FrmGestiuneUtilizator  (1CU / Administrare — admin only)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Utilizator')
BEGIN
    DECLARE @parent INT, @id INT;

    SET @parent = (
        SELECT MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '1CU' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0 AND MBME_Nume = N'Administrare'
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', ISNULL(@parent, 0), N'Gestiune Utilizator', 99, 1);
    SET @id = SCOPE_IDENTITY();

    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
    INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
    WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Utilizator adaugat (admin only), MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Utilizator exista deja.';
GO

-- ============================================================
-- 13. FrmGestiuneDiviziune  (0CG — admin only)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Diviziune')
BEGIN
    DECLARE @parent INT, @id INT;

    SET @parent = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '0CG' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0
        ORDER BY MBME_OrdineAfisare
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('0CG', ISNULL(@parent, 0), N'Gestiune Diviziune', 99, 1);
    SET @id = SCOPE_IDENTITY();

    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
    INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
    WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Diviziune adaugat (admin only), MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Diviziune exista deja.';
GO

-- ============================================================
-- 14. FrmGestiuneSocietate  (0CG — admin only)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Ascuns = 1 AND MBME_Nume = N'Gestiune Societate')
BEGIN
    DECLARE @parent INT, @id INT;

    SET @parent = (
        SELECT TOP 1 MBME_Id FROM MB_Meniu
        WHERE MBME_Aria = '0CG' AND MBME_MBME_Id = 0 AND MBME_Ascuns = 0
        ORDER BY MBME_OrdineAfisare
    );

    INSERT INTO MB_Meniu (MBME_Aria, MBME_MBME_Id, MBME_Nume, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('0CG', ISNULL(@parent, 0), N'Gestiune Societate', 99, 1);
    SET @id = SCOPE_IDENTITY();

    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_DreptFolosinta, MBDM_MBDV_Id)
    SELECT @id, MBRU_MBUT_Id, 1, MBRU_MBDV_Id FROM MB_RoluriUtilizatori
    INNER JOIN MB_Utilizatori ON MBRU_MBUT_Id = MBUT_Id
    WHERE MBUT_InUz = 1 AND MBRU_Administrator = 1;

    PRINT N'[OK] Gestiune Societate adaugat (admin only), MBME_Id = ' + CAST(@id AS NVARCHAR);
END
ELSE PRINT N'[SKIP] Gestiune Societate exista deja.';
GO
