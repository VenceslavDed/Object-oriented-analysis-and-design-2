import java.util.List;

public interface IFlightProvider {
    List<Flight> fetchFlights(String from, String to);
    boolean confirmReservation(String flightId);
}