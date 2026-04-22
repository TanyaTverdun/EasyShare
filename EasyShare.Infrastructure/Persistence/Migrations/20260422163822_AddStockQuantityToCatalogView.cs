using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStockQuantityToCatalogView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_item_catalog;
                CREATE VIEW vw_item_catalog AS
        
                WITH ItemRatings AS (
                    SELECT 
                        b.item_id, 
                        ROUND(AVG(r.rating)::numeric, 1) AS average_rating,
                        COUNT(r.review_id) AS reviews_count
                    FROM bookings b
                    JOIN reviews r ON b.booking_id = r.booking_id
                    GROUP BY b.item_id
                )

                SELECT 
                    i.item_id,
                    i.type_id,
                    it.category_id,
                    i.name,
                    i.description,
                    i.price,
                    i.image_url,
                    it.name AS type_name,
                    c.name AS company_name,
                    loc.city,
                    loc.street,
                    loc.building,
                    i.stock_quantity,
                    COALESCE(ir.average_rating, 0.0) AS average_rating,
                    COALESCE(ir.reviews_count, 0) AS reviews_count
                FROM items i
                JOIN item_types it ON i.type_id = it.type_id
                JOIN companies c ON i.company_id = c.company_id
                JOIN locations loc ON i.location_id = loc.location_id
                LEFT JOIN ItemRatings ir ON i.item_id = ir.item_id
                WHERE i.is_active = true;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_item_catalog;
                CREATE VIEW vw_item_catalog AS
        
                WITH ItemRatings AS (
                    SELECT 
                        b.item_id, 
                        ROUND(AVG(r.rating)::numeric, 1) AS average_rating,
                        COUNT(r.review_id) AS reviews_count
                    FROM bookings b
                    JOIN reviews r ON b.booking_id = r.booking_id
                    GROUP BY b.item_id
                )

                SELECT 
                    i.item_id,
                    i.type_id,
                    it.category_id,
                    i.name,
                    i.description,
                    i.price,
                    i.image_url,
                    it.name AS type_name,
                    c.name AS company_name,
                    loc.city,
                    loc.street,
                    loc.building,
                    COALESCE(ir.average_rating, 0.0) AS average_rating,
                    COALESCE(ir.reviews_count, 0) AS reviews_count
                FROM items i
                JOIN item_types it ON i.type_id = it.type_id
                JOIN companies c ON i.company_id = c.company_id
                JOIN locations loc ON i.location_id = loc.location_id
                LEFT JOIN ItemRatings ir ON i.item_id = ir.item_id
                WHERE i.is_active = true;
            ");
        }
    }
}
