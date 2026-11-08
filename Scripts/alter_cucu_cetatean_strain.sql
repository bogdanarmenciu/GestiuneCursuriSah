-- Adaugă suport cetățeni străini pe tabela cursanților
ALTER TABLE CU_Cursanti
    ADD CUCU_CetAteanStrain BIT NOT NULL DEFAULT 0;

ALTER TABLE CU_Cursanti
    ADD CUCU_NrDocumentStrain NVARCHAR(100) NULL;

-- Verificare
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
  FROM INFORMATION_SCHEMA.COLUMNS
 WHERE TABLE_NAME = 'CU_Cursanti'
   AND COLUMN_NAME IN ('CUCU_CetAteanStrain', 'CUCU_NrDocumentStrain');
