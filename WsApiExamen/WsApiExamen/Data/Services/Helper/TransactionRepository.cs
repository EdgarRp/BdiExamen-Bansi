using WsApiExamen.Data.Models;
using WsApiExamen.Data.Models.Response;

namespace WsApiExamen.Data.Services.Helper {
    public class TransactionRepository {

        private readonly WsApiExamenDbContext _ctx;

        public TransactionRepository( WsApiExamenDbContext context ) {
            _ctx = context;
        }

        /// <summary>
        /// Execute Transaction Async
        /// Comentarios: Con esto agregamos transaccionalidad a las operaciones
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">Db operation</param>
        /// <returns>true if was save other wise false</returns>
        public async Task<bool> ExecuteTransactionAsync<T>( Func<Task<T>> action ) {
            using var transaction = await _ctx.Database.BeginTransactionAsync();
            try {
                await action();
                await _ctx.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;

            } catch( Exception) {
                await transaction.RollbackAsync();
                return false;
            }
        }

        

    }
}
