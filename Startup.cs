using Data.Interfaces;
using Data.Mocks;
using Data;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Data.Repository;
using Microsoft.Data.SqlClient;
using Data.Model;

namespace ReviewAggregatorWebApp
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAllUsers, UserRepository>();
            services.AddScoped<ICategoryRepository<Country>, CountryRepository>();
            services.AddScoped<ICategoryRepository<Director>, DirectorRepository>();
            services.AddScoped<ICategoryRepository<Genre>, GenreRepository>();
            services.AddTransient<IAllMovies, MovieRepository>();
            services.AddTransient<IAllReviews, ReviewRepository>();
        }

        private static bool TestConnection(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                return false;
            }
        }
    }
}