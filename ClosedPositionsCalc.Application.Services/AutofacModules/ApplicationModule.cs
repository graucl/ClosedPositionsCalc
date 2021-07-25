using Autofac;
using ClosedPositionsCalc.Application.Services.Contracts;
using ClosedPositionsCalc.Application.Services.Implementation;
using ClosedPositionsCalc.Infrastructure.Repository.Contracts;
using ClosedPositionsCalc.Infrastructure.Repository.Implementation;

namespace ClosedPositionsCalc.Application.Services.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IncomeAppService>().As<IIncomeAppService>().InstancePerDependency();
            builder.RegisterType<IncomeRepository>().As<IIncomeRepository>().InstancePerDependency();

            base.Load(builder);
        }
    }
}
