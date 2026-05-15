import java.sql.*;
import java.util.ArrayList;
import java.util.List;

// Реальная служба: работает с БД
// В боевом режиме здесь был бы HTTP-запрос к Amadeus API
public class RealAmadeusService implements IFlightProvider {
    private Connection connection;

    public RealAmadeusService(Connection connection) {
        this.connection = connection;
    }

    @Override
    public List<Flight> fetchFlights(String from, String to) {
        List<Flight> flights = new ArrayList<>();
        try {
            String query = "SELECT * FROM flights WHERE origin LIKE ? OR ? = ''";
            PreparedStatement ps = connection.prepareStatement(query);
            ps.setString(1, "%" + from + "%");
            ps.setString(2, from);
            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                flights.add(new Flight(
                    rs.getInt("id"),
                    rs.getString("flight_number"),
                    rs.getString("origin"),
                    rs.getString("destination"),
                    rs.getDouble("base_price")
                ));
            }
        } catch (SQLException e) {
            System.err.println("Ошибка БД: " + e.getMessage());
        }
        return flights;
    }

    @Override
    public boolean confirmReservation(String flightId) {
        // В реальности — запрос к Amadeus API + запись в БД
        System.out.println("[REAL] Запрос к реальному API для рейса " + flightId);
        return true;
    }

    public void saveBooking(int userId, int flightId, String passport, String seat, double amount) {
        try {
            String query = "INSERT INTO bookings (user_id, flight_id, booking_date, status, total_amount, passport_number, seat_number) VALUES (?, ?, CURDATE(), 'CONFIRMED', ?, ?, ?)";
            PreparedStatement ps = connection.prepareStatement(query);
            ps.setInt(1, userId);
            ps.setInt(2, flightId);
            ps.setDouble(3, amount);
            ps.setString(4, passport);
            ps.setString(5, seat);
            ps.executeUpdate();
        } catch (SQLException e) {
            System.err.println("Ошибка сохранения брони: " + e.getMessage());
        }
    }

    public List<User> getUsers() {
        List<User> users = new ArrayList<>();
        try {
            Statement st = connection.createStatement();
            ResultSet rs = st.executeQuery("SELECT * FROM users");
            while (rs.next()) {
                users.add(new User(
                    rs.getInt("id"),
                    rs.getString("name"),
                    rs.getString("email"),
                    rs.getInt("loyalty_points")
                ));
            }
        } catch (SQLException e) {
            System.err.println("Ошибка: " + e.getMessage());
        }
        return users;
    }

    public List<Booking> getBookings() {
        List<Booking> bookings = new ArrayList<>();
        try {
            String query = "SELECT b.id, b.booking_date, b.status, b.total_amount FROM bookings b";
            Statement st = connection.createStatement();
            ResultSet rs = st.executeQuery(query);
            while (rs.next()) {
                bookings.add(new Booking(
                    rs.getInt("id"),
                    rs.getString("booking_date"),
                    rs.getString("status"),
                    rs.getDouble("total_amount")
                ));
            }
        } catch (SQLException e) {
            System.err.println("Ошибка: " + e.getMessage());
        }
        return bookings;
    }
}