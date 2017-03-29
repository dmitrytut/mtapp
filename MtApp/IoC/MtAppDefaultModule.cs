using System;
using MtApp.Data;
using MtApp.Models.Entities;
using MtApp.Repos;
using MtApp.Services;
using MtApp.Settings;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace MtApp.IoC
{
	public class MtAppDefaultModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IApiSettings>().To<ApiSettings>();
			Bind<IDbSettings>().To<DbSettings>();
			Bind<IRestService>().To<RestService>();
			Bind<IKvService>().To<KvService>();
			Bind<IRepository<KeyValue>>().To<KvStorage>()
				.WithConstructorArgument("db", context => context.Kernel.Get<IDbSettings>().Connection);
		}
	}
}
