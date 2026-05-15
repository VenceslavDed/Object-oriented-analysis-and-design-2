public class Flight {
    private int id;
    private String flightNumber;
    private String origin;
    private String destination;
    private double basePrice;

    public Flight(int id, String flightNumber, String origin, String destination, double basePrice) {
        this.id = id;
        this.flightNumber = flightNumber;
        this.origin = origin;
        this.destination = destination;
        this.basePrice = basePrice;
    }

    public int getId() { return id; }
    public String getFlightNumber() { return flightNumber; }
    public String getOrigin() { return origin; }
    public String getDestination() { return destination; }
    public double getBasePrice() { return basePrice; }

    public boolean isAvailable() { return basePrice > 0; }

    @Override
    public String toString() {
        return flightNumber + " | " + origin + " → " + destination + " | $" + basePrice;
    }
}