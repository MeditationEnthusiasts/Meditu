//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Meditu.Gui
{
    public class Startup
    {
        // ---------------- Fields ----------------

        /// <summary>
        /// Where to put the logbook. It is possible for the user to specify this from the command line.
        /// However, by default, it will end up in the user's Application Data folder if
        /// not specified from the command line.
        /// </summary>
        private string logbookLocation;

        // ---------------- Functions ----------------

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc(
                options => options.EnableEndpointRouting = false
            );
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary> 
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IConfiguration config )
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
                        template: "{controller=Home}/{action=Index}/{id?}"
                    );
                }
            );

            this.logbookLocation = config["logbook"];
            if( string.IsNullOrWhiteSpace( this.logbookLocation ) )
            {
                this.logbookLocation = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
                this.logbookLocation = Path.Combine( this.logbookLocation, "Meditu", "logbook.db" );
            }

            app.UseStaticFiles();

            lifetime.ApplicationStarted.Register( this.ApplicationStarted );
            lifetime.ApplicationStopped.Register( this.ApplicationStopped );

            if( HybridSupport.IsElectronActive )
            {
                this.ElectronBootstrap();
            }
        }

        private void ApplicationStarted()
        {
            try
            {
                string dir = Path.GetDirectoryName( this.logbookLocation );
                if( Directory.Exists( dir ) == false )
                {
                    Directory.CreateDirectory( dir );
                }

                Console.WriteLine( "Database Location: " + this.logbookLocation );

                ApiBridge.CreateInstance( this.logbookLocation );
                ApiBridge.Instance.LogBook.Refresh();
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
            ApiBridge.DestroyInstance();
        }

        private async void ElectronBootstrap()
        {
            var browserWindow = await Electron.WindowManager.CreateWindowAsync( 
                new BrowserWindowOptions
                {
                    Width = 1152,
                    Height = 864,
                    Show = false
                }
            );

            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.SetTitle( "Meditation Logger" );
        }
    }
}
