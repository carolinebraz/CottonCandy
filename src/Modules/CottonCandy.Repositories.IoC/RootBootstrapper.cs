using CottonCandy.Repositories.IoC.Application;
using CottonCandy.Repositories.IoC.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CottonCandy.Repositories.IoC
{
    public class RootBootstrapper
    {
        public void RootRegisterServices(IServiceCollection services)
        {
            new ApplicationBootstrapper().ChildServiceRegister(services);
            new RepositoryBootstrapper().ChildServiceRegister(services);
        }
    }
}
