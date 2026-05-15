import java.util.ArrayList;
import java.util.List;

// ПАТТЕРН Service Stub (Фиктивная служба)
public class FlightServiceStub implements IFlightProvider {
    private List<Flight> mockFlights;

    public FlightServiceStub() {
        mockFlights = new ArrayList<>();
        mockFlights.add(new Flight(1, "SU-101", "Москва", "Лондон", 250.00));
        mockFlights.add(new Flight(2, "SU-202", "Москва", "Париж", 180.00));
        mockFlights.add(new Flight(3, "SU-303", "Москва", "Берлин", 120.00));
        mockFlights.add(new Flight(4, "BA-400", "Лондон", "Париж", 100.00));
    }

    @Override
    public List<Flight> fetchFlights(String from, String to) {
        // Мгновенно возвращает список без запросов к API
        List<Flight> result = new ArrayList<>();
        for (Flight f : mockFlights) {
            if (f.getOrigin().equalsIgnoreCase(from) || from.isEmpty()) {
                result.add(f);
            }
        }
        return result;
    }

    @Override
    public boolean confirmReservation(String flightId) {
        System.out.println("[STUB] Бронирование рейса " + flightId + " подтверждено (заглушка)");
        return true;
    }
}