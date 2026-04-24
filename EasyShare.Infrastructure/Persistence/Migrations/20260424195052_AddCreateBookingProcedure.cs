using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateBookingProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

                    SELECT COALESCE(SUM(quantity), 0)
                    INTO v_booked
                    FROM bookings
                    WHERE item_id = p_item_id
                      AND status IN (
                          'PendingConfirmation'::booking_status, 
                          'Confirmed'::booking_status, 
                          'Active'::booking_status, 
                          'PendingReturn'::booking_status
                      )
                      AND start_date < p_end_date
                      AND end_date > p_start_date;

                    v_available := v_stock - v_booked;
                    IF v_available < p_quantity THEN
                        RAISE EXCEPTION 'Overbooking';
                    END IF;

                    INSERT INTO bookings (
                        item_id, user_id, start_date, end_date,
                        quantity, status, created_at
                    )
                    VALUES (
                        p_item_id, p_user_id, p_start_date, p_end_date,
                        p_quantity, 'PendingConfirmation'::booking_status, NOW()
                    );
                END;
                $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_create_booking_safe;");
        }
    }
}
