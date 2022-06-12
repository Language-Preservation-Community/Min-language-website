using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLanguageDictionaryWebsite.Data.Migrations
{
    public partial class Dictinary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictionaryModel",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryModel", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "VocabularyModel",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishMeaning = table.Column<string>(nullable: true),
                    ExampleSentences = table.Column<string>(nullable: true),
                    AdditionalNote = table.Column<string>(nullable: true),
                    DictionaryModelKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocabularyModel", x => x.Key);
                    table.ForeignKey(
                        name: "FK_VocabularyModel_DictionaryModel_DictionaryModelKey",
                        column: x => x.DictionaryModelKey,
                        principalTable: "DictionaryModel",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryModel",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(nullable: true),
                    VocabularyModelKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryModel", x => x.Key);
                    table.ForeignKey(
                        name: "FK_CategoryModel_VocabularyModel_VocabularyModelKey",
                        column: x => x.VocabularyModelKey,
                        principalTable: "VocabularyModel",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionalPronunciationModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hanji = table.Column<string>(nullable: true),
                    Pronunciation = table.Column<string>(nullable: true),
                    AudioLink = table.Column<string>(nullable: true),
                    VocabularyModelKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalPronunciationModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegionalPronunciationModel_VocabularyModel_VocabularyModelKey",
                        column: x => x.VocabularyModelKey,
                        principalTable: "VocabularyModel",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionModel",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(nullable: true),
                    VocabularyModelKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionModel", x => x.Key);
                    table.ForeignKey(
                        name: "FK_RegionModel_VocabularyModel_VocabularyModelKey",
                        column: x => x.VocabularyModelKey,
                        principalTable: "VocabularyModel",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryModel_VocabularyModelKey",
                table: "CategoryModel",
                column: "VocabularyModelKey");

            migrationBuilder.CreateIndex(
                name: "IX_RegionalPronunciationModel_VocabularyModelKey",
                table: "RegionalPronunciationModel",
                column: "VocabularyModelKey");

            migrationBuilder.CreateIndex(
                name: "IX_RegionModel_VocabularyModelKey",
                table: "RegionModel",
                column: "VocabularyModelKey");

            migrationBuilder.CreateIndex(
                name: "IX_VocabularyModel_DictionaryModelKey",
                table: "VocabularyModel",
                column: "DictionaryModelKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryModel");

            migrationBuilder.DropTable(
                name: "RegionalPronunciationModel");

            migrationBuilder.DropTable(
                name: "RegionModel");

            migrationBuilder.DropTable(
                name: "VocabularyModel");

            migrationBuilder.DropTable(
                name: "DictionaryModel");
        }
    }
}
