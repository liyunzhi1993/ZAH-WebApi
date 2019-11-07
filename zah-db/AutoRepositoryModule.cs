using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;

namespace zah_db
{
    /// <summary>
    /// 注入仓储类模块
    /// </summary>
    public class AutoRepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.Load(new AssemblyName("zah-db"));
            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.Name.EndsWith("Repository"))
                {
                    string implClassName = "zah_db.Repository.Impl." + typeInfo.Name.Substring(1)+"Impl";
                    builder.RegisterType(Type.GetType(implClassName)).As(typeInfo.AsType()).InstancePerLifetimeScope();
                }
            }

        }
    }
}
