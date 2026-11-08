ALTER TABLE MB_Diviziune
ALTER COLUMN MBDV_Adresa NVARCHAR(max);

UPDATE MB_Diviziune
SET MBDV_CodDiviziune='SB'
WHERE MBDV_Id=1

ALTER TABLE MB_Societate
ADD MBSO_AdresaReprezentant nvarchar(max)

ALTER TABLE CU_Cursanti
ALTER COLUMN CUCU_NumeTutore nvarchar(100) NULL

ALTER TABLE CU_Contracte
ADD CUCO_Listat bit NOT NULL DEFAULT 0;