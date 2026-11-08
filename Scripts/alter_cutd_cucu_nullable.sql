-- Permite importul tuturor jucătorilor dintr-un turneu, nu doar al cursanților noștri.
-- Jucătorii externi vor avea CUTD_CUCU_Id = NULL.

-- 1. Drop FK existent pe CUTD_CUCU_Id
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_CU_TurneeDetaliu_Cursant')
    ALTER TABLE CU_TurneeDetaliu DROP CONSTRAINT FK_CU_TurneeDetaliu_Cursant;

-- 2. Fă coloana nullable
ALTER TABLE CU_TurneeDetaliu ALTER COLUMN CUTD_CUCU_Id INT NULL;

-- 3. Recreează FK (NULL nu violează FK în SQL Server)
ALTER TABLE CU_TurneeDetaliu
    ADD CONSTRAINT FK_CU_TurneeDetaliu_Cursant
    FOREIGN KEY (CUTD_CUCU_Id) REFERENCES CU_Cursanti(CUCU_Id);
