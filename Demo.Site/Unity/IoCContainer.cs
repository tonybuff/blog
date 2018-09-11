using Demo.Core.Data.DbContext;
using Demo.Core.Data.Repository;
using Demo.Core.Data.Repository.Imp;
using Demo.Site.Adapter;
using Microsoft.Practices.Unity;
using AutoMapper;

namespace Demo.Site.Unity
{
    /// <summary>
    /// IoC容器
    /// </summary>
     public static class IoCContainer
    {
         #region 属性
        static IUnityContainer _currentContainer;

        /// <summary>
        /// Get the current configured container
        /// </summary>
        /// <returns>Configured container</returns>
        public static IUnityContainer Current
        {
            get
            {
                return _currentContainer;
            }
        }
         #endregion

         /// <summary>
         /// 构造器
         /// </summary>
        static IoCContainer()
        {
            ConfigureContainer();
            ConfigureFactoty();
        }

         /// <summary>
         /// 注入容器
         /// </summary>
        private static void ConfigureContainer()
        {
            _currentContainer=new UnityContainer();
            //DbContext,测试后不配置dbcontext的注入也可以正常使用
             _currentContainer.RegisterType(typeof (DemoDbContext), new PerResolveLifetimeManager());
            //Service,IService
            _currentContainer.RegisterType<IMemberRepository,MemberRepository>();
            _currentContainer.RegisterType<ILoginLogRepository,LoginLogRepository>();
            _currentContainer.RegisterType<IConsumeRecordRepository, ConsumeRecordRepository>();

        }

        private static void ConfigureFactoty()
        {
            Mapper.Configuration.AddProfile(new CommonProfile());
        }
     }
}
