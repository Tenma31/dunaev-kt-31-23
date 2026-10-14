using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    c_discipline_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_discipline_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    c_discipline_is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.c_discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_specialty",
                columns: table => new
                {
                    c_specialty_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_specialty_title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    c_specialty_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_specialty_specialty_id", x => x.c_specialty_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_group",
                columns: table => new
                {
                    c_group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_group_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    c_group_course = table.Column<int>(type: "int", nullable: false),
                    f_specialty_id = table.Column<int>(type: "int", nullable: false),
                    c_group_is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_group_group_id", x => x.c_group_id);
                    table.ForeignKey(
                        name: "fk_f_specialty_id",
                        column: x => x.f_specialty_id,
                        principalTable: "cd_specialty",
                        principalColumn: "c_specialty_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    c_student_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_student_firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    c_student_lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    f_group_id = table.Column<int>(type: "int", nullable: false),
                    c_student_is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_student_student_id", x => x.c_student_id);
                    table.ForeignKey(
                        name: "fk_f_group_id",
                        column: x => x.f_group_id,
                        principalTable: "cd_group",
                        principalColumn: "c_group_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    c_grade_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_grade_value = table.Column<int>(type: "int", nullable: false),
                    f_student_id = table.Column<int>(type: "int", nullable: false),
                    f_discipline_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.c_grade_id);
                    table.ForeignKey(
                        name: "fk_f_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "c_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_f_student_id",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "c_student_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_f_discipline_id",
                table: "cd_grade",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_f_student_id",
                table: "cd_grade",
                column: "f_student_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_group_f_specialty_id",
                table: "cd_group",
                column: "f_specialty_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_f_group_id",
                table: "cd_student",
                column: "f_group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_group");

            migrationBuilder.DropTable(
                name: "cd_specialty");
        }
    }
}
