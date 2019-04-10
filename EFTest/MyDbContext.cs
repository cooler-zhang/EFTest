using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    public class MyDbContext : DbContext
    {
        public DbSet<StudentEntity> Students { get; set; }

        public DbSet<TeacherEntity> Teachers { get; set; }

        public DbSet<ClassEntity> Classes { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public IDbSet<TourEntity> Tours { get; set; }

        public IDbSet<HotelEntity> Hotels { get; set; }

        public MyDbContext(string dbName)
            : base(dbName)
        {
        }

        public MyDbContext(Func<string> dbFunc)
            : base(dbFunc.Invoke())
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //关闭表名映射复数
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ClassEntity>()
                .HasMany(a => a.Students)
                .WithRequired(a => a.Class)
                .HasForeignKey(a => a.Class_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<StudentEntity>()
                .HasMany(a => a.Teachers)
                .WithMany(a => a.Students)
                .Map(a => a.ToTable("TeacherEntity_StudentEntity"));

            modelBuilder.Entity<TourEntity>()
                .ToTable("TourEntity");

            modelBuilder.Entity<HotelEntity>()
                .ToTable("HotelEntity");
        }

        public void ExecProcedure(string procedureName)
        {
            if (this.Database.Connection.State == ConnectionState.Closed)
                this.Database.Connection.Open();

            var cmd = Database.Connection.CreateCommand();

            cmd.CommandText = procedureName;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandTimeout = 60 * 1000 * 30;
            var result = cmd.ExecuteNonQuery();
        }
    }
}
