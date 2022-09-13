using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AxaFrance.Extensions.DependencyInjection.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Data;
using TodoApp.Providers;

namespace TodoApp;

#nullable enable

public class MvcApplication : System.Web.HttpApplication
{
    private IDisposable? _provider;

    protected void Application_Start()
    {

        AreaRegistration.RegisterAllAreas();
        RouteConfig.RegisterRoutes(RouteTable.Routes);

        // グローバルフィルターの登録
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

        // EF 初期化処理
        Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoDbContext, TodoApp.Migrations.Configuration>());

        // DI コンテナサービスの作成と登録
        var provider = new ServiceCollection()
            .AddMvc()
            .AddSingleton<TodoDbContext>()
            .AddScoped(_ =>
            {
                return Membership.Providers[nameof(TodoAppMembershipProvider)] switch
                {
                    TodoAppMembershipProvider m => m,
                    _ => throw new NotSupportedException()
                };
            })
            .AddScoped<TodoAppRoleProvider>()
            .BuildServiceProvider();

        DependencyResolver.SetResolver(new DefaultDependencyResolver(provider));

        _provider = provider;
    }

    protected void Application_End()
    {
        // サービスプロバイダの破棄
        _provider?.Dispose();
    }
}
