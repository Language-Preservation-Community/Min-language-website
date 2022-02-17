using Microsoft.EntityFrameworkCore.Migrations;

namespace MinLanguage.Data.Migrations
{
    public partial class Vocabs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_regionalPronunciation_Vocabs_VocabsId",
                table: "regionalPronunciation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vocabs",
                table: "Vocabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regionalPronunciation",
                table: "regionalPronunciation");

            migrationBuilder.DropIndex(
                name: "IX_regionalPronunciation_VocabsId",
                table: "regionalPronunciation");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "VocabsId",
                table: "regionalPronunciation");

            migrationBuilder.AddColumn<int>(
                name: "key",
                table: "Vocabs",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "regionalPronunciation",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "key",
                table: "regionalPronunciation",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Vocabskey",
                table: "regionalPronunciation",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vocabs",
                table: "Vocabs",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_regionalPronunciation",
                table: "regionalPronunciation",
                column: "key");

            migrationBuilder.CreateIndex(
                name: "IX_regionalPronunciation_Vocabskey",
                table: "regionalPronunciation",
                column: "Vocabskey");

            migrationBuilder.AddForeignKey(
                name: "FK_regionalPronunciation_Vocabs_Vocabskey",
                table: "regionalPronunciation",
                column: "Vocabskey",
                principalTable: "Vocabs",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_regionalPronunciation_Vocabs_Vocabskey",
                table: "regionalPronunciation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vocabs",
                table: "Vocabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regionalPronunciation",
                table: "regionalPronunciation");

            migrationBuilder.DropIndex(
                name: "IX_regionalPronunciation_Vocabskey",
                table: "regionalPronunciation");

            migrationBuilder.DropColumn(
                name: "key",
                table: "Vocabs");

            migrationBuilder.DropColumn(
                name: "key",
                table: "regionalPronunciation");

            migrationBuilder.DropColumn(
                name: "Vocabskey",
                table: "regionalPronunciation");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Vocabs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "regionalPronunciation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VocabsId",
                table: "regionalPronunciation",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vocabs",
                table: "Vocabs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_regionalPronunciation",
                table: "regionalPronunciation",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_regionalPronunciation_VocabsId",
                table: "regionalPronunciation",
                column: "VocabsId");

            migrationBuilder.AddForeignKey(
                name: "FK_regionalPronunciation_Vocabs_VocabsId",
                table: "regionalPronunciation",
                column: "VocabsId",
                principalTable: "Vocabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
