using Microsoft.EntityFrameworkCore.Migrations;

namespace MinLanguage.Data.Migrations
{
    public partial class AddSuggest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_regionalPronunciation_Vocabs_Vocabskey",
                table: "regionalPronunciation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regionalPronunciation",
                table: "regionalPronunciation");

            migrationBuilder.DropColumn(
                name: "hanji",
                table: "Vocabs");

            migrationBuilder.RenameTable(
                name: "regionalPronunciation",
                newName: "RegionalPronunciation");

            migrationBuilder.RenameColumn(
                name: "wordClass",
                table: "Vocabs",
                newName: "WordClass");

            migrationBuilder.RenameColumn(
                name: "regionUsed",
                table: "Vocabs",
                newName: "RegionUsed");

            migrationBuilder.RenameColumn(
                name: "exampleSentences",
                table: "Vocabs",
                newName: "ExampleSentences");

            migrationBuilder.RenameColumn(
                name: "englishTranslation",
                table: "Vocabs",
                newName: "EnglishTranslation");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "Vocabs",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "Vocabs",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "pronunciation",
                table: "RegionalPronunciation",
                newName: "Pronunciation");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "RegionalPronunciation",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Vocabskey",
                table: "RegionalPronunciation",
                newName: "VocabsKey");

            migrationBuilder.RenameColumn(
                name: "key",
                table: "RegionalPronunciation",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_regionalPronunciation_Vocabskey",
                table: "RegionalPronunciation",
                newName: "IX_RegionalPronunciation_VocabsKey");

            migrationBuilder.AddColumn<string>(
                name: "Hanji",
                table: "RegionalPronunciation",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegionalPronunciation",
                table: "RegionalPronunciation",
                column: "Key");

            migrationBuilder.CreateTable(
                name: "VocabsSuggest",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VocabsKey = table.Column<int>(nullable: true),
                    EnglishTranslation = table.Column<string>(nullable: true),
                    RegionUsed = table.Column<string>(nullable: true),
                    ExampleSentences = table.Column<string>(nullable: true),
                    WordClass = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocabsSuggest", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "RegionalSuggest",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionalKey = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Pronunciation = table.Column<string>(nullable: true),
                    Hanji = table.Column<string>(nullable: true),
                    VocabsSuggestKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionalSuggest", x => x.Key);
                    table.ForeignKey(
                        name: "FK_RegionalSuggest_VocabsSuggest_VocabsSuggestKey",
                        column: x => x.VocabsSuggestKey,
                        principalTable: "VocabsSuggest",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegionalSuggest_VocabsSuggestKey",
                table: "RegionalSuggest",
                column: "VocabsSuggestKey");

            migrationBuilder.AddForeignKey(
                name: "FK_RegionalPronunciation_Vocabs_VocabsKey",
                table: "RegionalPronunciation",
                column: "VocabsKey",
                principalTable: "Vocabs",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegionalPronunciation_Vocabs_VocabsKey",
                table: "RegionalPronunciation");

            migrationBuilder.DropTable(
                name: "RegionalSuggest");

            migrationBuilder.DropTable(
                name: "VocabsSuggest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegionalPronunciation",
                table: "RegionalPronunciation");

            migrationBuilder.DropColumn(
                name: "Hanji",
                table: "RegionalPronunciation");

            migrationBuilder.RenameTable(
                name: "RegionalPronunciation",
                newName: "regionalPronunciation");

            migrationBuilder.RenameColumn(
                name: "WordClass",
                table: "Vocabs",
                newName: "wordClass");

            migrationBuilder.RenameColumn(
                name: "RegionUsed",
                table: "Vocabs",
                newName: "regionUsed");

            migrationBuilder.RenameColumn(
                name: "ExampleSentences",
                table: "Vocabs",
                newName: "exampleSentences");

            migrationBuilder.RenameColumn(
                name: "EnglishTranslation",
                table: "Vocabs",
                newName: "englishTranslation");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Vocabs",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Vocabs",
                newName: "key");

            migrationBuilder.RenameColumn(
                name: "VocabsKey",
                table: "regionalPronunciation",
                newName: "Vocabskey");

            migrationBuilder.RenameColumn(
                name: "Pronunciation",
                table: "regionalPronunciation",
                newName: "pronunciation");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "regionalPronunciation",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "regionalPronunciation",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_RegionalPronunciation_VocabsKey",
                table: "regionalPronunciation",
                newName: "IX_regionalPronunciation_Vocabskey");

            migrationBuilder.AddColumn<string>(
                name: "hanji",
                table: "Vocabs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_regionalPronunciation",
                table: "regionalPronunciation",
                column: "key");

            migrationBuilder.AddForeignKey(
                name: "FK_regionalPronunciation_Vocabs_Vocabskey",
                table: "regionalPronunciation",
                column: "Vocabskey",
                principalTable: "Vocabs",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
