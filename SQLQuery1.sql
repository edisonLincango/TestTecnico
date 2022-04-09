SELECT * FROM CLIENTE

--DELETE FROM CLIENTE

SELECT * FROM COLA

SELECT DATEADD(MINUTE,15,GETDATE())

EXEC SP_TURNO


	DECLARE @minutos_atencion1 int;
	DECLARE @minutos_atencion2 int;

	SELECT @minutos_atencion1 = minutosAtencion FROM Cola WHERE numeroCola = 1;
	SELECT @minutos_atencion2 = minutosAtencion FROM Cola WHERE numeroCola = 2;

	SET @minutos_atencion1 = -1*@minutos_atencion1
	SET @minutos_atencion2 = -1*@minutos_atencion2

	SELECT * FROM 
		Cliente
	WHERE
		numeroCola = 1
		AND estado = 'PEN'
		AND horaRegistro < DATEADD(MINUTE,@minutos_atencion1,GETDATE())