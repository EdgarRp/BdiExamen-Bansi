USE BdiExamen
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'spEliminar')
	DROP PROCEDURE dbo.spEliminar
GO

/**********************************************/
/* Nombre Fisico: BdiExamen-spEliminar        */
/* Autor: Edgar Roque                         */
/* Fecha: 07/02/2025                          */
/* Proyecto: BdiExamen                        */
/* Descripción: Elimina un registro en la     */
/*              tabla tblexamen.			  */
/**********************************************/
CREATE PROCEDURE [spEliminar]
	 @Id INT

AS
BEGIN 
	SET NOCOUNT ON;

	DECLARE @Response TABLE (
		code INT,
		message VARCHAR(255)
	);

	BEGIN TRY

		BEGIN TRANSACTION;

		
		--Delete examen
		DELETE FROM tblExamen WHERE idExamen = @Id;

		--Save success response
		INSERT INTO @Response 
		SELECT 0, 'Registro eliminado satisfactoriamente';

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