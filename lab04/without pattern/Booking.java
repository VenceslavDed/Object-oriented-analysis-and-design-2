public class Booking {
    private int id;
    private String date;
    private String status;
    private double totalAmount;

    public Booking(int id, String date, String status, double totalAmount) {
        this.id = id;
        this.date = date;
        this.status = status;
        this.totalAmount = totalAmount;
    }

    public double calculateTotal() { return totalAmount; }

    public boolean confirmBooking(IFlightProvider provider, String flightId) {
        return provider.confirmReservation(flightId);
    }

    public int getId() { return id; }
    public String getDate() { return date; }
    public String getStatus() { return status; }
    public double getTotalAmount() { return totalAmount; }

    @Override
    public String toString() {
        return "Бронь #" + id + " | " + date + " | " + status + " | $" + totalAmount;
    }
}