-- Adaugă coloana ADRN_ValoareSponsorizare la AD_RegistruNumereInterne
-- Reprezintă valoarea totală a contractului de sponsorizare
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'AD_RegistruNumereInterne'
      AND COLUMN_NAME = 'ADRN_ValoareSponsorizare'
)
BEGIN
    ALTER TABLE AD_RegistruNumereInterne
    ADD ADRN_ValoareSponsorizare decimal(10,2) NULL;
    PRINT 'Coloana ADRN_ValoareSponsorizare adaugata cu succes.';
END
ELSE
    PRINT 'Coloana ADRN_ValoareSponsorizare exista deja.';
