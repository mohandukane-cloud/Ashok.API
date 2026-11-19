using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWeatherForecastId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherForecasts",
                table: "WeatherForecasts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherForecasts",
                table: "WeatherForecasts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherForecasts",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WeatherForecasts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherForecasts",
                table: "WeatherForecasts",
                column: "Date");
        }
    }
}
