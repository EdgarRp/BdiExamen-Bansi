using apiexamen.Data.Models;
using apiexamen.Data.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apiexamen.Data.Repositories {
    public class StoredProcedureRepository : IStoredProcedureRepository {

        public StoredProcedureRepository() {
            
        }

        /// <summary>
        /// Execute Stored Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName">Store Procedure Name</param>
        /// <param name="parameters">Params</param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, object>>?> ExecuteStoredProcedureAsync(string storedProcedureName, Dictionary<string, object> parameters, string connetion ) {
            var results = new List<Dictionary<string, object>>();
            try {
                var conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString ?? connetion;
                using( var connection = new SqlConnection( conn ) )
                using( var command = new SqlCommand( storedProcedureName, connection ) ) {
                    command.CommandType = CommandType.StoredProcedure;
                    if( parameters != null ) {
                        foreach( var param in parameters ) {
                            command.Parameters.AddWithValue( param.Key, param.Value );
                        }
                    }
                    await connection.OpenAsync();

                    using( var reader = await command.ExecuteReaderAsync() ) {
                        while( await reader.ReadAsync() ) {
                            var row = new Dictionary<string, object>();

                            for( int i = 0; i < reader.FieldCount; i++ ) {
                                string columnName = reader.GetName( i );
                                object value =  reader.GetValue( i );
                                row[columnName] = value!;
                            }
                            results.Add( row );
                        }
                    }
                    return results;

                }
            } catch( Exception ) {
                return null;
            }

        }

        /// <summary>
        /// Parse Dictionary to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">Data row</param>
        /// <returns></returns>
        public T ParseToObject<T>( Dictionary<string, object>? row ) where T : new() {
            T obj = new T();
            foreach( var (property, value) in 
            from PropertyInfo property 
                    in typeof( T ).GetProperties()
                                              
            where row!.ContainsKey( property.Name )
                let value = row![property.Name]
            select (property, value) ) {
                property.SetValue( obj, value );
            }

            return obj;
        }


        /// <summary>
        /// Parse Dictionary List to Object List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dynamicList"></param>
        /// <returns></returns>
        public List<T> ParseToList<T>( List<Dictionary<string, object>> dynamicList ) where T : new() {
            var list = new List<T>();

            foreach( var row in dynamicList ) {
                T obj = ParseToObject<T>( row );
                list.Add( obj );
            }

            return list;
        }
    }
}
