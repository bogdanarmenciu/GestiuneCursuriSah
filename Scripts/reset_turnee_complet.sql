-- ============================================================
-- Sterge TOATE datele de turnee si jucatori, reseteaza ID-uri
-- Ruleaza o singura data pentru restart curat
-- ============================================================

BEGIN TRANSACTION;

-- 1. jocuri (FK spre detaliu)
DELETE FROM CU_TurneeJocuri;

-- 2. detalii participare (FK spre turneu si jucator)
DELETE FROM CU_TurneeDetaliu;

-- 3. turnee
DELETE FROM CU_Turnee;

-- 4. jucatori centrali
DELETE FROM CU_Jucatori;

-- 5. reseteaza contoarele IDENTITY
DBCC CHECKIDENT ('CU_TurneeJocuri',  RESEED, 0);
DBCC CHECKIDENT ('CU_TurneeDetaliu', RESEED, 0);
DBCC CHECKIDENT ('CU_Turnee',        RESEED, 0);
DBCC CHECKIDENT ('CU_Jucatori',      RESEED, 0);

COMMIT;

PRINT 'Stergere completa. Toate turneele, detaliile si jucatorii au fost eliminati.';
