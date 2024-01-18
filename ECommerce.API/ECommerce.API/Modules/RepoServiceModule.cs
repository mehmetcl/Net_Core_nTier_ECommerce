using Autofac;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.Caching;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Module = Autofac.Module;

namespace ECommerce.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericDal<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();

          

            var apiAssembly=Assembly.GetExecutingAssembly();
            //Üzerinde çalıştığın assembly(API Katmanı)
            var repoAssembly=Assembly.GetAssembly(typeof(ECommerceContext));
            //(data katmanı)repo katmanındaki herhangi bir class
            var serviceAssembly = Assembly.GetAssembly(typeof(CategoryService));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Dal")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
        }
    }
}
