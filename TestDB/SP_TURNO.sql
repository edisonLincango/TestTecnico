CREATE PROCEDURE [dbo].[SP_TURNO]
AS
BEGIN

	DECLARE @minutos_atencion1 int;
	DECLARE @minutos_atencion2 int;

	SELECT @minutos_atencion1 = minutosAtencion FROM Cola WHERE numeroCola = 1;
	SELECT @minutos_atencion2 = minutosAtencion FROM Cola WHERE numeroCola = 2;

	SET @minutos_atencion1 = -1*@minutos_atencion1
	SET @minutos_atencion2 = -1*@minutos_atencion2

	UPDATE 
		Cliente
	SET
		estado = 'FIN'
	WHERE
		numeroCola = 1
		AND estado = 'PEN'
		AND horaRegistro < DATEADD(MINUTE,@minutos_atencion1,GETDATE())
		
	UPDATE 
		Cliente
	SET
		estado = 'FIN'
	WHERE
		numeroCola = 2
		AND estado = 'PEN'
		AND horaRegistro < DATEADD(MINUTE,@minutos_atencion2,GETDATE())

END