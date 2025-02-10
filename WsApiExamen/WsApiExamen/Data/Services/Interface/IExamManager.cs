using WsApiExamen.Data.Models;

namespace WsApiExamen.Data.Repositories.Interface {

    /// <summary>
    /// Examen Repository interface
    /// Comentarios:
    /// Con esto cumplimos con el CRUD
    /// </summary>
    public interface IExamManager {

        /// <summary>
        /// Add Exam
        /// </summary>
        /// <param name="exam">Exam Entity</param>
        /// <returns>Exam Entity</returns>
        public Task<Exam?> AddExam( Exam exam );

        /// <summary>
        /// Update Exam
        /// </summary>
        /// <param name="exam">Exam Entity</param>
        /// <returns>true if was update else false</returns>
        public Task<bool> UpdateExam( Exam exam );

        /// <summary>
        /// Delete Exam
        /// </summary>
        /// <param name="Id">Id Entity</param>
        /// <returns>true if was delete else false</returns>
        public Task<bool> DeleteExam( int Id );


        /// <summary>
        /// Get Exam by Id
        /// </summary>
        /// <param name="Id">Id Entity</param>
        /// <returns>Exam</returns>
        public Task<Exam?> GetById( int Id );

        /// <summary>
        /// Get Exam List
        /// </summary>
        /// <param name="Name">Exam Name</param>
        /// <param name="Description">Exam Description</param>
        /// <returns></returns>
        public Task<IEnumerable<Exam>> Get( string Name, string Description );
    }
}
