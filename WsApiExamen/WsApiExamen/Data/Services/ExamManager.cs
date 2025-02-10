using Microsoft.EntityFrameworkCore;
using WsApiExamen.Data.Models;
using WsApiExamen.Data.Repositories.Interface;
using WsApiExamen.Data.Services.Helper;

namespace WsApiExamen.Data.Services {
    /// <summary>
    /// Examen repository class
    /// </summary>
    public class ExamManager : IExamManager {

        private readonly TransactionRepository _repository;
        private readonly WsApiExamenDbContext _ctx;
        public ExamManager(TransactionRepository repository, WsApiExamenDbContext context) {
            _repository = repository;
            _ctx = context;
        }

        /// <summary>
        /// Add Examn
        /// </summary>
        /// <param name="exam">Model</param>
        /// <returns>Exam</returns>
        public async Task<Exam?> AddExam( Exam exam ) {
            var result = await _repository.ExecuteTransactionAsync( async () => {
                await _ctx.Exams.AddAsync( exam );
                return exam;
            } );
            if( result )
                return exam;

            return null;
        }

        /// <summary>
        /// Update Exam
        /// </summary>
        /// <param name="exam">Model</param>
        /// <returns>True if was update else false</returns>
        public async Task<bool> UpdateExam (Exam exam ) {
            var result = await _repository.ExecuteTransactionAsync( () => {
                _ctx.Exams.Update( exam );
                return Task.FromResult( true );
            } );

            return result;
        }

        /// <summary>
        /// Delete Exam
        /// </summary>
        /// <param name="Id">Id Entity</param>
        /// <returns>True if was delete, else false</returns>
        public async Task<bool> DeleteExam(int Id ) {
            var result = await _repository.ExecuteTransactionAsync( async () => {
                var exam = await _ctx.Exams.FindAsync( Id );
                if( exam != null ) 
                    return _ctx.Exams.Remove( exam ).State == EntityState.Deleted;
                
                return false;
            } );
            
            return result;
        }


        /// <summary>
        /// Get Exam by Id
        /// </summary>
        /// <param name="Id">Id Entity</param>
        /// <returns>Exam</returns>
        public async Task<Exam?> GetById(int Id ) {
            return await _ctx.Exams.FindAsync( Id );
        }

        /// <summary>
        /// Get Exam List by Name or Description
        /// </summary>
        /// <param name="Name">Exam Name</param>
        /// <param name="Description">Exam Description</param>
        /// <returns>Exam List</returns>
        public async Task<IEnumerable<Exam>> Get(string Name, string Description ) {
            return await ( from e in _ctx.Exams
                         where e.Nombre!.Equals( Name ) || e.Descripcion!.Equals( Description )
                         select e).ToListAsync();
        }
    }
}
