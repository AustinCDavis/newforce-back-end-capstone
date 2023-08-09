using BackEndCapstone.Repositories;

namespace BackEndCapstone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IRegimenRepository, RegimenRepository>();
            builder.Services.AddTransient<IRegimenExerciseRepository, RegimenExerciseRepository>();
            builder.Services.AddTransient<IRegimenAssignmentRepository, RegimenAssignmentRepository>();
            builder.Services.AddTransient<IPatientAssignmentRepository, PatientAssignmentRepository>();
            builder.Services.AddTransient<INoteRepository, NoteRepository>();
            builder.Services.AddTransient<IMessageRepository, MessageRepository>();
            builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddTransient<ICommentRepository, CommentRepository>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}