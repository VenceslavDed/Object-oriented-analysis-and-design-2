public class User {
    private int id;
    private String name;
    private String email;
    private int loyaltyPoints;

    public User(int id, String name, String email, int loyaltyPoints) {
        this.id = id;
        this.name = name;
        this.email = email;
        this.loyaltyPoints = loyaltyPoints;
    }

    public int getId() { return id; }
    public String getName() { return name; }
    public String getEmail() { return email; }
    public int getLoyaltyPoints() { return loyaltyPoints; }

    public String getInfo() {
        return "ID: " + id + " | " + name + " | " + email + " | Бонусы: " + loyaltyPoints;
    }
}