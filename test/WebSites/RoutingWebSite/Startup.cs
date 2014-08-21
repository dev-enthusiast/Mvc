﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

namespace RoutingWebSite
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = app.GetTestConfiguration();

            app.UseServices(services =>
            {
                services.AddMvc(configuration);

                services.AddScoped<TestResponseGenerator>();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute",
                                "{area:exists}/{controller}/{action}",
                                new { controller = "Home", action = "Index" });

                routes.MapRoute("ActionAsMethod", "{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    "products",
                    "api/Products/{country}/{action}",
                    defaults: new { controller = "Products" });

                // Added this route to validate that we throw an exception when a conventional
                // route matches a link generated by a named attribute route.
                // The conventional route will match first, but when the attribute route generates
                // a valid route an exception will be thrown.
                routes.MapRoute(
                    "DuplicateRoute",
                    "conventional/Duplicate",
                    defaults: new { controller = "Duplicate", action = "Duplicate" });
            });
        }
    }
}
