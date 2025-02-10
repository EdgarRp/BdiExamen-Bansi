USE BdiExamen
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'spConsultar')
	DROP PROCEDURE dbo.spConsultar
GO

/**********************************************/
/* Nombre Fisico: BdiExamen-spConsultar       */
/* Autor: Edgar Roque                         */
/* Fecha: 07/02/2025                          */
/* Proyecto: BdiExamen                        */
/* Descripción: Consulta un registro en la    */
/*              tabla tblexamen.			  */
/**********************************************/
CREATE PROCEDURE [spConsultar]
	 @Nombre VARCHAR(255),
	 @Descripcion VARCHAR(255)

AS
BEGIN 
	SET NOCOUNT ON;

	SELECT  
		EX.idExamen 'Id',
		EX.Nombre,
		EX.Descripcion 'Descripción'
	FROM tblExamen ex
	WHERE 
        (@Nombre IS NULL OR LTRIM(RTRIM(EX.Nombre)) = LTRIM(RTRIM(@Nombre)))
        OR (@Descripcion IS NULL OR LTRIM(RTRIM(EX.Descripcion)) = LTRIM(RTRIM(@Descripcion)));
END;
