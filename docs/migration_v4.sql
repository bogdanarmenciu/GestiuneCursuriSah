-- ============================================================
-- Migration v4 — Notă de reziliere contract
-- Idempotent: poate fi rulat de oricâte ori
-- ============================================================

-- 1. Coloana CUCO_Retras pe CU_Contracte
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CU_Contracte') AND name = 'CUCO_Retras'
)
    ALTER TABLE CU_Contracte ADD CUCO_Retras BIT NOT NULL DEFAULT 0;

-- 2. Tip document 7 = Notă de reziliere (în registrul de numere interne)
SET IDENTITY_INSERT AD_TipuriDocumenteNumarIntern ON;

IF NOT EXISTS (SELECT 1 FROM AD_TipuriDocumenteNumarIntern WHERE ADTD_Id = 7)
    INSERT INTO AD_TipuriDocumenteNumarIntern
        (ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit, ADTD_Activ, ADTD_OrdineAfisare)
    VALUES (7, 'Nota de reziliere', 1, 1, 7);

SET IDENTITY_INSERT AD_TipuriDocumenteNumarIntern OFF;

-- Verificare
SELECT 'CU_Contracte.CUCO_Retras' AS Coloana,
       (SELECT COUNT(*) FROM CU_Contracte WHERE CUCO_Retras = 1) AS Contracte_retrase;
SELECT ADTD_Id, ADTD_Denumire FROM AD_TipuriDocumenteNumarIntern ORDER BY ADTD_Id;
