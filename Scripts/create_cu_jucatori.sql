-- ============================================================
-- Introduce CU_Jucatori - tabel central de jucatori
-- Refactorizare CU_TurneeDetaliu si CU_TurneeJocuri
-- Ruleaza DUPA create_cu_turnee.sql + alter_cutu_turnee_v2.sql
-- ATENTIE: presupune CU_TurneeDetaliu si CU_TurneeJocuri goale
-- ============================================================

-- 1. Tabel central de jucatori
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('CU_Jucatori') AND type = 'U')
BEGIN
    CREATE TABLE CU_Jucatori (
        CUJA_Id          INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_CU_Jucatori PRIMARY KEY,
        CUJA_FideId      INT NULL,
        CUJA_Nume        NVARCHAR(100) NOT NULL,
        CUJA_Prenume     NVARCHAR(100) NOT NULL CONSTRAINT DF_CUJA_Prenume DEFAULT '',
        CUJA_Fed         NCHAR(3) NULL,
        CUJA_RatingUltim INT NULL,
        CUJA_CUCU_Id     INT NULL,
        CONSTRAINT FK_CU_Jucatori_Cursant FOREIGN KEY (CUJA_CUCU_Id) REFERENCES CU_Cursanti(CUCU_Id)
    );
    -- index unic partial pe FIDE ID (NULL permis, dar daca exista trebuie sa fie unic)
    CREATE UNIQUE INDEX UQ_CU_Jucatori_FideId ON CU_Jucatori (CUJA_FideId) WHERE CUJA_FideId IS NOT NULL;
    CREATE INDEX IX_CU_Jucatori_Nume ON CU_Jucatori (CUJA_Nume, CUJA_Prenume);
    PRINT 'CU_Jucatori creat.';
END
ELSE PRINT 'CU_Jucatori exista deja.';

-- 2. Refactorizare CU_TurneeDetaliu
--    inlocuieste CUTD_CUCU_Id + CUTD_NumeImport + CUTD_FideId cu CUTD_CUJA_Id

-- drop FK vechi spre CU_Cursanti
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_CU_TurneeDetaliu_Cursant')
    ALTER TABLE CU_TurneeDetaliu DROP CONSTRAINT FK_CU_TurneeDetaliu_Cursant;

-- drop index pe CUTD_CUCU_Id daca exista
IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'IX_CU_TurneeDetaliu_Cursant')
    DROP INDEX IX_CU_TurneeDetaliu_Cursant ON CU_TurneeDetaliu;

-- drop coloane vechi (doar daca tabelul e gol - altfel migrare manuala)
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_CUCU_Id')
    ALTER TABLE CU_TurneeDetaliu DROP COLUMN CUTD_CUCU_Id;
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_NumeImport')
    ALTER TABLE CU_TurneeDetaliu DROP COLUMN CUTD_NumeImport;
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_FideId')
    ALTER TABLE CU_TurneeDetaliu DROP COLUMN CUTD_FideId;

-- adauga CUTD_CUJA_Id
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_CUJA_Id')
    ALTER TABLE CU_TurneeDetaliu ADD CUTD_CUJA_Id INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_CU_TurneeDetaliu_Jucator')
    ALTER TABLE CU_TurneeDetaliu ADD CONSTRAINT FK_CU_TurneeDetaliu_Jucator
        FOREIGN KEY (CUTD_CUJA_Id) REFERENCES CU_Jucatori(CUJA_Id);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'IX_CU_TurneeDetaliu_Jucator')
    CREATE INDEX IX_CU_TurneeDetaliu_Jucator ON CU_TurneeDetaliu (CUTD_CUJA_Id);

PRINT 'CU_TurneeDetaliu refactorizat.';

-- 3. Refactorizare CU_TurneeJocuri
--    inlocuieste CUTJ_CUCU_Id_Adv + CUTJ_NumeAdversar + CUTJ_FedAdv cu CUTJ_CUJA_Id_Adv
--    pastreaza CUTJ_RatingAdv  (ratingul adversarului LA MOMENTUL turneului)
--    pastreaza CUTJ_NrStartAdv (util pentru matching si afisare)

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_CU_TurneeJocuri_Cursant')
    ALTER TABLE CU_TurneeJocuri DROP CONSTRAINT FK_CU_TurneeJocuri_Cursant;
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeJocuri') AND name = 'CUTJ_CUCU_Id_Adv')
    ALTER TABLE CU_TurneeJocuri DROP COLUMN CUTJ_CUCU_Id_Adv;
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeJocuri') AND name = 'CUTJ_NumeAdversar')
    ALTER TABLE CU_TurneeJocuri DROP COLUMN CUTJ_NumeAdversar;
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeJocuri') AND name = 'CUTJ_FedAdv')
    ALTER TABLE CU_TurneeJocuri DROP COLUMN CUTJ_FedAdv;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeJocuri') AND name = 'CUTJ_CUJA_Id_Adv')
    ALTER TABLE CU_TurneeJocuri ADD CUTJ_CUJA_Id_Adv INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_CU_TurneeJocuri_JucatorAdv')
    ALTER TABLE CU_TurneeJocuri ADD CONSTRAINT FK_CU_TurneeJocuri_JucatorAdv
        FOREIGN KEY (CUTJ_CUJA_Id_Adv) REFERENCES CU_Jucatori(CUJA_Id);

PRINT 'CU_TurneeJocuri refactorizat.';
PRINT 'Schema CU_Jucatori aplicata cu succes.';
