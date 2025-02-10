USE BdiExamen
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'spAgregar')
	DROP PROCEDURE dbo.spAgregar
GO

/**********************************************/
/* Nombre Fisico: BdiExamen-spAgregar         */
/* Autor: Edgar Roque                         */
/* Fecha: 07/02/2025                          */
/* Proyecto: BdiExamen                        */
/* Descripción: Insertar un registro en la    */
/*              tabla tblexamen.			  */
/**********************************************/
CREATE PROCEDURE [spAgregar]
	 @Id INT,
	 @Nombre VARCHAR(255),
	 @Descripcion VARCHAR(255)

AS
BEGIN 
	SET NOCOUNT ON;

	DECLARE @Response TABLE (
		code INT,
		message VARCHAR(255)
	);

	BEGIN TRY

		BEGIN TRANSACTION;
		--Insert examen
		INSERT INTO tblExamen (Nombre, Descripcion)
		VALUES (@Nombre, @Descripcion)

		COMMIT;
		
		--Save success response
		INSERT INTO @Response 
		SELECT 0, 'Registro insertado satisfactoriamente';


		--Return response
		SELECT * FROM @Response


	END TRY
	BEGIN CATCH
		ROLLBACK;

		--Save error response
		INSERT INTO @Response 
		SELECT ISNULL(ERROR_NUMBER(), -1), ERROR_MESSAGE();

		--Return error response
		SELECT * FROM @Response


	END CATCH
END;
GO