-- Adauga tipul documentului de identitate la cursant
-- 1 = Carte de identitate, 2 = Certificat de nastere, 3 = Pasaport
ALTER TABLE CU_Cursanti ADD CUCU_TipDocument TINYINT NULL;
