-- Resetează toate facturile la status Neîncasată și șterge toate înregistrările din CU_Incasari
-- !! ATENȚIE: operație ireversibilă, rulează doar pe date de test !!

BEGIN TRANSACTION;

-- 1. Șterge toate înregistrările de încasări
DELETE FROM CU_Incasari;

-- 2. Resetează câmpurile de încasare de pe toate facturile
UPDATE FV_Facturi
SET FVFA_TipIncasare  = 0,
    FVFA_NrDocument   = NULL,
    FVFA_DataIncasare = NULL,
    FVFA_ADRN_Id      = NULL;

-- Verificare
SELECT COUNT(*) AS [Facturi resetate] FROM FV_Facturi;
SELECT COUNT(*) AS [Incasari ramase]  FROM CU_Incasari;

COMMIT;
