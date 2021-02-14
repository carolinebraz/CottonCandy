using CottonCandy.Repositories.IoC.Application;
using CottonCandy.Repositories.IoC.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CottonCandy.Repositories.IoC
{
    public class RootBootstraper
    {
        public void RootRegisterServices(IServiceCollection services)
        {
            new ApplicationBootstraper().ChildServiceRegister(services);
            new RepositoryBootstraper().ChildServiceRegister(services);
        }
    }
}
