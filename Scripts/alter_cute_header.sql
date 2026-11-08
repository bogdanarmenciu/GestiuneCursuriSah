-- Coloane antet turneu (import din chess-results crosstable)
ALTER TABLE CU_Turnee ADD
    CUTE_Organizator    NVARCHAR(200) NULL,
    CUTE_DirectorTurneu NVARCHAR(200) NULL,
    CUTE_ArbSef         NVARCHAR(200) NULL,
    CUTE_Cadenta        NVARCHAR(100) NULL,
    CUTE_TipSistem      NVARCHAR(100) NULL;
