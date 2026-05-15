public class Ticket {
    private String passportNumber;
    private String seatNumber;
    private double price;

    public Ticket(String passportNumber, String seatNumber, double price) {
        this.passportNumber = passportNumber;
        this.seatNumber = seatNumber;
        this.price = price;
    }

    public String getPassportNumber() { return passportNumber; }
    public String getSeatNumber() { return seatNumber; }
    public double getPrice() { return price; }
}