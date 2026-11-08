-- Seteaza tipul documentului de identitate pe baza varstei
-- < 14 ani  => 2 (Certificat de nastere)
-- >= 14 ani => 1 (Carte de identitate)
-- Actualizeaza doar inregistrarile fara tip setat si cu data nasterii completata

UPDATE CU_Cursanti
SET CUCU_TipDocument =
    CASE
        WHEN DATEDIFF(year, CUCU_DataNasterii, GETDATE())
             - CASE WHEN MONTH(CUCU_DataNasterii) * 100 + DAY(CUCU_DataNasterii)
                         > MONTH(GETDATE()) * 100 + DAY(GETDATE())
                    THEN 1 ELSE 0 END < 14
        THEN 2  -- Certificat de nastere
        ELSE 1  -- Carte de identitate
    END
WHERE CUCU_TipDocument IS NULL
  AND CUCU_DataNasterii IS NOT NULL;

-- Raport
SELECT
    CUCU_TipDocument,
    CASE CUCU_TipDocument WHEN 1 THEN 'Carte de identitate' WHEN 2 THEN 'Certificat de nastere' END AS Tip,
    COUNT(*) AS Nr
FROM CU_Cursanti
WHERE CUCU_TipDocument IS NOT NULL
GROUP BY CUCU_TipDocument
ORDER BY CUCU_TipDocument;
