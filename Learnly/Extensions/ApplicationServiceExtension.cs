using Learnly.APIs.Errors;
using Learnly.APIs.Helpers;
using Learnly.Core.Entities.Identity;
using Learnly.Core.Repositories.Contract;
using Learnly.Core.Services.Contract;
using Learnly.Repository.Data.Identity;
using Learnly.Repository.Repositories;
using Learnly.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learnly.APIs.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(ICourseSelectionRepository), typeof(CourseSelectionRepository));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IEnrollmentServicee), typeof(EnrollmentService));

            services.AddScoped(typeof(IAuthService), typeof(AuthService));


            // Handling Vaidation Error
            services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.InvalidModelStateResponseFactory = (ActionContext) =>
                    {
                        var errors = ActionContext.ModelState.Where(c => c.Value.Errors.Count() > 0)
                        .SelectMany(c => c.Value.Errors)
                        .Select(E => E.ErrorMessage).ToList();

                        var response = new ApiValidationErroResponse()
                        {
                            Errors = errors
                        };
                        return new BadRequestObjectResult(response);
                    };
                });

            

            return services;
        }

    }
}
