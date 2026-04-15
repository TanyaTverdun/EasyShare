using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_items_location_id",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_ItemAttributeValues_attribute_id",
                table: "ItemAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_bookings_item_id",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_user_id",
                table: "bookings");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Reviews_Rating",
                table: "reviews",
                sql: "rating >= 1 AND rating <= 5");

            migrationBuilder.CreateIndex(
                name: "IX_items_location_id",
                table: "items",
                column: "location_id",
                filter: "is_active = true");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValues_attribute_id_value",
                table: "ItemAttributeValues",
                columns: new[] { "attribute_id", "value" });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_item_id_start_datetime_end_datetime",
                table: "bookings",
                columns: new[] { "item_id", "start_datetime", "end_datetime" });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_user_id_created_at",
                table: "bookings",
                columns: new[] { "user_id", "created_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Reviews_Rating",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_items_location_id",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_ItemAttributeValues_attribute_id_value",
                table: "ItemAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_bookings_item_id_start_datetime_end_datetime",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_user_id_created_at",
                table: "bookings");

            migrationBuilder.CreateIndex(
                name: "IX_items_location_id",
                table: "items",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValues_attribute_id",
                table: "ItemAttributeValues",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_item_id",
                table: "bookings",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_user_id",
                table: "bookings",
                column: "user_id");
        }
    }
}
