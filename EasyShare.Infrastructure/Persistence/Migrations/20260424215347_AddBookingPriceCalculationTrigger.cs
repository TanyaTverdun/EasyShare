using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingPriceCalculationTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION fn_calculate_booking_price()
                RETURNS TRIGGER AS $$
                DECLARE
                    v_item_price DECIMAL(10,2);
                    v_billing_period TEXT;
                    v_deposit_per_unit DECIMAL(10,2);
                    v_duration_hours FLOAT;
                    v_rental_cost DECIMAL(10,2);
                    v_total_deposit DECIMAL(10,2);
                BEGIN
                    SELECT price, billing_period, COALESCE(deposit_amount, 0)
                    INTO v_item_price, v_billing_period, v_deposit_per_unit
                    FROM items 
                    WHERE item_id = NEW.item_id;

                    v_duration_hours := EXTRACT(EPOCH FROM (NEW.end_datetime - NEW.start_datetime)) / 3600;

                    IF v_billing_period = 'Daily' THEN
                        v_rental_cost := v_item_price * CEIL(v_duration_hours / 24.0) * NEW.rented_quantity;
                    ELSIF v_billing_period = 'Hourly' THEN
                        v_rental_cost := v_item_price * CEIL(v_duration_hours) * NEW.rented_quantity;
                    ELSE
                        v_rental_cost := v_item_price * NEW.rented_quantity;
                    END IF;

                    v_total_deposit := v_deposit_per_unit * NEW.rented_quantity;

                    UPDATE bookings 
                    SET total_price = v_rental_cost + v_total_deposit
                    WHERE booking_id = NEW.booking_id;

                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS trg_calculate_booking_price ON bookings;
                CREATE TRIGGER trg_calculate_booking_price
                AFTER INSERT ON bookings
                FOR EACH ROW
                EXECUTE FUNCTION fn_calculate_booking_price();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_calculate_booking_price ON bookings;");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS fn_calculate_booking_price();");
        }
    }
}
