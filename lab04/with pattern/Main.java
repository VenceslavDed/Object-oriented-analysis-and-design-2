import java.sql.*;
import javax.swing.*;

public class Main {
    // Измени пароль на свой!
    private static final String DB_URL = "jdbc:mysql://localhost:3306/aviation_db?useSSL=false&allowPublicKeyRetrieval=true&serverTimezone=UTC";
    private static final String DB_USER = "root";
    private static final String DB_PASS = "1234";

    public static void main(String[] args) {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            Connection connection = DriverManager.getConnection(DB_URL, DB_USER, DB_PASS);
            System.out.println("✅ Подключение к БД успешно");

            SwingUtilities.invokeLater(() -> {
                MainFrame frame = new MainFrame(connection);
                frame.setVisible(true);
            });

        } catch (Exception e) {
            JOptionPane.showMessageDialog(null, "Ошибка подключения к БД:\n" + e.getMessage());
            e.printStackTrace();
        }
    }
}