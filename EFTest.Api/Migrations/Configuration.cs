namespace EFTest.Api.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFTest.Api.MigrationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EFTest.Api.MigrationDbContext context)
        {
            context.Classes.AddRange(
                new List<ClassEntity>()
                {
                    new ClassEntity {
                        Name = "小班"
                    },
                    new ClassEntity{
                        Name = "中班"
                    },
                    new ClassEntity(){
                        Name = "中班"
                    }
                });
            context.SaveChanges();
        }
    }
}
