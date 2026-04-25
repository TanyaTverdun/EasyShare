using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateBookingProcedureAndTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE sp_update_booking_safe(
                    p_booking_id INT,
                    p_new_start_datetime TIMESTAMPTZ,
                    p_new_end_datetime TIMESTAMPTZ,
                    p_new_quantity INT
                )
                LANGUAGE plpgsql
                AS $$
                DECLARE
                    v_item_id INT;
                    v_total_stock INT;
                    v_overlapping_count INT;
                    v_current_status TEXT;
                BEGIN
                    SELECT b.item_id, i.stock_quantity, b.status::TEXT 
                    INTO v_item_id, v_total_stock, v_current_status
                    FROM bookings b
                    JOIN items i ON b.item_id = i.item_id
                    WHERE b.booking_id = p_booking_id
                    FOR UPDATE;

                    IF v_current_status != 'PendingConfirmation' THEN
                        RAISE EXCEPTION 'Це бронювання вже не можна редагувати.';
                    END IF;

                    SELECT COALESCE(SUM(rented_quantity), 0)
                    INTO v_overlapping_count
                    FROM bookings
                    WHERE item_id = v_item_id
                      AND booking_id != p_booking_id
                      AND status IN (
                          'PendingConfirmation', 
                          'Confirmed', 
                          'Active', 
                          'PendingReturn'
                      ) 
                      AND start_datetime < p_new_end_datetime
                      AND end_datetime > p_new_start_datetime;

                    IF (v_total_stock - v_overlapping_count) < p_new_quantity THEN
                        RAISE EXCEPTION 'Недостатньо одиниць товару на обрані дати.';
                    END IF;

                    UPDATE bookings
                    SET start_datetime = p_new_start_datetime,
                        end_datetime = p_new_end_datetime,
                        rented_quantity = p_new_quantity
                    WHERE booking_id = p_booking_id;
                END;
                $$;
            ");

            // 2. Оновлюємо тригер ціни
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS trg_calculate_booking_price ON bookings;
        
                CREATE TRIGGER trg_calculate_booking_price
                AFTER INSERT OR UPDATE OF start_datetime, end_datetime, rented_quantity ON bookings
                FOR EACH ROW
                EXECUTE FUNCTION fn_calculate_booking_price();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_update_booking_safe;");

            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS trg_calculate_booking_price ON bookings;
                CREATE TRIGGER trg_calculate_booking_price
                AFTER INSERT ON bookings
                FOR EACH ROW
                EXECUTE FUNCTION fn_calculate_booking_price();
            ");
        }
    }
}
