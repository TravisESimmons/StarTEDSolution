#region Additional Namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarTEDSystem.BLL;
using StarTEDSystem.DAL;
#endregion


namespace StarTEDSystem
{
    public static class STBackendExtensions
    {
        public static void AddBackendDependencies(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            //  Within this method, we will register all the services that will
            //      be used by the system (context setup) and will be provided by
            //      the system.
            services.AddDbContext<StarTEDContext>(options);

            //  register the service classes

            //  add any business logic layer class to the service collection so our 
            //      web app has access to the methods (services) within the BLL class

            //  The argument for the AddsTransient is called a factory
            //  Basically what you are adding is a localize method
            services.AddTransient<EmployeeServices>((ServiceProvider) =>
            {
                //  get the dbcontext class that has been registered
                var context = ServiceProvider.GetService<StarTEDContext>();
                //  create an instance of the service class (BuidVersionServices) supplying
                //      the context reference to the service class
                //  return the service class instance
                return new EmployeeServices(context);
            }
             );
            services.AddTransient<ProgramServices>((ServiceProvider) =>
            {
                //  get the dbcontext class that has been registered
                var context = ServiceProvider.GetService<StarTEDContext>();
                //  create an instance of the service class (RegionServices) supplying
                //      the context reference to the service class
                //  return the service class instance
                return new ProgramServices(context);
            }
             );
            services.AddTransient<PositionServices>((ServiceProvider) =>
            {
                //  get the dbcontext class that has been registered
                var context = ServiceProvider.GetService<StarTEDContext>();
                //  create an instance of the service class (TerritoryServices) supplying
                //      the context reference to the service class
                //  return the service class instance
                return new PositionServices(context);
            }
            );
        }
    }
}
