//
// Meditation Logger.
// Copyright (C) 2017  Seth Hendrick.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using ElectronNET.API;
using ElectronNET.API.Entities;
using MeditationLogger.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MeditationLogger.Gui
{
    public class Startup
    {
        private MeditationLoggerApi api;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime )
        {
            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc( 
                routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}" );
                } 
            );

            lifetime.ApplicationStarted.Register( this.ApplicationStarted );
            lifetime.ApplicationStopped.Register( this.ApplicationStopped );

            this.Bootstrap();
        }

        private void ApplicationStarted()
        {
            try
            {
                string dbLocation = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
                dbLocation = Path.Combine( dbLocation, "MeditationLogger" );

                if( Directory.Exists( dbLocation ) == false )
                {
                    Directory.CreateDirectory( dbLocation );
                }
                dbLocation = Path.Combine( dbLocation, "logbook.db" );

                Console.WriteLine( "Database Location: " + dbLocation );

                this.api = new MeditationLoggerApi( dbLocation );
            }
            catch( Exception e )
            {
                Console.WriteLine( "Error creating API" );
                Console.WriteLine( e );
                throw;
            }
        }

        private void ApplicationStopped()
        {
            Console.WriteLine( "Disposing API." );
            this.api?.Dispose();
        }

        private async void Bootstrap()
        {
            var options = new BrowserWindowOptions
            {
                WebPreferences = new WebPreferences
                {
                    WebSecurity = false
                }
            };

            await Electron.WindowManager.CreateWindowAsync( options );
        }
    }
}
