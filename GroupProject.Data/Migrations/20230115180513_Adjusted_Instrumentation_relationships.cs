using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupProject.Data.Migrations
{
    public partial class Adjusted_Instrumentation_relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compositions_Composers_ComposerId",
                table: "Compositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentations_Compositions_CompositionId",
                table: "Instrumentations");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentations_Instruments_InstrumentId",
                table: "Instrumentations");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_Compositions_CompositionEntityId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Instruments_CompositionEntityId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "CompositionEntityId",
                table: "Instruments");

            migrationBuilder.AlterColumn<int>(
                name: "InstrumentId",
                table: "Instrumentations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CompositionId",
                table: "Instrumentations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Compositions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ComposerId",
                table: "Compositions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Compositions_Composers_ComposerId",
                table: "Compositions",
                column: "ComposerId",
                principalTable: "Composers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentations_Compositions_CompositionId",
                table: "Instrumentations",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentations_Instruments_InstrumentId",
                table: "Instrumentations",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compositions_Composers_ComposerId",
                table: "Compositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentations_Compositions_CompositionId",
                table: "Instrumentations");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentations_Instruments_InstrumentId",
                table: "Instrumentations");

            migrationBuilder.AddColumn<int>(
                name: "CompositionEntityId",
                table: "Instruments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstrumentId",
                table: "Instrumentations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompositionId",
                table: "Instrumentations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Compositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComposerId",
                table: "Compositions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_CompositionEntityId",
                table: "Instruments",
                column: "CompositionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compositions_Composers_ComposerId",
                table: "Compositions",
                column: "ComposerId",
                principalTable: "Composers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentations_Compositions_CompositionId",
                table: "Instrumentations",
                column: "CompositionId",
                principalTable: "Compositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentations_Instruments_InstrumentId",
                table: "Instrumentations",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_Compositions_CompositionEntityId",
                table: "Instruments",
                column: "CompositionEntityId",
                principalTable: "Compositions",
                principalColumn: "Id");
        }
    }
}
