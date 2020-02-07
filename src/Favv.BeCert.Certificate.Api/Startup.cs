using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CQRSlite.Caching;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using CQRSlite.Messages;
using CQRSlite.Queries;
using CQRSlite.Routing;
using Favv.BeCert.Certificate.Domain.WriteModel.Handlers;
using Favv.BeCert.Certificate.Domain.WriteModel.Infrastructure;
using Favv.Bricks.RabbitMQAdapter;
using Favv.Bricks.SharedCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ISession = CQRSlite.Domain.ISession;

namespace Favv.BeCert.Certificate.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            Configuration = configuration;

            // Using Serilog
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "Serilog-{Date}.txt"))
               .CreateLogger();

            Log.Information("Inside Startup ctor");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            //Add Cqrs services
            services.AddSingleton<Router>(new Router());
            services.AddSingleton<ICommandSender>(y => y.GetService<Router>());
            services.AddSingleton<IEventPublisher>(y => y.GetService<Router>());
            services.AddSingleton<IHandlerRegistrar>(y => y.GetService<Router>());
            services.AddSingleton<IQueryProcessor>(y => y.GetService<Router>());
            services.AddSingleton<IEventStore, CertificateItemInMemoryEventStore>();
            services.AddSingleton<ICache, MemoryCache>();
            services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            services.AddScoped<ISession, Session>();

            //Scan for commandhandlers and eventhandlers
            services.Scan(scan => scan
                .FromAssemblies(typeof(CertificateItemCommandHandlers).GetTypeInfo().Assembly)
                    .AddClasses(classes => classes.Where(x =>
                    {
                        var allInterfaces = x.GetInterfaces();
                        return
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IHandler<>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableHandler<>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IQueryHandler<,>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableQueryHandler<,>));
                    }))
                    .AsSelf()
                    .WithTransientLifetime()
            );

            // Add message bus services
            services.AddSingleton<InterProcessBus>(new InterProcessBus());
            services.AddSingleton<IInterProcessBus<string>>(y => y.GetService<InterProcessBus>());

            //Add framework services
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Register routes
            services.AddHttpContextAccessor(); // No longer registered by default in ASP.NET Core 2.1
            var serviceProvider = services.BuildServiceProvider();
            var registrar = new RouteRegistrar(new Provider(serviceProvider));
            registrar.RegisterInAssemblyOf(typeof(CertificateItemCommandHandlers));

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    //This makes scoped services work inside router.
    public class Provider : IServiceProvider
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _contextAccessor;

        public Provider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _contextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
        }

        public object GetService(Type serviceType)
        {
            return _contextAccessor?.HttpContext?.RequestServices.GetService(serviceType) ??
                   _serviceProvider.GetService(serviceType);
        }
    }
}
