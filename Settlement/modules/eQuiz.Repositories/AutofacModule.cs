using Autofac;
using Settlement.Repositories.Abstract;
using Settlement.Repositories.Concrete;

namespace Settlement.Repositories
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFDataContextFactory>().As<IDataContextFactory>();
            builder.RegisterType<EFRepository>().As<IRepository>();                                   
        }
    }
}
