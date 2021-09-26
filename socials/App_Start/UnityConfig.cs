using socials.Controllers;
using socials.Repository;
using socials.Services;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace socials
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());

            // Repositories
            container.RegisterType<IUserRepository, UserRepository>();

            // Services
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IFollowService, FollowService>();
            container.RegisterType<INotificationService, NotificationService>();
            container.RegisterType<IPostService, PostService>();

            return container;
        }
        //     public static void RegisterComponents()
        //     {
        //var container = new UnityContainer();

        //         // register all your components with the container here
        //         // it is NOT necessary to register your controllers

        //         //container.RegisterType<INotificationService, NotificationService>();

        //         DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        //     }
    }
}