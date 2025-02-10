USE BdiExamen
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'spConsultarPorId')
	DROP PROCEDURE dbo.spConsultarPorId
GO

/**********************************************/
/* Nombre Fisico: BdiExamen-spConsultarPorId  */
/* Autor: Edgar Roque                         */
/* Fecha: 09/02/2025                          */
/* Proyecto: BdiExamen                        */
/* Descripción: Consulta un registro en la    */
/*              tabla tblexamen.			  */
/**********************************************/
CREATE PROCEDURE [spConsultarPorId]
	 @Id INT

AS
BEGIN 
	SET NOCOUNT ON;

	SELECT  
		EX.idExamen 'Id',
		EX.Nombre,
		EX.Descripcion 'Descripción'
	FROM tblExamen ex
	WHERE EX.idExamen = @Id
END;
