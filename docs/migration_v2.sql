-- ============================================================
-- Migration v2 — Registru numere interne + Abonamente
-- Idempotent: poate fi rulat de oricâte ori fără efecte secundare
-- Baze de date: Onixteia, OnixteiaTest
-- ============================================================

-- ============================================================
-- 1. Tipuri de documente pentru registrul de numere interne
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('AD_TipuriDocumenteNumarIntern'))
BEGIN
    CREATE TABLE AD_TipuriDocumenteNumarIntern (
        ADTD_Id                 INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        ADTD_Denumire           VARCHAR(200)      NOT NULL,
        ADTD_EsteIesireImplicit BIT               NOT NULL DEFAULT 1,
        ADTD_Activ              BIT               NOT NULL DEFAULT 1,
        ADTD_OrdineAfisare      INT               NOT NULL DEFAULT 0
    );
END;

-- Inserăm tipurile lipsă (ADTD_Id=1 rezervat pentru "Contract cursant" în cod)
SET IDENTITY_INSERT AD_TipuriDocumenteNumarIntern ON;

IF NOT EXISTS (SELECT 1 FROM AD_TipuriDocumenteNumarIntern WHERE ADTD_Id = 1)
    INSERT INTO AD_TipuriDocumenteNumarIntern (ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit, ADTD_Activ, ADTD_OrdineAfisare)
    VALUES (1, 'Contract cursant', 1, 1, 1);

IF NOT EXISTS (SELECT 1 FROM AD_TipuriDocumenteNumarIntern WHERE ADTD_Id = 2)
    INSERT INTO AD_TipuriDocumenteNumarIntern (ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit, ADTD_Activ, ADTD_OrdineAfisare)
    VALUES (2, 'Adeverinta', 1, 1, 2);

IF NOT EXISTS (SELECT 1 FROM AD_TipuriDocumenteNumarIntern WHERE ADTD_Id = 3)
    INSERT INTO AD_TipuriDocumenteNumarIntern (ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit, ADTD_Activ, ADTD_OrdineAfisare)
    VALUES (3, 'Cerere inscriere', 0, 1, 3);

IF NOT EXISTS (SELECT 1 FROM AD_TipuriDocumenteNumarIntern WHERE ADTD_Id = 4)
    INSERT INTO AD_TipuriDocumenteNumarIntern (ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit, ADTD_Activ, ADTD_OrdineAfisare)
    VALUES (4, 'Proces verbal', 1, 1, 4);

IF NOT EXISTS (SELECT 1 FROM AD_TipuriDocumenteNumarIntern WHERE ADTD_Id = 5)
    INSERT INTO AD_TipuriDocumenteNumarIntern (ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit, ADTD_Activ, ADTD_OrdineAfisare)
    VALUES (5, 'Document intern', 1, 1, 5);

IF NOT EXISTS (SELECT 1 FROM AD_TipuriDocumenteNumarIntern WHERE ADTD_Id = 6)
    INSERT INTO AD_TipuriDocumenteNumarIntern (ADTD_Id, ADTD_Denumire, ADTD_EsteIesireImplicit, ADTD_Activ, ADTD_OrdineAfisare)
    VALUES (6, 'Act aditional', 1, 1, 6);

SET IDENTITY_INSERT AD_TipuriDocumenteNumarIntern OFF;


-- ============================================================
-- 2. Secvență numere interne
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('AD_SecventaNumarIntern'))
BEGIN
    CREATE TABLE AD_SecventaNumarIntern (
        ADSN_Id        INT NOT NULL PRIMARY KEY,
        ADSN_UltimulNr INT NOT NULL DEFAULT 0
    );
END;

IF NOT EXISTS (SELECT 1 FROM AD_SecventaNumarIntern WHERE ADSN_Id = 1)
    INSERT INTO AD_SecventaNumarIntern (ADSN_Id, ADSN_UltimulNr) VALUES (1, 0);


-- ============================================================
-- 3. Registrul de numere interne
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('AD_RegistruNumereInterne'))
BEGIN
    CREATE TABLE AD_RegistruNumereInterne (
        ADRN_Id                        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        ADRN_Numar                     INT          NOT NULL,
        ADRN_Data                      DATETIME     NOT NULL,
        ADRN_ADTD_Id                   INT          NOT NULL,
        ADRN_Iesire                    BIT          NOT NULL DEFAULT 1,
        ADRN_MBDV_Id                   INT          NOT NULL,
        ADRN_CUCO_Id                   INT          NULL,
        ADRN_Descriere                 VARCHAR(MAX) NOT NULL,
        ADRN_Observatii                VARCHAR(MAX) NULL,
        ADRN_MBUT_Id                   INT          NULL,
        ADRN_DataSalvarii              DATETIME     NULL,
        ADRN_MBUT_Id_UltimeiModificari INT          NULL,
        ADRN_DataUltimeiModificari     DATETIME     NULL
    );
END;


-- ============================================================
-- 4. Arhivă — coloană legătură cu registrul
-- ============================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('AD_Arhiva') AND name = 'ADAH_ADRN_Id'
)
    ALTER TABLE AD_Arhiva ADD ADAH_ADRN_Id INT NULL;


-- ============================================================
-- 5. Istoricul perioadelor de abonament per contract
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('CU_AbonamenteCursant'))
BEGIN
    CREATE TABLE CU_AbonamenteCursant (
        CUAC_Id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CUAC_CUCO_Id      INT          NOT NULL,
        CUAC_CUAB_Id      INT          NOT NULL,
        CUAC_DataInceput  DATETIME     NOT NULL,
        CUAC_DataSfarsit  DATETIME     NOT NULL,
        CUAC_Pret         DECIMAL(9,2) NOT NULL,
        CUAC_MBUT_Id      INT          NULL,
        CUAC_DataSalvarii DATETIME     NULL
    );
END;


-- ============================================================
-- 6. Încasări — coloană legătură cu contractul
-- ============================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CU_Incasari') AND name = 'CUIN_CUCO_Id'
)
    ALTER TABLE CU_Incasari ADD CUIN_CUCO_Id INT NULL;


-- ============================================================
-- 7. CU_Contracte — face CUCO_CUAB_Id și CUCO_Pret nullable
--    (sursa de adevăr pentru abonamente este acum CU_AbonamenteCursant)
-- ============================================================
IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CU_Contracte') AND name = 'CUCO_CUAB_Id'
      AND is_nullable = 0
)
    ALTER TABLE CU_Contracte ALTER COLUMN CUCO_CUAB_Id INT NULL;

IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CU_Contracte') AND name = 'CUCO_Pret'
      AND is_nullable = 0
)
    ALTER TABLE CU_Contracte ALTER COLUMN CUCO_Pret DECIMAL(9,2) NULL;


-- ============================================================
-- 8. Migrare date istorice: prima perioadă din fiecare contract vechi
--    → CU_AbonamenteCursant (doar pentru contracte fără înregistrări deja)
-- ============================================================

-- Corectează datele deja migrate: data_sfarsit = data_sfarsit - 1 zi
-- (convenție nouă: ultima zi inclusivă, nu prima zi din perioada următoare)
-- Rulează doar pe rânduri care provin din migrarea inițială (sursa = CU_Contracte)
UPDATE ac
SET    ac.CUAC_DataSfarsit = DATEADD(day, -1, ac.CUAC_DataSfarsit)
FROM   CU_AbonamenteCursant ac
INNER JOIN CU_Contracte co ON co.CUCO_Id = ac.CUAC_CUCO_Id
WHERE  ac.CUAC_DataSfarsit = co.CUCO_DataSfarsitContract;  -- potrivire exactă = migrat cu formula veche

INSERT INTO CU_AbonamenteCursant
    (CUAC_CUCO_Id, CUAC_CUAB_Id, CUAC_DataInceput, CUAC_DataSfarsit, CUAC_Pret, CUAC_DataSalvarii)
SELECT
    CUCO_Id,
    CUCO_CUAB_Id,
    CUCO_DataInceputContract,
    DATEADD(day, -1, CUCO_DataSfarsitContract),   -- converti la ultima zi inclusivă
    ISNULL(CUCO_Pret, 0),
    GETDATE()
FROM CU_Contracte
WHERE CUCO_CUAB_Id IS NOT NULL
  AND NOT EXISTS (
      SELECT 1 FROM CU_AbonamenteCursant WHERE CUAC_CUCO_Id = CUCO_Id
  );

-- ============================================================
-- Gata. Verificare rapidă:
-- ============================================================
SELECT 'AD_TipuriDocumenteNumarIntern' AS Tabel, COUNT(*) AS Randuri FROM AD_TipuriDocumenteNumarIntern
UNION ALL
SELECT 'AD_SecventaNumarIntern',  COUNT(*) FROM AD_SecventaNumarIntern
UNION ALL
SELECT 'AD_RegistruNumereInterne', COUNT(*) FROM AD_RegistruNumereInterne
UNION ALL
SELECT 'CU_AbonamenteCursant',    COUNT(*) FROM CU_AbonamenteCursant;
