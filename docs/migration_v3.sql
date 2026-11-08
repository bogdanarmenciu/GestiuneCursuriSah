-- ============================================================
-- Migration v3 — Istoricul prețurilor per tip abonament
-- Idempotent: poate fi rulat de oricâte ori fără efecte secundare
-- ============================================================

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'CU_PreturiAbonamente'))
BEGIN
    CREATE TABLE CU_PreturiAbonamente (
        CUABP_Id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CUABP_CUAB_Id     INT          NOT NULL,
        CUABP_DataStart   DATE         NOT NULL,
        CUABP_DataSfarsit DATE         NULL,     -- NULL = valabil și în prezent
        CUABP_Pret        DECIMAL(9,2) NOT NULL
    );
END;

-- Migrare prețuri curente în noua tabelă (01.01.2022 – prezent), idempotent
-- DataSfarsit = NULL înseamnă "valabil în prezent și în continuare"
INSERT INTO CU_PreturiAbonamente (CUABP_CUAB_Id, CUABP_DataStart, CUABP_DataSfarsit, CUABP_Pret)
SELECT CUAB_Id, '2022-01-01', NULL, CUAB_Pret
FROM CU_Abonamente
WHERE CUAB_Pret IS NOT NULL
  AND NOT EXISTS (
      SELECT 1 FROM CU_PreturiAbonamente WHERE CUABP_CUAB_Id = CUAB_Id
  );

-- Fac CUAB_Pret nullable (codul nu îl mai folosește; coloana rămâne în BD pentru compatibilitate)
IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CU_Abonamente') AND name = 'CUAB_Pret' AND is_nullable = 0
)
    ALTER TABLE CU_Abonamente ALTER COLUMN CUAB_Pret DECIMAL(9,2) NULL;

-- Corectează datele de sfârșit existente: aduce CUAC_DataSfarsit la ultima zi din lună
-- (migrarea anterioară a scăzut eronat 1 zi din date deja corecte)
UPDATE CU_AbonamenteCursant
SET    CUAC_DataSfarsit = EOMONTH(CUAC_DataSfarsit)
WHERE  CUAC_DataSfarsit <> EOMONTH(CUAC_DataSfarsit);

-- Verificare rapidă
SELECT 'CU_PreturiAbonamente' AS Tabel, COUNT(*) AS Randuri FROM CU_PreturiAbonamente
UNION ALL
SELECT 'CU_Abonamente (fara pret null)', COUNT(*) FROM CU_Abonamente WHERE CUAB_Pret IS NULL
UNION ALL
SELECT 'CU_AbonamenteCursant corectat', COUNT(*) FROM CU_AbonamenteCursant
WHERE  CUAC_DataSfarsit = EOMONTH(CUAC_DataSfarsit);
