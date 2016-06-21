using Autofac;
using Autofac.Integration.Mvc;
using Settlement.Repositories.Abstract;
using Settlement.Repositories.Concrete;
using System.Configuration;


namespace Settlement.Web
{
	public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterControllers(System.Reflection.Assembly.GetExecutingAssembly());

            string connectionString = ConfigurationManager.ConnectionStrings["eQuizDB"].ConnectionString;
            builder.Register(с => new DefaultDataContextSettings(connectionString)).As<IDataContextSettings>();            
        }
    }
}