-- Repară legătura ADAH_ADRN_Id pentru fișierele arhivate fără link la registru
-- Rulează după ce nr. intern au fost create automat, dar arhiva nu a primit legătura

-- 1. Contracte (un singur ADRN cu ADTD_Id=1 per contract)
UPDATE AD_Arhiva
   SET ADAH_ADRN_Id = ADRN.ADRN_Id
  FROM AD_Arhiva
 INNER JOIN AD_RegistruNumereInterne ADRN
         ON ADRN.ADRN_CUCO_Id = AD_Arhiva.ADAH_CUCO_Id
        AND ADRN.ADRN_ADTD_Id = 1
 WHERE AD_Arhiva.ADAH_ADRN_Id IS NULL
   AND AD_Arhiva.ADAH_NumeFisier LIKE 'Contract[_]%';

-- 2. Note de reziliere (un singur ADRN cu ADTD_Id=7 per contract)
UPDATE AD_Arhiva
   SET ADAH_ADRN_Id = ADRN.ADRN_Id
  FROM AD_Arhiva
 INNER JOIN AD_RegistruNumereInterne ADRN
         ON ADRN.ADRN_CUCO_Id = AD_Arhiva.ADAH_CUCO_Id
        AND ADRN.ADRN_ADTD_Id = 7
 WHERE AD_Arhiva.ADAH_ADRN_Id IS NULL
   AND AD_Arhiva.ADAH_NumeFisier LIKE 'Nota%Reziliere%';

-- 3. Acte adiționale (ADTD_Id=6, pot fi mai multe per contract)
--    Extragem nr. act din nume fișier: ActAditional_{nrContract}_{nrAct}_{...}.docx
--    Comparăm cu ROW_NUMBER() ordonat după ADRN_Numar
UPDATE AD_Arhiva
   SET ADAH_ADRN_Id = sub.ADRN_Id
  FROM AD_Arhiva
 INNER JOIN (
     SELECT ar.ADAH_Id,
            rn.ADRN_Id
       FROM AD_Arhiva ar
      CROSS APPLY (
          -- extrage al treilea token din numele fișierului (nrAct)
          SELECT TRY_CAST(
              SUBSTRING(ar.ADAH_NumeFisier,
                  -- start: după al doilea '_'
                  CHARINDEX('_', ar.ADAH_NumeFisier,
                      CHARINDEX('_', ar.ADAH_NumeFisier) + 1) + 1,
                  -- lungime: până la al treilea '_'
                  CHARINDEX('_', ar.ADAH_NumeFisier,
                      CHARINDEX('_', ar.ADAH_NumeFisier,
                          CHARINDEX('_', ar.ADAH_NumeFisier) + 1) + 1)
                  - CHARINDEX('_', ar.ADAH_NumeFisier,
                      CHARINDEX('_', ar.ADAH_NumeFisier) + 1) - 1
              ) AS INT
          ) AS nrAct
      ) tok
      INNER JOIN (
          SELECT ADRN_Id, ADRN_CUCO_Id,
                 ROW_NUMBER() OVER (PARTITION BY ADRN_CUCO_Id ORDER BY ADRN_Numar) AS rn
            FROM AD_RegistruNumereInterne
           WHERE ADRN_ADTD_Id = 6
      ) rn ON rn.ADRN_CUCO_Id = ar.ADAH_CUCO_Id
           AND rn.rn = tok.nrAct
      WHERE ar.ADAH_ADRN_Id IS NULL
        AND ar.ADAH_NumeFisier LIKE 'ActAditional[_]%'
        AND tok.nrAct IS NOT NULL
 ) sub ON sub.ADAH_Id = AD_Arhiva.ADAH_Id;

-- Verificare rezultate
SELECT ADAH_Id, ADAH_NumeFisier, ADAH_ADRN_Id, ADAH_CUCO_Id
  FROM AD_Arhiva
 WHERE ADAH_ADRN_Id IS NULL
   AND ADAH_CUCO_Id IS NOT NULL
   AND ADAH_NumeFisier LIKE 'Act%';
