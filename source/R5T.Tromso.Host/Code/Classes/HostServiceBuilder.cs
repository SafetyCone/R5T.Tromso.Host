using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace R5T.Tromso.Host
{
    public class HostServiceBuilder : IServiceBuilder<IHost>
    {
        private List<Action<IConfigurationBuilder, IServiceProvider>> ConfigureConfigurationActions { get; } = new List<Action<IConfigurationBuilder, IServiceProvider>>();
        private List<Action<IServiceCollection>> ConfigureServicesActions { get; } = new List<Action<IServiceCollection>>();
        private List<Action<IServiceProvider>> ConfigureActions { get; } = new List<Action<IServiceProvider>>();

        private IHostBuilder HostBuilder { get; }
        private IHost Host { get; set; }


        public HostServiceBuilder(IHostBuilder hostBuilder)
        {
            this.HostBuilder = hostBuilder;
        }

        public void AddConfigureConfiguration(Action<IConfigurationBuilder, IServiceProvider> configureConfigurationAction)
        {
            this.ConfigureConfigurationActions.Add(configureConfigurationAction);
        }

        public void AddConfigureServices(Action<IServiceCollection> configureServicesAction)
        {
            this.ConfigureServicesActions.Add(configureServicesAction);
        }

        public void AddConfigure(Action<IServiceProvider> configureAction)
        {
            this.ConfigureActions.Add(configureAction);
        }

        public void Build(IServiceProvider configurationServiceProvider)
        {
            this.Host = this.HostBuilder.Build();

            foreach (var action in this.ConfigureActions)
            {
                action(this.Host.Services);
            }
        }

        public IHost GetResult()
        {
            return this.Host;
        }
    }
}
