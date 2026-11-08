-- Adauga CUCU_RatingCurent pe CU_Cursanti
-- Se actualizeaza automat la fiecare import de turneu
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_Cursanti') AND name = 'CUCU_RatingCurent')
    ALTER TABLE CU_Cursanti ADD CUCU_RatingCurent INT NULL;
