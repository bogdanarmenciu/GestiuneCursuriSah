-- ============================================================
-- SmartBill config on MB_Societate
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('MB_Societate') AND name = 'MBSO_SmartBillEmail')
    ALTER TABLE MB_Societate ADD MBSO_SmartBillEmail  nvarchar(100) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('MB_Societate') AND name = 'MBSO_SmartBillApiKey')
    ALTER TABLE MB_Societate ADD MBSO_SmartBillApiKey nvarchar(200) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('MB_Societate') AND name = 'MBSO_SmartBillSerie')
    ALTER TABLE MB_Societate ADD MBSO_SmartBillSerie  nvarchar(20)  NULL;

-- ============================================================
-- Tracking factura per linie prezenta individuala
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_PrezenteDetaliu') AND name = 'CUPD_FVFA_Id')
    ALTER TABLE CU_PrezenteDetaliu ADD CUPD_FVFA_Id int NULL
        CONSTRAINT FK_CUPD_FVFA FOREIGN KEY REFERENCES FV_Facturi(FVFA_Id);

-- ============================================================
-- Meniu "Setari generale" in zona Setari (0CG), nivel 0
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Aria = '0CG' AND MBME_Nume = 'Setari generale')
    INSERT INTO MB_Meniu (MBME_Aria, MBME_Nume, MBME_MBME_Id, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('0CG', 'Setari generale', 0, 1, 0);

-- ============================================================
-- Meniu "Situatie financiara" in zona Meniu cursanti (1CU), nivel 0
-- Drepturi: se adauga manual din Gestiune drepturi meniu dupa insert
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Aria = '1CU' AND MBME_Nume = 'Situatie financiara')
    INSERT INTO MB_Meniu (MBME_Aria, MBME_Nume, MBME_MBME_Id, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', 'Situatie financiara', 0, 25, 0);

IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Aria = '0CG' AND MBME_Nume = 'SmartBill' AND MBME_MBME_Id = 13)
    INSERT INTO MB_Meniu (MBME_Aria, MBME_Nume, MBME_MBME_Id, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('0CG', 'SmartBill', 13, 5, 0);

-- Mută "Situatie financiara" sub "Situatii faine" (ID=21) — dinamic, fără ID hardcodat
DECLARE @sitFinId INT;
SET @sitFinId = (SELECT MBME_Id FROM MB_Meniu WHERE MBME_Aria = '1CU' AND MBME_Nume = 'Situatie financiara');

IF @sitFinId IS NOT NULL
BEGIN
    UPDATE MB_Meniu SET MBME_MBME_Id = 21 WHERE MBME_Id = @sitFinId;

    -- Adaugă drept de folosință pentru toți utilizatorii care au drepturi la "Situatii faine"
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_MBDV_Id, MBDM_DreptFolosinta)
    SELECT @sitFinId, MBDM_MBUT_Id, MBDM_MBDV_Id, 1
    FROM MB_DrepturiMeniu
    WHERE MBDM_MBME_Id = 21
      AND NOT EXISTS (
        SELECT 1 FROM MB_DrepturiMeniu d2
        WHERE d2.MBDM_MBME_Id = @sitFinId
          AND d2.MBDM_MBUT_Id = MB_DrepturiMeniu.MBDM_MBUT_Id
          AND d2.MBDM_MBDV_Id = MB_DrepturiMeniu.MBDM_MBDV_Id);
END

-- ============================================================
-- Meniu "Import SmartBill" in zona Meniu cursanti (1CU), sub ID=21
-- ============================================================
DECLARE @importSBId INT;
IF NOT EXISTS (SELECT 1 FROM MB_Meniu WHERE MBME_Aria = '1CU' AND MBME_Nume = 'Import SmartBill')
BEGIN
    INSERT INTO MB_Meniu (MBME_Aria, MBME_Nume, MBME_MBME_Id, MBME_OrdineAfisare, MBME_Ascuns)
    VALUES ('1CU', 'Import SmartBill', 21, 26, 1);
END

SET @importSBId = (SELECT MBME_Id FROM MB_Meniu WHERE MBME_Aria = '1CU' AND MBME_Nume = 'Import SmartBill');

-- Drepturi: same users who have access to parent menu (21)
IF @importSBId IS NOT NULL
BEGIN
    INSERT INTO MB_DrepturiMeniu (MBDM_MBME_Id, MBDM_MBUT_Id, MBDM_MBDV_Id, MBDM_DreptFolosinta)
    SELECT @importSBId, MBDM_MBUT_Id, MBDM_MBDV_Id, 1
    FROM MB_DrepturiMeniu
    WHERE MBDM_MBME_Id = 21
      AND NOT EXISTS (
        SELECT 1 FROM MB_DrepturiMeniu d2
        WHERE d2.MBDM_MBME_Id = @importSBId
          AND d2.MBDM_MBUT_Id = MB_DrepturiMeniu.MBDM_MBUT_Id
          AND d2.MBDM_MBDV_Id = MB_DrepturiMeniu.MBDM_MBDV_Id);
END
