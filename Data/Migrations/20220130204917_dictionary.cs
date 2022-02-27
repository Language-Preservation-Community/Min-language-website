using Microsoft.EntityFrameworkCore.Migrations;

namespace MinLanguage.Data.Migrations
{
    public partial class dictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vocabs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hanji = table.Column<string>(nullable: true),
                    englishTranslation = table.Column<string>(nullable: true),
                    regionUsed = table.Column<string>(nullable: true),
                    exampleSentences = table.Column<string>(nullable: true),
                    wordClass = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vocabs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "regionalPronunciation",
                columns: table => new
                {
                    name = table.Column<string>(nullable: false),
                    pronunciation = table.Column<string>(nullable: true),
                    VocabsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regionalPronunciation", x => x.name);
                    table.ForeignKey(
                        name: "FK_regionalPronunciation_Vocabs_VocabsId",
                        column: x => x.VocabsId,
                        principalTable: "Vocabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_regionalPronunciation_VocabsId",
                table: "regionalPronunciation",
                column: "VocabsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "regionalPronunciation");

            migrationBuilder.DropTable(
                name: "Vocabs");
        }
    }
}
