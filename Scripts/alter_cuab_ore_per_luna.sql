-- Adaugă limita de ore/lună pe tipul de abonament (NULL = nelimitat)
ALTER TABLE CU_Abonamente
    ADD CUAB_OrePerLuna DECIMAL(5,1) NULL;

-- "Prima Mutare" = 8 ore/lună calendaristică
UPDATE CU_Abonamente
   SET CUAB_OrePerLuna = 8.0
 WHERE CUAB_Id = 8;  -- Prima Mutare

-- Verificare
SELECT CUAB_Id, CUAB_Descriere, CUAB_OrePerLuna
  FROM CU_Abonamente
 ORDER BY CUAB_Id;
