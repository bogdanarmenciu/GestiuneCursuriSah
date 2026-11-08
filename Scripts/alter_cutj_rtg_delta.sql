-- Adauga coloane pentru calculul ELO per runda (calculabile din Crosstable)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeJocuri') AND name = 'CUTJ_WeExpected')
    ALTER TABLE CU_TurneeJocuri ADD CUTJ_WeExpected DECIMAL(6,4) NULL;  -- scor asteptat 0.0000-1.0000

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('CU_TurneeJocuri') AND name = 'CUTJ_RtgDelta')
    ALTER TABLE CU_TurneeJocuri ADD CUTJ_RtgDelta DECIMAL(6,1) NULL;   -- rtg+/- pentru runda asta
