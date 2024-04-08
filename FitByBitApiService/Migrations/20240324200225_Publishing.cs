using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitByBitApiService.Migrations
{
    public partial class Publishing : Migration
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
                    { "6d8c0550-f1a4-4669-8cd7-ed382e9aaacd", 0, 23, 1.8899999999999999, "28a83716-0440-4357-9cf8-1bc282422dd4", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8189), 1, "osinfadeidris@gmail.com", true, "Idris", 1, 1.8300000000000001, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Osinfade", false, null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8189), "OSINFADEIDRIS@GMAIL.COM", "OSINFADEIDRIS@GMAIL.COM", "AQAAAAEAACcQAAAAECh++xR6INEZJHSVZxrXMOO1QXCR5mAoo08mFGMC+F4OcQtYXx3Sl6t3bY4wK/HttQ==", "07069576909", true, "d2608a3f-ef5e-42ee-b228-a30451757ed5", 80.0, 0.0, false, "osinfadeidris@gmail.com" },
                    { "fc3ea0ad-7ad2-408a-8311-20f4279b8b04", 0, 23, 1.8899999999999999, "1ef73f89-b28b-4b34-9343-c6f1870dd480", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8115), 1, "superuserD@gmail.com", true, "Super", 1, 1.8300000000000001, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", false, null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8116), "SUPERUSERD@GMAIL.COM", "SUPERUSER@GMAIL.COM", "AQAAAAEAACcQAAAAECh++xR6INEZJHSVZxrXMOO1QXCR5mAoo08mFGMC+F4OcQtYXx3Sl6t3bY4wK/HttQ==", "08130927418", true, "1050b348-835a-49b8-8713-f78b1d742447", 80.0, 0.0, false, "superuser@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "ExerciseName", "ImageUrl", "Name", "Number", "Reps", "Rest", "Set", "Time" },
                values: new object[,]
                {
                    { new Guid("00dbb0ef-5ad5-48dc-98fc-b3c13a5a95e3"), "Reverse or Walking Lunge", null, "Beginner At Home Workout", "7", "10-15", null, "2", null },
                    { new Guid("09a1ed6c-f51d-422b-8293-9ffd6a49d46c"), "Deadlift", null, "Beginner Strength Training Workout", "1", "12, 10, 8", null, "3", null },
                    { new Guid("1cf46cc4-c51b-440a-9af9-2a1f5ad7b1e8"), "Lateral Raise", null, "Beginner Strength Training Workout", "5", "12, 10, 8", null, "3", null },
                    { new Guid("1e28caf4-a699-44ec-9691-b9967b950ddc"), "Star Jumps", null, "Beginner At Home Workout", "8", null, null, "2", "1 min" },
                    { new Guid("2e385b79-7bcd-4368-b7ec-fe20dbd02b8b"), "Walking Lunge", null, "Beginner Strength Training Workout", "3", "12, 10, 8", null, "3", null },
                    { new Guid("31cb19c7-e815-454c-abbc-d32374c5a6c6"), "Crunches", null, "45 Minute HIIT Workout", "3", null, "10 sec", null, "30 sec" },
                    { new Guid("408cd5a2-08de-468b-b354-93b4b94180ca"), "Push Ups", null, "45 Minute HIIT Workout", "6", null, "10 sec", null, "30 sec" },
                    { new Guid("457cd7b9-a5f5-41d4-8f8d-0413a7598edf"), "Burpees", null, "30 Minute Cardio Workout", "6", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("5179a590-59fe-4d00-9b19-b2d7de416336"), "Crunches", null, "30 Minute Cardio Workout", "3", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("518ad58d-e176-4ab5-b24c-159e9fc8e2bf"), "Jumping Lunges", null, "45 Minute HIIT Workout", "5", null, "10 sec", null, "30 sec" },
                    { new Guid("591394dd-b44d-4459-9247-022ebc5efbfd"), "Squat Jumps", null, "45 Minute HIIT Workout", "2", null, "10 sec", null, "30 sec" },
                    { new Guid("66fed5ce-3869-4c30-934f-cbbeb7bd970b"), "Floor Crunch", null, "Beginner At Home Workout", "3", "10-15", null, "2", null },
                    { new Guid("682db761-7c12-4200-ad69-e70bfddda5cb"), "Leg Press", null, "Intermediate Fat Loss Workout", "3", "12, 10, 8", null, "3", null },
                    { new Guid("692d9157-15f9-410b-9844-61840f7a3771"), "Bodyweight Squat", null, "20 Minute HIIT Workout", "2", null, "10 sec", null, "30 sec" },
                    { new Guid("6d7c4ec0-c0de-4082-8db2-3ea876419750"), "Burpees", null, "45 Minute HIIT Workout", "1", null, "10 sec", null, "30 sec" },
                    { new Guid("815e1623-d3d6-49b5-9a52-ed068618e6b8"), "Push Up", null, "Beginner At Home Workout", "2", "10-15", null, "2", null },
                    { new Guid("90b6ca6e-03eb-482a-a919-3ad0784bb200"), "Jumping Jacks", null, "45 Minute HIIT Workout", "4", null, "10 sec", null, "30 sec" },
                    { new Guid("919908a3-b802-40d4-8213-57843b18be4b"), "High Kick", null, "20 Minute HIIT Workout", "6", null, "10 sec", null, "30 sec" },
                    { new Guid("963cc095-8005-4b4e-b37b-b06cc252dbbd"), "Goblet Squat", null, "Intermediate Fat Loss Workout", "1", "12, 10, 8", null, "3", null },
                    { new Guid("96c427c9-a931-4d7a-bf4f-fda70f7c4f9e"), "Mountain Climber", null, "20 Minute HIIT Workout", "3", null, "10 sec", null, "30 sec" },
                    { new Guid("9798c406-797b-48d0-9e14-8e0db564bb60"), "Jumping Jacks", null, "30 Minute Cardio Workout", "1", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("a82c8cfd-6e98-4bff-bc39-a89a8f172008"), "Lateral Raise", null, "Intermediate Fat Loss Workout", "7", "12, 10, 8", null, "3", null },
                    { new Guid("a928f1c2-02bd-4a7c-a3f0-b9fbb1cc4ffc"), "Squat Jumps", null, "30 Minute Cardio Workout", "4", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("a983465a-d157-4059-9bdd-e2dbe6013b61"), "Jogging in Place", null, "20 Minute HIIT Workout", "4", null, "10 sec", null, "30 sec" },
                    { new Guid("ac747251-9432-42c2-a6b1-8716b5d87f51"), "Mountain Climber", null, "30 Minute Cardio Workout", "5", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("b3ea7b44-c921-4a15-82aa-22e1bd9b1e97"), "Burpees", null, "20 Minute HIIT Workout", "5", null, "10 sec", null, "30 sec" },
                    { new Guid("b9508742-25ed-416b-9e72-dc9d47bc8b71"), "Stiff Leg Deadlift", null, "Intermediate Fat Loss Workout", "2", "12, 10, 8", null, "3", null },
                    { new Guid("bacc7051-6c35-48a1-bbab-2bd96f11fe1e"), "Cruches", null, "Beginner Strength Training Workout", "4", "12, 12, 12", null, "3", null },
                    { new Guid("bcf64ccb-ce3e-44fa-b00a-97f43aab5200"), "Hammer Dumbbell Curl", null, "Beginner Strength Training Workout", "6", "12, 10, 9", null, "3", null },
                    { new Guid("c8c32d81-3a81-42b5-98de-d7852d5689a9"), "Leg Press", null, "Beginner Strength Training Workout", "2", "12, 10, 8", null, "3", null },
                    { new Guid("c9ac2ec5-2590-429b-907c-16478baee60d"), "Straight Bar Tricep Extension", null, "Intermediate Fat Loss Workout", "9", "12, 10, 10", null, "3", null },
                    { new Guid("d7a4ce00-ae65-44a9-8a6c-e14bbc255cb8"), "Seated Calf Raise", null, "Intermediate Fat Loss Workout", "5", "12, 10, 8", null, "3", null },
                    { new Guid("da3d22b0-e238-476e-b864-96d8240807d8"), "Burpee", null, "Beginner At Home Workout", "1", "10-15", null, "2", null },
                    { new Guid("dcb993e6-4550-45ea-ac0d-7b4bc1a4f999"), "Squat Jump", null, "Beginner At Home Workout", "4", "10-15", null, "2", null },
                    { new Guid("deaa9976-d8a6-4b13-89b0-ffa0c08d2caa"), "Crunches", null, "Intermediate Fat Loss Workout", "6", "12, 12, 12", null, "3", null },
                    { new Guid("e109af33-5c1c-4c19-b27a-3c100d5992f2"), "Forward Lunge", null, "20 Minute HIIT Workout", "1", null, "10 sec", null, "30 sec" },
                    { new Guid("e25cc5d7-5999-42c3-8858-409da5755c26"), "Bicycle Crunch", null, "Beginner At Home Workout", "5", "10-15", null, "2", null },
                    { new Guid("e2b88506-10dc-4a92-8511-67f6f8f8890f"), "Jogging in Place", null, "30 Minute Cardio Workout", "7", null, "10-20 sec", null, "40-50 sec" },
                    { new Guid("e54e4316-8aa4-4a63-a26f-bd1a4223113b"), "Walking Lunge", null, "Intermediate Fat Loss Workout", "4", "12, 10, 8", null, "3", null },
                    { new Guid("f735c7c4-b093-416b-abc7-40272139032e"), "Jumping Lunges", null, "30 Minute Cardio Workout", "8", null, "10-20 sec", null, "40-50 sec" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "ExerciseName", "ImageUrl", "Name", "Number", "Reps", "Rest", "Set", "Time" },
                values: new object[,]
                {
                    { new Guid("f748167f-2fcf-4045-aa4f-5f580d4dcbd9"), "Hammer Dumbbell Curl", null, "Intermediate Fat Loss Workout", "8", "12, 10, 9", null, "3", null },
                    { new Guid("f9f57f6b-5ab2-4299-afb3-f7c3eb5bcd24"), "Mountain Climber", null, "Beginner At Home Workout", "6", "10-15", null, "2", null },
                    { new Guid("fa1df10e-da7a-46fa-9309-3631081b367b"), "Push Ups", null, "30 Minute Cardio Workout", "2", null, "10-20 sec", null, "40-50 sec" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "CreatedDate", "FoodGroup", "ImageUrl", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0b14628c-9317-43c4-976d-b75f3a6a2a53"), "200", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7965), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7966), "Chicken Drumstick Grilled" },
                    { new Guid("0becc998-4408-4386-a78f-f3148d5b23cd"), "182", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7972), "4", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7972), "Wild Atlantic Salmon" },
                    { new Guid("0c9a1d4c-c674-4a44-8380-87288b04af82"), "123", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7887), "1", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7888), "Brown Rice" },
                    { new Guid("0f28227e-e6e6-454e-b7d8-5bdb1cba69a8"), "136", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7982), "4", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7982), "Canned Pink Salmon" },
                    { new Guid("16069de6-107a-4926-a59c-90e7092c2a88"), "206", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7988), "4", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7989), "Farmed Atlantic Salmon" },
                    { new Guid("1ce5b792-7024-4a75-ab35-944be0b24fab"), "167", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7978), "4", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7979), "Canned Salmon" },
                    { new Guid("1e596455-ea45-4d7c-bc74-a15a4b5665c6"), "145", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7890), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7891), "Beef And Rice No Sauce" },
                    { new Guid("2c1fbd10-e776-4424-97cc-b6f32194eed7"), "89", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8004), "5", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8005), "Bananas" },
                    { new Guid("32c4a4b7-a398-4320-947a-c700086c677e"), "381", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7933), "3", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7933), "Cereal (Kellogg's Rice Krispies)" },
                    { new Guid("3314ee4d-0b7b-422f-8fda-acc2cbc1d24b"), "112", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7901), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7901), "Chili Con Carne With Beans And Rice" },
                    { new Guid("38a1083b-d0ff-4eb7-b830-6d3823b69070"), "130", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7867), "1", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7867), "Medium Grain White Rice" },
                    { new Guid("534c0446-9d5a-4b7e-ad39-75defb1bca87"), "149", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7936), "1", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7936), "Whole Wheat Pasta" },
                    { new Guid("58a25144-7ccf-487f-8f1d-9505b07e597f"), "147", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7926), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7927), "Beef, Rice And Vegetables" },
                    { new Guid("5906e1a1-c320-49ed-947f-38b05bdcbdfe"), "108", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7871), "1", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7871), "Rice Noodles (Cooked)" },
                    { new Guid("5a2c69a6-df25-44ab-b6c4-9b8a79d8fcc7"), "148", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7943), "1", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7943), "Pasta Whole Grain Cooked" },
                    { new Guid("6f739565-4fa6-4d9d-a444-8f2176860dfe"), "141", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7915), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7916), "Chicken Or Turkey And Rice With Tomato-Based Sauce" },
                    { new Guid("799d8e82-4cac-49be-8cfb-49d917d9dd25"), "135", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7918), "4", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7919), "Fish And Rice With Tomato-Based Sauce" },
                    { new Guid("854bcbe6-b81f-4a0c-bf4e-01106ba1e80b"), "139", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7904), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7904), "Pork And Rice With Tomato-Based Sauce" },
                    { new Guid("88e30bbf-d091-497f-b782-0c0d090abbb2"), "190", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7961), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7962), "Chicken Drumstick Baked" },
                    { new Guid("937b32b8-58ce-4fbf-8cdd-973ce6548ba5"), "48", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8002), "5", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(8002), "Apples (Without Skin)" },
                    { new Guid("94325018-7818-4cac-8b8c-bd63a1da30fa"), "183", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7992), "4", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7992), "Salmon Baked" },
                    { new Guid("94ddfb73-981b-414c-afc1-95f52a2db1e1"), "130", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7829), "1", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7833), "White Rice" },
                    { new Guid("9a18dc8b-fd11-4c9f-8035-bbc786343955"), "136", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7897), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7898), "Beef And Rice With Tomato-Based Sauce" },
                    { new Guid("b1c70256-ff12-4a85-af48-e6a13ff698b7"), "226", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7958), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7959), "Chicken Leg Drumstick And Thigh Baked" },
                    { new Guid("bb9e296d-6263-4d9b-9e8b-8b8e4685a425"), "192", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7948), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7948), "Chicken Breast Baked" },
                    { new Guid("bc5f0ac2-da85-49a0-bd09-571acaf03bf6"), "71", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7995), "5", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7995), "Fortified Fruit Juice Smoothie" },
                    { new Guid("bc608b00-4385-4182-a672-841dd43aaecb"), "52", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7998), "5", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7999), "Apples" },
                    { new Guid("c9adc489-5194-4c94-985b-11be8ce2c9f4"), "211", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7969), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7969), "Chicken Drumstick Fried" },
                    { new Guid("d5565c55-69c0-4e31-bb8d-d84ee915e731"), "117", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7975), "4", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7976), "Smoked Salmon" },
                    { new Guid("d8fe8841-807c-477e-8511-7d968dcdfb9f"), "148", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7910), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7911), "Chicken Or Turkey And Rice No Sauce" },
                    { new Guid("e6975c9b-5b9e-40d9-8ad2-ffae7a98de9b"), "128", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7929), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7930), "Pork, Rice And Vegetables " },
                    { new Guid("eafdd572-a637-4c76-9ae3-68948ebd0f32"), "157", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7939), "1", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7939), "Pasta Cooked" },
                    { new Guid("ee605fec-1ea3-456d-8ec8-19e31110e799"), "176", new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7951), "2", null, new DateTime(2024, 3, 24, 20, 2, 25, 135, DateTimeKind.Utc).AddTicks(7951), "Chicken Breast Grilled" }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPrograms",
                columns: new[] { "Id", "Category", "ExerciseName", "ExpertiseLevel" },
                values: new object[,]
                {
                    { new Guid("0e11d2aa-d67c-4bf6-ad3b-33318454bb9f"), 4, "Intermediate Fat Loss Workout", 2 },
                    { new Guid("16cfe627-93a2-4e20-a572-33c8c006af57"), 2, "45 Minute HIIT Workout", 3 },
                    { new Guid("17a3497c-0090-4a79-8cb9-59e4e96ce6a6"), 1, "Intermediate Fat Loss Workout", 3 },
                    { new Guid("1887507d-5fc1-4bcb-a1d7-166242fce8e8"), 1, "45 Minute HIIT Workout", 2 },
                    { new Guid("261e785b-3ec4-4483-a3f2-b2ed6b6e1376"), 1, "45 Minute HIIT Workout", 3 },
                    { new Guid("28a9d26c-de7e-469f-8a49-211e6464d64f"), 1, "Intermediate Fat Loss Workout", 2 }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPrograms",
                columns: new[] { "Id", "Category", "ExerciseName", "ExpertiseLevel" },
                values: new object[,]
                {
                    { new Guid("53b3cf8f-3485-4cd3-a0fb-0e1b5daaf3d1"), 3, "Beginner Strength Training Workout", 1 },
                    { new Guid("57dcb18d-04ba-4d8f-932e-fb3ff61c872a"), 1, "20 Minute HIIT Workout", 2 },
                    { new Guid("681963c0-08df-4456-a565-7393b67bf85f"), 1, "Beginner At Home Workout", 1 },
                    { new Guid("7f99121e-6a2a-4bc2-a0d3-add339426625"), 1, "30 Minute Cardio Workout", 1 },
                    { new Guid("8ff32807-00dd-4d41-a009-9f9852ce4ea8"), 4, "45 Minute HIIT Workout", 2 },
                    { new Guid("9c1753ff-1754-4311-82ef-e1a2e364370c"), 4, "Intermediate Fat Loss Workout", 3 },
                    { new Guid("a83a647e-f7dc-456c-ac44-a99366477e53"), 4, "45 Minute HIIT Workout", 3 },
                    { new Guid("e1516370-df7c-4f0e-9d1c-4c5ad25749aa"), 2, "45 Minute HIIT Workout", 2 },
                    { new Guid("f6f9ecc1-d1e1-4ed8-9548-c57c15310dec"), 4, "20 Minute HIIT Workout", 2 }
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
                name: "Meals");

            migrationBuilder.DropTable(
                name: "VerificationOtps");

            migrationBuilder.DropTable(
                name: "WorkoutPrograms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
