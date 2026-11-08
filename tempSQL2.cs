EXEC sp_rename 'MB_Societate.MBSO_Strada', 'MBSO_StradaPrincipala', 'COLUMN';
EXEC sp_rename 'MB_Societate.MBSO_NumarStrada', 'MBSO_NumarStradaPrincipala', 'COLUMN';
EXEC sp_rename 'MB_Societate.MBSO_Apartament', 'MBSO_ApartamentPrincipal', 'COLUMN';


ALTER TABLE MB_Societate
ALTER COLUMN MBSO_Descriere NVARCHAR(MAX);

ALTER TABLE MB_Societate
ADD MBSO_StradaSecundara NVARCHAR(MAX);

ALTER TABLE MB_Societate
ADD MBSO_NumarStradaSecundara VARCHAR(MAX);

ALTER TABLE MB_Societate
ADD MBSO_ApartamentSecundar VARCHAR(MAX);


ALTER TABLE CU_Cursanti 
ALTER COLUMN CUCU_Nume NVARCHAR(100) COLLATE Latin1_General_100_CI_AI_SC_UTF8;

ALTER TABLE CU_Cursanti 
ALTER COLUMN CUCU_Prenume NVARCHAR(100) COLLATE Latin1_General_100_CI_AI_SC_UTF8;

ALTER TABLE CU_Cursanti
ALTER COLUMN CUCU_PrenumeTutore NVARCHAR(100);

ALTER TABLE CU_Cursanti
ALTER COLUMN CUCU_Oras NVARCHAR(50);

ALTER TABLE CU_Cursanti
ALTER COLUMN CUCU_Judet NVARCHAR(50);

ALTER TABLE CU_Cursanti
ALTER COLUMN CUCU_LocalitateaNasterii NVARCHAR(100);

ALTER TABLE CU_Cursanti
ALTER COLUMN CUCU_JudetulNasterii NVARCHAR(100);

ALTER TABLE CU_Cursanti
ALTER COLUMN CUCU_Strada NVARCHAR(100);

ALTER TABLE MB_Judet
ALTER COLUMN MBJU_Judet NVARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC_UTF8 NOT NULL;

ALTER TABLE MB_Localitate
ALTER COLUMN MBLO_Localitate NVARCHAR(100) COLLATE Latin1_General_100_CI_AI_SC_UTF8 NOT NULL;



UPDATE MB_Societate
SET 
    MBSO_Descriere = N'ASOCIAȚIA CLUB SPORTIV ONIXTEIA',
    MBSO_CONT = 'RO65BTRLRONCRT0651805701',
    MBSO_EmailSocietate = 'contact@clubsah.ro',
    MBSO_CUI = '47000449',
    MBSO_CINumarReprezentant = '084156',
    MBSO_NumarStradaPrincipala = '1A',
    MBSO_ApartamentPrincipal = '4',
    MBSO_StradaSecundara = N'Constituției',
    MBSO_NumarStradaSecundara = '3',
    MBSO_ApartamentSecundar = '5'
WHERE MBSO_Id = 1;

UPDATE MB_Judet
SET MBJU_Judet = CASE 
    WHEN MBJU_Id = 106 THEN N'Argeș'
    WHEN MBJU_Id = 107 THEN N'Bacău'
    WHEN MBJU_Id = 109 THEN N'Bistrița'
    WHEN MBJU_Id = 110 THEN N'Botoșani'
    WHEN MBJU_Id = 111 THEN N'Brașov'
    WHEN MBJU_Id = 112 THEN N'Brăila'
    WHEN MBJU_Id = 113 THEN N'Buzău'
    WHEN MBJU_Id = 114 THEN N'Caraș Severin'
    WHEN MBJU_Id = 116 THEN N'Constanța'
    WHEN MBJU_Id = 118 THEN N'Dâmbovița'
    WHEN MBJU_Id = 120 THEN N'Galați'
    WHEN MBJU_Id = 124 THEN N'Ialomița'
    WHEN MBJU_Id = 125 THEN N'Iași'
    WHEN MBJU_Id = 127 THEN N'Maramureș'
    WHEN MBJU_Id = 128 THEN N'Mehedinți'
    WHEN MBJU_Id = 129 THEN N'Mureș'
    WHEN MBJU_Id = 130 THEN N'Neamț'
    WHEN MBJU_Id = 134 THEN N'Sălaj'
    WHEN MBJU_Id = 138 THEN N'Timiș'
    WHEN MBJU_Id = 143 THEN N'Călărași'
    WHEN MBJU_Id = 144 THEN N'București'
    ELSE MBJU_Judet
END
WHERE MBJU_Id IN (106, 107, 109, 110, 111, 112, 113, 114, 116, 118, 120, 124, 125, 127, 128, 129, 130, 134, 138, 143, 144);

DELETE FROM MB_Localitate
DBCC CHECKIDENT ('MB_Localitate', RESEED, 0);

INSERT INTO MB_Localitate (MBLO_CodLocalitate, MBLO_Localitate, MBLO_AbreviereJudet)
SELECT natCode, name, countyMn
FROM date_import

DROP TABLE date_import;