-- Adaugă coloana folder arhivă pe tipurile de documente pentru nr. interne
ALTER TABLE AD_TipuriDocumenteNumarIntern
    ADD ADTD_FolderArhiva NVARCHAR(260) NULL;
GO

-- Setează folderele pentru tipurile existente
-- Ajustează valorile după denumirile reale din tabela ta
UPDATE AD_TipuriDocumenteNumarIntern
   SET ADTD_FolderArhiva = 'CONTRACTE'
 WHERE ADTD_Denumire LIKE '%contract%cursant%'
    OR ADTD_Denumire LIKE '%contract%pregatire%'
    OR ADTD_Denumire LIKE '%contract%sportiv%';

UPDATE AD_TipuriDocumenteNumarIntern
   SET ADTD_FolderArhiva = 'CONTRACTE SPONSORIZARE'
 WHERE ADTD_Denumire LIKE '%sponsor%';

-- Vezi ce tipuri există și ce folder au primit (sau NULL = nesetat):
SELECT ADTD_Id, ADTD_Denumire, ADTD_FolderArhiva
  FROM AD_TipuriDocumenteNumarIntern
 ORDER BY ADTD_OrdineAfisare;
