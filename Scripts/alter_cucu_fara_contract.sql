-- Suport "cursant rapid" — înregistrare minimă (Nume+Prenume) fără contract
-- CUCU_FaraContract=1 înseamnă că urmează să aducă actele pentru contract real

-- 1. Flag cursant fără contract
ALTER TABLE CU_Cursanti
    ADD CUCU_FaraContract BIT NOT NULL DEFAULT 0;

-- 2. CNP devine nullable — cursanții rapizi nu au CNP la înrolare
--    Eliminăm mai întâi eventuala constrângere UNIQUE pe CNP
DECLARE @uqCNP NVARCHAR(256);
SELECT TOP 1 @uqCNP = TC.CONSTRAINT_NAME
  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KU
         ON KU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
 WHERE TC.TABLE_NAME = 'CU_Cursanti'
   AND TC.CONSTRAINT_TYPE = 'UNIQUE'
   AND KU.COLUMN_NAME = 'CUCU_CNP';

IF @uqCNP IS NOT NULL
    EXEC('ALTER TABLE CU_Cursanti DROP CONSTRAINT ' + @uqCNP);

ALTER TABLE CU_Cursanti
    ALTER COLUMN CUCU_CNP NVARCHAR(13) NULL;

-- Verificare
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
  FROM INFORMATION_SCHEMA.COLUMNS
 WHERE TABLE_NAME = 'CU_Cursanti'
   AND COLUMN_NAME IN ('CUCU_FaraContract', 'CUCU_CNP');
