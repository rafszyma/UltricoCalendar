using Autofac;
using UltricoCalendarCommon;
using IContainer = System.ComponentModel.IContainer;

namespace UltricoCalendarTest
{
    public class BaseTests
    {

            private IContainer _autofacContainer;
            protected IContainer AutofacContainer
            {
                get
                {
                    if (UltricoModule.IoCContainer == null)
                    {
                        var builder = new ContainerBuilder();

                        // Repositories
                        builder.RegisterType<CompanyDataDb>().As<CompanyDataDb>().InstancePerLifetimeScope();

                        // Register the CompanyDataRepository for property injection not constructor allowing circular references
                        builder.RegisterType<CompanyDataRepository>().As<ICompanyDataRepository>()
                            .InstancePerLifetimeScope()
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

                        // Other wireups....

                        var container = builder.Build();

                        _autofacContainer = container;
                    }

                    return _autofacContainer;
                }
            }

            protected I CompanyDataRepository
            {
                get
                {
                    return AutofacContainer.Resolve<ICompanyDataRepository>();
                }
            }

            protected CompanyDataDb CompanyDataDb
            {
                get
                {
                    return AutofacContainer.Resolve<CompanyDataDb>();
                }
            }
       
    }
}