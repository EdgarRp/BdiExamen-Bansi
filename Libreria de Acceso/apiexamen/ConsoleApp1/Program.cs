

using apiexamen;
using apiexamen.Data.Models;
using System.Configuration;


/// <summary>
/// Definition Class
/// </summary>
public static class Program {

    static async Task Main() {
        var http = new HttpClient();

        var clsExamen = new ClsExamen( http );
        Exam model = new Exam() {
            IdExamen = 0,
            Descripcion = "Desde la DLL",
            Nombre = "Desde la DLL"

        };
        var UseAPI = bool.Parse( ConfigurationManager.AppSettings["UseAPI"] ?? "false" );

        if( UseAPI ) {
            //add Exam
            var added = await clsExamen.AgregarExamen( model );

            //Update
            added!.ResponseData!.Descripcion = "Update from DLL";
            var updated = await clsExamen.ActualizarExamen( added.ResponseData );

            //Delete
            var delete = await clsExamen.EliminarExamen( 1 );

            //Get
            var get = await clsExamen.ConsultarExamen( 1 );
            var getList = await clsExamen.ConsultarExamenes( "Update fix", "Update from DLL" );

            Console.WriteLine( $"Add: {added.IsSuccess}, " +
                $"Update: {updated?.IsSuccess}, " +
                $"Delete: {delete?.IsSuccess}, " +
                $"Get: {get?.IsSuccess}, " +
                $"GetLis: {getList?.ResponseData?.Count}" );

        } else {
            //add Exam
            var added = await clsExamen.AgregarExamen( model );

            //updated
            var updated = await clsExamen.ActualizarExamen( new Exam() { 
                IdExamen = 1,
                Descripcion = "Updated",
                Nombre = "Updated name"
            } );

            //Delete
            var delete = await clsExamen.EliminarExamen( 1 );

            //Get
            var get = await clsExamen.ConsultarExamen( 1 );
            var getList = await clsExamen.ConsultarExamenes( "Updated", "Updated name" );


            Console.WriteLine( $"Add: {added?.IsSuccess}, " +
                $"Update: {updated?.IsSuccess}, " +
                $"Delete: {delete?.IsSuccess}, " +
                $"Get: {get?.IsSuccess}, " +
                $"GetLis: {getList?.ResponseData?.Count}" );

        }
        


    }
}

