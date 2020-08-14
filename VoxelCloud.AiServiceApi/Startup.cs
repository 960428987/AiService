using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace VoxelCloud.AiServiceApi
{
    public class Startup
    {
        /// <summary>
        /// Api�汾��Ϣ
        /// </summary>
        private IApiVersionDescriptionProvider provider;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region ���Swagger
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo { Title = "VoxelCloud Ai API", Version = "v1" });
            //    string[] xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            //    foreach (var xmlFile in xmlFiles)
            //    {
            //        options.IncludeXmlComments(xmlFile, true);
            //    }
            //});
            services.AddSwaggerGen(options =>
            {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    
                    options.SwaggerDoc(description.GroupName,
                         new OpenApiInfo()
                         {
                             Title = $"VoxelCloud Ai API v{description.ApiVersion}",
                             Version = description.ApiVersion.ToString(),
                             Description = "�л��汾������Ͻǰ汾�л�",
                         }
                    );
                    string[] xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                    foreach (var xmlFile in xmlFiles)
                    {
                        options.IncludeXmlComments(xmlFile, true);
                    }
                }
            });
            #endregion
            #region ���ApiVersion
            services.AddApiVersioning(option =>
            {
                // ��ѡ��ΪtrueʱAPI����֧�ֵİ汾��Ϣ
                option.ReportApiVersions = true;
                // ���ṩ�汾ʱ��Ĭ��Ϊ1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                // ������δָ���汾ʱĬ��Ϊ1.0
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.QueryStringApiVersionReader("version");//Ĭ����api-version,�����޸ĳ�version
            }).AddVersionedApiExplorer(option =>
            {          // �汾���ĸ�ʽ��v+�汾��
                option.GroupNameFormat = "'v'V";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            this.provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            #endregion
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            // ���Swagger�й��м��
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // c.SwaggerEndpoint("/swagger/v1/swagger.json", "VoxelCloud Ai API");
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi");
                //c.OAuthClientId("testuserservicesapiexaminationservicesapi");
                //c.OAuthAppName("������");
            });
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
