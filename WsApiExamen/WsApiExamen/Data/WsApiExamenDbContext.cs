using Microsoft.EntityFrameworkCore;
using WsApiExamen.Data.Models;

namespace WsApiExamen.Data {

    /// <summary>
    /// Data Base Context Class
    /// </summary>
    public class WsApiExamenDbContext : DbContext {


        public WsApiExamenDbContext(DbContextOptions<WsApiExamenDbContext> options) : base(options) {
            
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<Exam>().ToTable( "tblExamen" );
            base.OnModelCreating( modelBuilder );
        }

        #region DbSet
        public DbSet<Exam> Exams { get; set; }
        #endregion


    }
}
