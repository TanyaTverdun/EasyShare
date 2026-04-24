using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEnumsToStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:billing_period.billing_period", "daily,monthly")
                .OldAnnotation("Npgsql:Enum:booking_status.booking_status", "pending_confirmation,confirmed,active,pending_return,completed");

            migrationBuilder.AlterColumn<string>(
                name: "billing_period",
                table: "items",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "bookings",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE sp_create_booking_safe(
                    p_user_id INT,
                    p_item_id INT,
                    p_quantity INT,
                    p_start_date TIMESTAMP WITH TIME ZONE,
                    p_end_date TIMESTAMP WITH TIME ZONE
                )
                LANGUAGE plpgsql
                AS $$
                DECLARE
                    v_stock INT;
                    v_booked INT;
                    v_available INT;
                BEGIN
                    SELECT stock_quantity INTO v_stock FROM items
                    WHERE item_id = p_item_id AND is_active = true FOR UPDATE;

                    IF NOT FOUND THEN RAISE EXCEPTION 'ItemNotFound'; END IF;

                    SELECT COALESCE(SUM(rented_quantity), 0) INTO v_booked
                    FROM bookings
                    WHERE item_id = p_item_id
                      AND status IN ('PendingConfirmation', 'Confirmed', 'Active', 'PendingReturn')
                      AND start_datetime < p_end_date
                      AND end_datetime > p_start_date;

                    v_available := v_stock - v_booked;
                    IF v_available < p_quantity THEN RAISE EXCEPTION 'Overbooking'; END IF;

                    INSERT INTO bookings (
                        item_id, user_id, start_datetime, end_datetime,
                        rented_quantity, total_price, status, created_at
                    )
                    VALUES (
                        p_item_id, p_user_id, p_start_date, p_end_date,
                        p_quantity, 0, 'PendingConfirmation', NOW()
                    );
                END;
                $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:billing_period.billing_period", "daily,monthly")
                .Annotation("Npgsql:Enum:booking_status.booking_status", "pending_confirmation,confirmed,active,pending_return,completed");

            migrationBuilder.AlterColumn<int>(
                name: "billing_period",
                table: "items",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "bookings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE sp_create_booking_safe(
                    p_user_id INT,
                    p_item_id INT,
                    p_quantity INT,
                    p_start_date TIMESTAMP WITH TIME ZONE,
                    p_end_date TIMESTAMP WITH TIME ZONE
                )
                LANGUAGE plpgsql
                AS $$
                DECLARE
                    v_stock INT;
                    v_booked INT;
                    v_available INT;
                BEGIN

                    SELECT stock_quantity
                    INTO v_stock
                    FROM items
                    WHERE item_id = p_item_id AND is_active = true
                    FOR UPDATE;

                    IF NOT FOUND THEN
                        RAISE EXCEPTION 'ItemNotFound';
                    END IF;

                    SELECT COALESCE(SUM(rented_quantity), 0)
                    INTO v_booked
                    FROM bookings
                    WHERE item_id = p_item_id
                      AND status IN (
                          'PendingConfirmation'::booking_status, 
                          'Confirmed'::booking_status, 
                          'Active'::booking_status, 
                          'PendingReturn'::booking_status
                      )
                      AND start_datetime < p_end_date
                      AND end_datetime > p_start_date;

                    v_available := v_stock - v_booked;
                    IF v_available < p_quantity THEN
                        RAISE EXCEPTION 'Overbooking';
                    END IF;

                    INSERT INTO bookings (
                        item_id, user_id, start_datetime, end_datetime,
                        rented_quantity, total_price, status, created_at
                    )
                    VALUES (
                        p_item_id, p_user_id, p_start_date, p_end_date,
                        p_quantity, 0, 'PendingConfirmation'::booking_status, NOW()
                    );

                END;
                $$;
            ");
        }
    }
}
