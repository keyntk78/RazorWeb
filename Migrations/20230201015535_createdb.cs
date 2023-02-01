using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using RazorWeb.Models;

#nullable disable

namespace RazorWeb.Migrations
{
    public partial class createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Creadted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ID);
                });


            // fake du lieu bang bogus

            Randomizer.Seed = new Random(8675309);

            var fakerArticle = new Faker<Article>();
            fakerArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5, 5));
            fakerArticle.RuleFor(a => a.Creadted, f => f.Date.Between(new DateTime(2023, 2, 1), new DateTime(2023, 2, 10)));
            fakerArticle.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1, 4));


            for (int i = 0; i < 10; i++)
            {
                Article article = fakerArticle.Generate();
                migrationBuilder.InsertData(
                    table: "Articles",
                    columns: new[] { "Title", "Creadted", "Content" },
                    values: new object[]
                    {
                    article.Title,
                    article.Creadted,
                    article.Content
                    }
                );
            }

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
