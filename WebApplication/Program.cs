using codeFirst.models;
using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using Repositories.Repositories;
using service.Common;
using service.Implementations;
using service.interfaces;
using service.services;

namespace WebApplicationProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<School>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IContext, School>();

            builder.Services.AddScoped<IstudentService, studentService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IClassRoomService, ClassRoomService>();
            builder.Services.AddScoped<ClassRoomRepository>();


            builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
            builder.Services.AddScoped<StudentRepository>();
            // --- Assignment Section ---
            // רישום ה-Repository פעם אחת בלבד
            builder.Services.AddScoped<IRepository<Assignment>, AssignmentRepository>();
            builder.Services.AddScoped<AssignmentRepository>();

            // רישום ה-Service פעם אחת בלבד
            builder.Services.AddScoped<IAssignmentService, AssignmentService>();

            builder.Services.AddScoped<IRepository<Staff>, StaffRepository>();
            builder.Services.AddScoped<StaffRepository>();

            builder.Services.AddScoped<IRepository<Submission>, SubmissionRepository>();
            builder.Services.AddScoped<SubmissionRepository>();

            builder.Services.AddScoped<IRepository<ClassRoom>, ClassRoomRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // כאן שים את הכתובת של ה-React שלך
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<TokenService>();
            var app = builder.Build();
            app.UseCors("AllowReactApp");
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
