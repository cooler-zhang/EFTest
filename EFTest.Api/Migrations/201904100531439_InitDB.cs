namespace EFTest.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                        StudyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassEntity", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.TeacherEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherEntity_StudentEntity",
                c => new
                    {
                        StudentEntity_Id = c.Int(nullable: false),
                        TeacherEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentEntity_Id, t.TeacherEntity_Id })
                .ForeignKey("dbo.StudentEntity", t => t.StudentEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.TeacherEntity", t => t.TeacherEntity_Id, cascadeDelete: true)
                .Index(t => t.StudentEntity_Id)
                .Index(t => t.TeacherEntity_Id);
            
            CreateTable(
                "dbo.HotelEntity",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductEntity", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TourEntity",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductEntity", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourEntity", "Id", "dbo.ProductEntity");
            DropForeignKey("dbo.HotelEntity", "Id", "dbo.ProductEntity");
            DropForeignKey("dbo.StudentEntity", "Class_Id", "dbo.ClassEntity");
            DropForeignKey("dbo.TeacherEntity_StudentEntity", "TeacherEntity_Id", "dbo.TeacherEntity");
            DropForeignKey("dbo.TeacherEntity_StudentEntity", "StudentEntity_Id", "dbo.StudentEntity");
            DropIndex("dbo.TourEntity", new[] { "Id" });
            DropIndex("dbo.HotelEntity", new[] { "Id" });
            DropIndex("dbo.TeacherEntity_StudentEntity", new[] { "TeacherEntity_Id" });
            DropIndex("dbo.TeacherEntity_StudentEntity", new[] { "StudentEntity_Id" });
            DropIndex("dbo.StudentEntity", new[] { "Class_Id" });
            DropTable("dbo.TourEntity");
            DropTable("dbo.HotelEntity");
            DropTable("dbo.TeacherEntity_StudentEntity");
            DropTable("dbo.ProductEntity");
            DropTable("dbo.TeacherEntity");
            DropTable("dbo.StudentEntity");
            DropTable("dbo.ClassEntity");
        }
    }
}
