using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitByBitApiService.Migrations
{
    public partial class LastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FitnessGoal = table.Column<int>(type: "int", nullable: false),
                    CurrentFitness = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    StartingWeight = table.Column<double>(type: "float", nullable: false),
                    TargetWeight = table.Column<double>(type: "float", nullable: false),
                    Bmi = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Set = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    ExpertiseLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerificationOtps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiry = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationOtps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationOtps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Bmi", "ConcurrencyStamp", "CreatedDate", "CurrentFitness", "Email", "EmailConfirmed", "FirstName", "FitnessGoal", "Height", "IsActive", "IsDeleted", "LastLogin", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StartingWeight", "TargetWeight", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "adecb697-fa5f-4625-bc49-5d45265de74d", 0, 23, 1.8899999999999999, "40da3646-e596-45b6-8275-f7e1ff6eca79", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2775), 1, "superuserD@gmail.com", true, "Super", 1, 1.8300000000000001, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", false, null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2775), "SUPERUSERD@GMAIL.COM", "SUPERUSER@GMAIL.COM", "AQAAAAEAACcQAAAAEJkXs+pAs3yQz99TcaMmI0xQUTvdv8ZVVDBygGS/ehHwOgoQpZyO3v+9mBvE8JmIcA==", "08130927418", true, "eaa96e01-9cb8-43d4-a27c-d0074134645e", 80.0, 0.0, false, "superuser@gmail.com" },
                    { "e4d269e5-7dc3-4a65-9485-578156f0e746", 0, 23, 1.8899999999999999, "0561bc76-f79d-400d-a3dd-256a685c73a3", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2829), 1, "osinfadeidris@gmail.com", true, "Idris", 1, 1.8300000000000001, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Osinfade", false, null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2829), "OSINFADEIDRIS@GMAIL.COM", "OSINFADEIDRIS@GMAIL.COM", "AQAAAAEAACcQAAAAEJkXs+pAs3yQz99TcaMmI0xQUTvdv8ZVVDBygGS/ehHwOgoQpZyO3v+9mBvE8JmIcA==", "07069576909", true, "6a09160e-9efb-4da9-bffe-e18debfaba2f", 80.0, 0.0, false, "osinfadeidris@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "ExerciseName", "ImageUrl", "Name", "Number", "Reps", "Rest", "Set", "Time" },
                values: new object[,]
                {
                    { new Guid("088d60e6-f039-4f92-88f4-c98647fc7eb1"), "Jumping Lunges", null, "45 Minute HIIT Workout", "5", null, "10 sec", null, "30 sec" },
                    { new Guid("0b0a2b9e-3ae6-4e93-9d4a-0f209df3c3ee"), "Mountain Climber", null, "20 Minute HIIT Workout", "3", null, "10 sec", null, "30 sec" },
                    { new Guid("0bbe3c16-60dd-4895-b785-9fcb1366c8e5"), "Stiff Leg Deadlift", null, "Intermediate Fat Loss Workout", "2", "12, 10, 8", null, "3", null },
                    { new Guid("1331e673-0958-4d84-937a-991dfd42c4d9"), "Jumping Jacks", null, "30 Minute Cardio Workout", "1", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("17f2091e-90a4-48ae-87ee-e585dce59842"), "High Kick", null, "20 Minute HIIT Workout", "6", null, "10 sec", null, "30 sec" },
                    { new Guid("1a8e3279-f421-49c9-aa22-4071d3b52945"), "Forward Lunge", null, "20 Minute HIIT Workout", "1", null, "10 sec", null, "30 sec" },
                    { new Guid("1b47c60a-020b-4314-95f6-56b950329c9b"), "Leg Press", null, "Intermediate Fat Loss Workout", "3", "12, 10, 8", null, "3", null },
                    { new Guid("1c54d6a1-dc0f-4ca1-a030-d04f91369da9"), "Cruches", null, "Beginner Strength Training Workout", "4", "12, 12, 12", null, "3", null },
                    { new Guid("216d8825-961f-410f-b32c-b311217c388d"), "Crunches", null, "30 Minute Cardio Workout", "3", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("2324740f-da71-4db8-af88-fa488faf170d"), "Burpees", null, "30 Minute Cardio Workout", "6", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("28ec5a44-1254-4e33-a732-2c129b6df179"), "Jogging in Place", null, "30 Minute Cardio Workout", "7", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("3858c199-35aa-4aa4-846b-6c651447a83c"), "Seated Calf Raise", null, "Intermediate Fat Loss Workout", "5", "12, 10, 8", null, "3", null },
                    { new Guid("38f62ace-bed1-4a35-a516-3b69b6ff38a6"), "Crunches", null, "Intermediate Fat Loss Workout", "6", "12, 12, 12", null, "3", null },
                    { new Guid("3f68ff82-cfba-4f3c-ac5a-53a612d528d5"), "Push Ups", null, "45 Minute HIIT Workout", "6", null, "10 sec", null, "30 sec" },
                    { new Guid("443cb2e2-de9c-4718-a0fa-e502551d4e4d"), "Lateral Raise", null, "Intermediate Fat Loss Workout", "7", "12, 10, 8", null, "3", null },
                    { new Guid("4b40ce62-789f-497f-9a25-b6c052a6821c"), "Jogging in Place", null, "20 Minute HIIT Workout", "4", null, "10 sec", null, "30 sec" },
                    { new Guid("5403281e-b58e-4ab2-ab4a-589674ed46c7"), "Jumping Lunges", null, "30 Minute Cardio Workout", "8", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("54a83681-d68c-4f04-a17c-19189338832d"), "Hammer Dumbbell Curl", null, "Intermediate Fat Loss Workout", "8", "12, 10, 9", null, "3", null },
                    { new Guid("5e8b5bd6-cad5-4b8b-9895-282f0a914881"), "Squat Jumps", null, "45 Minute HIIT Workout", "2", null, "10 sec", null, "30 sec" },
                    { new Guid("657966cf-ce31-4e44-b3ca-1bc75c02d70a"), "Goblet Squat", null, "Intermediate Fat Loss Workout", "1", "12, 10, 8", null, "3", null },
                    { new Guid("73fdba60-8344-453d-b441-0a8f2157166f"), "Burpee", null, "Beginner At Home Workout", "1", "10-15", null, "2", null },
                    { new Guid("89c3e5ca-b9c2-471d-b33f-65adf56d8c4d"), "Deadlift", null, "Beginner Strength Training Workout", "1", "12, 10, 8", null, "3", null },
                    { new Guid("8c75b956-db07-4033-bc3a-ccfd99522b8d"), "Leg Press", null, "Beginner Strength Training Workout", "2", "12, 10, 8", null, "3", null },
                    { new Guid("9ee7e06e-91ac-44ec-88bb-0514b58b18b5"), "Star Jumps", null, "Beginner At Home Workout", "8", null, null, "2", "1 min" },
                    { new Guid("9fe0bed3-c861-435e-be45-2d36e73133df"), "Jumping Jacks", null, "45 Minute HIIT Workout", "4", null, "10 sec", null, "30 sec" },
                    { new Guid("a0b375dc-5043-4dcb-89d8-c4d1a3f2af54"), "Reverse or Walking Lunge", null, "Beginner At Home Workout", "7", "10-15", null, "2", null },
                    { new Guid("a0ba1d0f-f961-433c-b2d3-90322866bf3e"), "Burpees", null, "45 Minute HIIT Workout", "1", null, "10 sec", null, "30 sec" },
                    { new Guid("a0bbf3a8-382a-4122-8f63-8e50f6146338"), "Mountain Climber", null, "30 Minute Cardio Workout", "5", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("a46fdc98-0980-49f7-82f7-119ee060a9e6"), "Mountain Climber", null, "Beginner At Home Workout", "6", "10-15", null, "2", null },
                    { new Guid("b2e3dce5-60a5-45b4-80af-2538d66f1fd3"), "Straight Bar Tricep Extension", null, "Intermediate Fat Loss Workout", "9", "12, 10, 10", null, "3", null },
                    { new Guid("b4d71a7d-799c-4fe2-8e47-faebdcb94504"), "Walking Lunge", null, "Intermediate Fat Loss Workout", "4", "12, 10, 8", null, "3", null },
                    { new Guid("b9236923-3644-493c-bae1-c847c65ea911"), "Burpees", null, "20 Minute HIIT Workout", "5", null, "10 sec", null, "30 sec" },
                    { new Guid("bc854a05-c3f5-4d1c-bb61-96f2306a0c72"), "Squat Jumps", null, "30 Minute Cardio Workout", "4", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("c9dc17af-62b8-478a-a8af-0a13e74003f8"), "Push Up", null, "Beginner At Home Workout", "2", "10-15", null, "2", null },
                    { new Guid("ca6a7f2c-2dd3-499f-bb03-dc9adfea5319"), "Walking Lunge", null, "Beginner Strength Training Workout", "3", "12, 10, 8", null, "3", null },
                    { new Guid("cd0186df-73ed-4f9e-97f1-2a411259d77c"), "Lateral Raise", null, "Beginner Strength Training Workout", "5", "12, 10, 8", null, "3", null },
                    { new Guid("cdaa0be3-0c7a-462c-b79e-b87a49f04062"), "Bicycle Crunch", null, "Beginner At Home Workout", "5", "10-15", null, "2", null },
                    { new Guid("dfff8c05-9bcc-479e-ab58-adc31ee888ac"), "Squat Jump", null, "Beginner At Home Workout", "4", "10-15", null, "2", null },
                    { new Guid("e3205083-4e91-479d-9676-b66f3bf37652"), "Push Ups", null, "30 Minute Cardio Workout", "2", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("e41e97a9-b4c7-41a7-869a-8d664e326f8d"), "Hammer Dumbbell Curl", null, "Beginner Strength Training Workout", "6", "12, 10, 9", null, "3", null }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "ExerciseName", "ImageUrl", "Name", "Number", "Reps", "Rest", "Set", "Time" },
                values: new object[,]
                {
                    { new Guid("f4850a3f-ffa7-468c-87a4-0dbff030260b"), "Crunches", null, "45 Minute HIIT Workout", "3", null, "10 sec", null, "30 sec" },
                    { new Guid("ff0ac2ed-84e8-428e-afe6-b588da2a0402"), "Floor Crunch", null, "Beginner At Home Workout", "3", "10-15", null, "2", null },
                    { new Guid("ffe615f9-6d51-4b66-b91f-cdfb2bd06c70"), "Bodyweight Squat", null, "20 Minute HIIT Workout", "2", null, "10 sec", null, "30 sec" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "CreatedDate", "FoodGroup", "ImageUrl", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0208e491-23e5-410b-bdf1-535cc440664e"), "183", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2614), "4", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2614), "Salmon Baked" },
                    { new Guid("06f5a277-8602-494d-8df8-d2acaf8741c5"), "139", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2504), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2505), "Pork And Rice With Tomato-Based Sauce" },
                    { new Guid("142dfe66-637d-4a59-a60f-bd2694ae670c"), "130", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2442), "1", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2445), "White Rice" },
                    { new Guid("16ca550c-3411-432c-b799-59f48fbbf6d4"), "136", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2550), "4", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2551), "Canned Pink Salmon" },
                    { new Guid("1da4bb91-9df8-4b4b-aae5-60c87d1bc30c"), "149", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2520), "1", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2520), "Whole Wheat Pasta" },
                    { new Guid("226b6c97-2a24-41f1-b0aa-8fec059cbbe8"), "226", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2534), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2534), "Chicken Leg Drumstick And Thigh Baked" },
                    { new Guid("3848087d-1161-4870-b09a-4250c001417a"), "135", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2512), "4", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2513), "Fish And Rice With Tomato-Based Sauce" },
                    { new Guid("3bd92e02-ef78-4ce2-a427-65cca332329a"), "130", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2476), "1", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2476), "Medium Grain White Rice" },
                    { new Guid("4542c9c9-de32-480b-a09e-e1fe342d4eab"), "147", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2514), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2515), "Beef, Rice And Vegetables" },
                    { new Guid("5d3b3b31-d49d-4f45-a536-7ea129af3e23"), "167", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2548), "4", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2549), "Canned Salmon" },
                    { new Guid("687a5531-a4df-4840-844b-9ad6eaad994e"), "71", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2617), "5", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2617), "Fortified Fruit Juice Smoothie" },
                    { new Guid("6a4873eb-d8f4-44f4-be3e-8c3bbee87b4f"), "48", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2624), "5", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2624), "Apples (Without Skin)" },
                    { new Guid("76441fc3-eadb-4752-ad26-164fad96d6ae"), "192", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2530), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2530), "Chicken Breast Baked" },
                    { new Guid("7f51c0f0-7e6c-4ade-9436-65246993f5b1"), "117", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2546), "4", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2547), "Smoked Salmon" },
                    { new Guid("85b19459-490b-427a-8066-190fa9677902"), "89", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2626), "5", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2626), "Bananas" },
                    { new Guid("9694908a-ef55-41a9-bee7-254077ae1034"), "381", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2518), "3", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2518), "Cereal (Kellogg's Rice Krispies)" },
                    { new Guid("9a9f40c1-15f1-4dcb-bd71-2752ce52efd7"), "112", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2489), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2489), "Chili Con Carne With Beans And Rice" },
                    { new Guid("a3eec80f-d569-4e23-b0aa-2b274cec338b"), "141", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2510), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2511), "Chicken Or Turkey And Rice With Tomato-Based Sauce" },
                    { new Guid("a79c1eb4-b770-46c0-9e67-2b615ba50005"), "128", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2516), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2516), "Pork, Rice And Vegetables " },
                    { new Guid("acbd9e99-c67e-44bf-a3cd-0897bd1faabd"), "136", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2487), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2487), "Beef And Rice With Tomato-Based Sauce" },
                    { new Guid("b2f083a9-1f51-4edf-a13e-a728fcb8aca5"), "206", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2552), "4", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2552), "Farmed Atlantic Salmon" },
                    { new Guid("b5377e48-74b2-443a-b23b-d4ee393b6d65"), "148", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2527), "1", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2527), "Pasta Whole Grain Cooked" },
                    { new Guid("b62aac6f-5c51-4611-99a0-124b09e0e4ab"), "108", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2479), "1", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2479), "Rice Noodles (Cooked)" },
                    { new Guid("c29c1ab0-6c3c-4959-973d-effaf0d432c0"), "211", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2539), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2540), "Chicken Drumstick Fried" },
                    { new Guid("cd9c1722-bc72-4770-b35b-6f4dc562c927"), "52", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2619), "5", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2619), "Apples" },
                    { new Guid("d77af16b-01ca-427b-97a2-960b3afbd326"), "148", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2507), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2507), "Chicken Or Turkey And Rice No Sauce" },
                    { new Guid("e9386ea7-70f2-462e-8c3d-214275dfd4b4"), "200", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2538), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2538), "Chicken Drumstick Grilled" },
                    { new Guid("ea0fcf1a-d8a2-4ecb-9617-a740db303a24"), "176", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2532), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2532), "Chicken Breast Grilled" },
                    { new Guid("ec3f5900-7653-4858-b964-413121dcbe05"), "182", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2544), "4", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2545), "Wild Atlantic Salmon" },
                    { new Guid("f476d60a-250d-4493-b6ed-af4cbddaceed"), "157", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2525), "1", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2525), "Pasta Cooked" },
                    { new Guid("f50008ac-4888-4e24-a12e-8820bc4ba85f"), "145", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2483), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2483), "Beef And Rice No Sauce" },
                    { new Guid("f9d4244c-cb52-428f-a713-67aa7cc113fd"), "190", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2536), "2", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2536), "Chicken Drumstick Baked" },
                    { new Guid("fdf77f82-2d8d-4e4f-bc58-012634d1590f"), "123", new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2481), "1", null, new DateTime(2024, 4, 21, 10, 33, 38, 43, DateTimeKind.Utc).AddTicks(2481), "Brown Rice" }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPrograms",
                columns: new[] { "Id", "Category", "ExerciseName", "ExpertiseLevel" },
                values: new object[,]
                {
                    { new Guid("02c64103-98e5-4f25-aea6-f3d3488270d5"), 4, "20 Minute HIIT Workout", 2 },
                    { new Guid("14c1cfa4-1582-4049-8d37-07e406e2dd37"), 4, "45 Minute HIIT Workout", 2 },
                    { new Guid("1b08f335-4c23-49e6-b9d1-2bfa9cdd1157"), 4, "Intermediate Fat Loss Workout", 2 },
                    { new Guid("1ebd7a1a-7663-450e-81fd-9df3e67f8681"), 1, "45 Minute HIIT Workout", 3 },
                    { new Guid("2ca8966c-7615-4a7c-9c72-b9e53fc2e1a2"), 1, "30 Minute Cardio Workout", 1 },
                    { new Guid("35f9f304-a15e-401d-bbc9-daa1185f1876"), 1, "20 Minute HIIT Workout", 2 }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPrograms",
                columns: new[] { "Id", "Category", "ExerciseName", "ExpertiseLevel" },
                values: new object[,]
                {
                    { new Guid("45632520-a6fa-480f-9fba-ae342e4a1706"), 1, "Beginner At Home Workout", 1 },
                    { new Guid("4db25770-64a3-4559-8d0a-592a772f39a2"), 2, "45 Minute HIIT Workout", 3 },
                    { new Guid("5464d40f-f0e1-48d7-9c5d-f827a234577d"), 3, "Beginner Strength Training Workout", 1 },
                    { new Guid("5a6f1b96-fcec-4551-a17f-b11c2c327ae8"), 1, "Intermediate Fat Loss Workout", 3 },
                    { new Guid("9d23751c-5c98-4608-9681-9bbfca61251e"), 4, "Intermediate Fat Loss Workout", 3 },
                    { new Guid("c44356c3-d9e6-4328-b46f-0858bc4e754a"), 1, "45 Minute HIIT Workout", 2 },
                    { new Guid("dd690b38-6a49-448d-b6ee-4dd6ffebef33"), 4, "45 Minute HIIT Workout", 3 },
                    { new Guid("ec5df2e1-6405-4617-ab12-a35046fe176c"), 2, "45 Minute HIIT Workout", 2 },
                    { new Guid("f393a1d9-e856-4e38-8138-2c00e26204b1"), 1, "Intermediate Fat Loss Workout", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationOtps_UserId",
                table: "VerificationOtps",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "MealPlans");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "VerificationOtps");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "WorkoutPrograms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
