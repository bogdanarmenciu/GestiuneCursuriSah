-- ============================================================
-- Migration v5 — Tip prezenta + sedinta individuala
-- Idempotent: poate fi rulat de oricâte ori
-- ============================================================

-- 1. Tabela tipuri prezenta
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('CU_TipPrezenta'))
BEGIN
    CREATE TABLE CU_TipPrezenta (
        CUTP_Id                      INT          NOT NULL PRIMARY KEY,
        CUTP_Descriere               VARCHAR(100) NOT NULL,
        CUTP_SedintaIndividuala BIT          NOT NULL DEFAULT 0
    );
END;

IF NOT EXISTS (SELECT 1 FROM CU_TipPrezenta WHERE CUTP_Id = 1)
    INSERT INTO CU_TipPrezenta (CUTP_Id, CUTP_Descriere, CUTP_SedintaIndividuala)
    VALUES (1, 'Sedinta Grup', 0);

IF NOT EXISTS (SELECT 1 FROM CU_TipPrezenta WHERE CUTP_Id = 2)
    INSERT INTO CU_TipPrezenta (CUTP_Id, CUTP_Descriere, CUTP_SedintaIndividuala)
    VALUES (2, 'Sedinta Individuala', 1);

-- 2. Flag sedinta individuala pe tipul de abonament
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CU_Abonamente') AND name = 'CUAB_SedintaIndividuala'
)
    ALTER TABLE CU_Abonamente ADD CUAB_SedintaIndividuala BIT NOT NULL DEFAULT 0;

-- 3. Tip prezenta pe sesiunea de prezenta (default = Sedinta Grup)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CU_Prezente') AND name = 'CUPR_CUTP_Id'
)
    ALTER TABLE CU_Prezente ADD CUPR_CUTP_Id INT NOT NULL DEFAULT 1;

-- Verificare
SELECT 'CU_TipPrezenta'                AS Tabel, COUNT(*) AS Randuri FROM CU_TipPrezenta
UNION ALL
SELECT 'CU_Abonamente (individuale)',  COUNT(*) FROM CU_Abonamente WHERE CUAB_SedintaIndividuala = 1
UNION ALL
SELECT 'CU_Prezente cu CUTP',         COUNT(*) FROM CU_Prezente WHERE CUPR_CUTP_Id IS NOT NULL;
