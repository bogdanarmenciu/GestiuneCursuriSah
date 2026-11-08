-- Extinde tabelele de turnee cu campuri suplimentare si adauga tabelul de jocuri
-- Ruleaza DUPA create_cu_turnee.sql

-- Adauga data sfarsit si tip turneu la CU_Turnee
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_Turnee') AND name = 'CUTE_DataSfarsit')
    ALTER TABLE CU_Turnee ADD CUTE_DataSfarsit DATE NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_Turnee') AND name = 'CUTE_NrRunde')
    ALTER TABLE CU_Turnee ADD CUTE_NrRunde INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_Turnee') AND name = 'CUTE_ChessResultsUrl')
    ALTER TABLE CU_Turnee ADD CUTE_ChessResultsUrl NVARCHAR(500) NULL;

-- Extinde CU_TurneeDetaliu cu campuri de import
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_NrStart')
    ALTER TABLE CU_TurneeDetaliu ADD CUTD_NrStart INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_RatingLaTurneu')
    ALTER TABLE CU_TurneeDetaliu ADD CUTD_RatingLaTurneu INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_RatingPerformanta')
    ALTER TABLE CU_TurneeDetaliu ADD CUTD_RatingPerformanta INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_NumeImport')
    ALTER TABLE CU_TurneeDetaliu ADD CUTD_NumeImport NVARCHAR(200) NULL; -- numele din fisier, pentru matching

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeDetaliu') AND name = 'CUTD_FideId')
    ALTER TABLE CU_TurneeDetaliu ADD CUTD_FideId INT NULL;

-- Tabel jocuri per runda
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('CU_TurneeJocuri') AND type = 'U')
BEGIN
    CREATE TABLE CU_TurneeJocuri (
        CUTJ_Id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CUTJ_CUTD_Id     INT NOT NULL,                      -- participantul nostru
        CUTJ_Runda       TINYINT NOT NULL,                  -- 1..n
        CUTJ_NrStartAdv  INT NULL,                          -- nr. start adversar (din fisier)
        CUTJ_NumeAdversar NVARCHAR(200) NULL,               -- numele adversarului
        CUTJ_RatingAdv   INT NULL,                          -- ratingul adversarului la turneu
        CUTJ_FedAdv      NCHAR(3) NULL,                     -- federatia adversarului
        CUTJ_CUCU_Id_Adv INT NULL,                          -- daca adversarul e si el cursant la noi
        CUTJ_Culoare     TINYINT NOT NULL,                  -- 1=alb 2=negru
        CUTJ_Rezultat    DECIMAL(2,1) NOT NULL,             -- 1.0 / 0.5 / 0.0
        CONSTRAINT FK_CU_TurneeJocuri_Detaliu  FOREIGN KEY (CUTJ_CUTD_Id)     REFERENCES CU_TurneeDetaliu(CUTD_Id),
        CONSTRAINT FK_CU_TurneeJocuri_Cursant  FOREIGN KEY (CUTJ_CUCU_Id_Adv) REFERENCES CU_Cursanti(CUCU_Id)
    );

    CREATE INDEX IX_CU_TurneeJocuri_Detaliu ON CU_TurneeJocuri (CUTJ_CUTD_Id);
END;

-- FIDE ID pe cursant pentru matching automat
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_Cursanti') AND name = 'CUCU_FideId')
    ALTER TABLE CU_Cursanti ADD CUCU_FideId INT NULL;
