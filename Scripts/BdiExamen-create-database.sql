/**********************************************/
/* Nombre Fisico: BdiExamen-create-database   */
/* Autor: Edgar Roque                         */
/* Fecha: 07/02/2025                          */
/* Proyecto: Examen básico de seleccion Bansi */
/* Descripción: Examen Técnico de selección   */
/**********************************************/

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BdiExamen')
BEGIN
	CREATE DATABASE BdiExamen;
END
GO

USE BdiExamen
GO

BEGIN TRANSACTION
	BEGIN TRY

		--Create table tblExamen
		IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tblExamen')
		BEGIN
			CREATE TABLE tblExamen (
				idExamen INT IDENTITY(1,1) PRIMARY KEY,
				Nombre VARCHAR(255) NULL,
				Descripcion VARCHAR(255) NULL
			);

		END

		--Commit transaction
		COMMIT;
		PRINT 'Bdi Created'
	END TRY

BEGIN CATCH
	ROLLBACK;
	PRINT 'Error the transacation was fail: ' + ERROR_MESSAGE();
END CATCH
GO



