-- Leagă nota de reziliere de persoana specifică (nu doar de contract)
-- Necesar când un contract are 2 persoane și doar una se retrage
ALTER TABLE AD_RegistruNumereInterne
    ADD ADRN_CUCU_Id INT NULL;

-- FK opțional (las NULL pentru înregistrările vechi)
-- ALTER TABLE AD_RegistruNumereInterne
--     ADD CONSTRAINT FK_ADRN_CUCU FOREIGN KEY (ADRN_CUCU_Id)
--         REFERENCES CU_Cursanti(CUCU_Id);

-- Verificare
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
  FROM INFORMATION_SCHEMA.COLUMNS
 WHERE TABLE_NAME = 'AD_RegistruNumereInterne' AND COLUMN_NAME = 'ADRN_CUCU_Id';
