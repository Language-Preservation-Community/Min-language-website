using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLanguageDictionaryWebsite.Data.Migrations
{
    public partial class AddAllRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegionModel_VocabularyModel_VocabularyModelKey",
                table: "RegionModel");

            migrationBuilder.DropIndex(
                name: "IX_RegionModel_VocabularyModelKey",
                table: "RegionModel");

            migrationBuilder.DropColumn(
                name: "VocabularyModelKey",
                table: "RegionModel");

            migrationBuilder.AddColumn<int>(
                name: "DictionaryModelKey",
                table: "RegionModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegionModel_DictionaryModelKey",
                table: "RegionModel",
                column: "DictionaryModelKey");

            migrationBuilder.AddForeignKey(
                name: "FK_RegionModel_DictionaryModel_DictionaryModelKey",
                table: "RegionModel",
                column: "DictionaryModelKey",
                principalTable: "DictionaryModel",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegionModel_DictionaryModel_DictionaryModelKey",
                table: "RegionModel");

            migrationBuilder.DropIndex(
                name: "IX_RegionModel_DictionaryModelKey",
                table: "RegionModel");

            migrationBuilder.DropColumn(
                name: "DictionaryModelKey",
                table: "RegionModel");

            migrationBuilder.AddColumn<int>(
                name: "VocabularyModelKey",
                table: "RegionModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegionModel_VocabularyModelKey",
                table: "RegionModel",
                column: "VocabularyModelKey");

            migrationBuilder.AddForeignKey(
                name: "FK_RegionModel_VocabularyModel_VocabularyModelKey",
                table: "RegionModel",
                column: "VocabularyModelKey",
                principalTable: "VocabularyModel",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
