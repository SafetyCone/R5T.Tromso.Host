using System;

using Microsoft.Extensions.Hosting;


namespace R5T.Tromso.Host
{
    public static class Host
    {
        public static HostServiceBuilder New()
        {
            var hostServiceBuilder = new HostServiceBuilder(new HostBuilder());
            return hostServiceBuilder;
        }
    }
}
