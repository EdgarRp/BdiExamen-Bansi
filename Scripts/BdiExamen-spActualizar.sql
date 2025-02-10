USE BdiExamen
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'spActualizar')
	DROP PROCEDURE dbo.spActualizar
GO

/**********************************************/
/* Nombre Fisico: BdiExamen-spActualizar      */
/* Autor: Edgar Roque                         */
/* Fecha: 07/02/2025                          */
/* Proyecto: BdiExamen                        */
/* Descripción: Actualizar un registro en la  */
/*              tabla tblexamen.			  */
/**********************************************/
CREATE PROCEDURE [spActualizar]
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

		--Exists 
		IF EXISTS (SELECT 1 FROM tblExamen WHERE idExamen = @Id)
			BEGIN
				--Update examen
				UPDATE tblExamen SET Nombre = @Nombre, Descripcion = @Descripcion
				WHERE idExamen = @Id

				--Save success response
				INSERT INTO @Response 
				SELECT 0, 'Registro actualizado satisfactoriamente';
			END
		ELSE 
		INSERT INTO @Response 
		SELECT -1, 'No se encontró el registro con el ID proporcionado';
		COMMIT;
		
		


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