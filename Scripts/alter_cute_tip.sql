-- Adauga tipul turneului (Clasic/Rapid/Blitz/Alte) la nivel de turneu
ALTER TABLE CU_Turnee
    ADD CUTE_Tip TINYINT NULL;   -- 1=Clasic, 2=Rapid, 3=Blitz, 4=Alte; NULL = nespecificat
