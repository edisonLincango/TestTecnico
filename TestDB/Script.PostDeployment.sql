
DELETE FROM Cola;
DBCC CHECKIDENT ('Cola', RESEED, 0);
INSERT INTO Cola(minutosAtencion) values(2);
INSERT INTO Cola(minutosAtencion) values(3);


