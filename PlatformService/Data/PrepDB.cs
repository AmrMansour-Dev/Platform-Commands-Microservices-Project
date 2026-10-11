using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        public static void PrepPopultion(IApplicationBuilder app)
        {
            using(var ServiceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(ServiceScope.ServiceProvider.GetRequiredService<AppDbcontext>());
            }
        }

        private static void SeedData(AppDbcontext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data..");

                context.Platforms.AddRange(
                    new Platform { Name = "Dot Net", Cost = "Free", Publisher = "Microsoft" },
                    new Platform { Name = "SQL Server", Cost = "Free", Publisher = "Microsoft" },
                    new Platform { Name = "Kubernetes", Cost = "Free", Publisher = "Cloud Native Computing Foundation" }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}
