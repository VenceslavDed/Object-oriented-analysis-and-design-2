import java.awt.*;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import javax.swing.*;
import javax.swing.table.*;

// БЕЗ ПАТТЕРНА: MainFrame напрямую работает с БД через SQL
// Нет интерфейса, нет заглушки — только жёсткая привязка к MySQL
public class MainFrame extends JFrame {

    // БЕЗ ПАТТЕРНА: прямое хранение соединения с БД
    private Connection connection;

    private JTable flightsTable, bookingsTable;
    private DefaultTableModel flightsModel, bookingsModel;
    private JLabel modeLabel;
    private JComboBox<String> userCombo;

    public MainFrame(Connection connection) {
        this.connection = connection;

        setTitle("✈ Система бронирования авиабилетов");
        setSize(950, 700);
        setMinimumSize(new Dimension(800, 600));
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        buildUI();
        loadUsers();
        loadFlights();
    }

    private void buildUI() {
        JPanel main = new JPanel(new BorderLayout(8, 8));
        main.setBackground(new Color(25, 35, 55));
        main.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
        setContentPane(main);

        // ── ВЕРХНЯЯ ПАНЕЛЬ ──────────────────────────────────
        JPanel topPanel = new JPanel(new FlowLayout(FlowLayout.LEFT, 12, 10));
        topPanel.setBackground(new Color(15, 22, 40));
        topPanel.setBorder(BorderFactory.createMatteBorder(0, 0, 1, 0, new Color(60, 80, 130)));

        // БЕЗ ПАТТЕРНА: нет переключения режимов — только реальная БД
        modeLabel = new JLabel("● РЕАЛЬНАЯ БАЗА ДАННЫХ (MySQL)");
        modeLabel.setForeground(new Color(255, 160, 60));
        modeLabel.setFont(new Font("Consolas", Font.BOLD, 14));

        topPanel.add(modeLabel);
        main.add(topPanel, BorderLayout.NORTH);

        // ── ЦЕНТР: таблица рейсов ────────────────────────────
        flightsModel = new DefaultTableModel(
            new String[]{"ID", "Рейс", "Откуда", "Куда", "Цена ($)"}, 0) {
            public boolean isCellEditable(int r, int c) { return false; }
        };
        flightsTable = new JTable(flightsModel);
        styleTable(flightsTable);
        flightsTable.getColumnModel().getColumn(0).setMaxWidth(50);
        flightsTable.getColumnModel().getColumn(1).setPreferredWidth(100);
        flightsTable.getColumnModel().getColumn(4).setPreferredWidth(90);

        JScrollPane flightsScroll = new JScrollPane(flightsTable);
        flightsScroll.getViewport().setBackground(new Color(35, 47, 70));

        JPanel centerPanel = titledPanel("✈  Доступные рейсы", new Color(100, 160, 255));
        centerPanel.add(flightsScroll, BorderLayout.CENTER);
        main.add(centerPanel, BorderLayout.CENTER);

        // ── НИЖНЯЯ ЧАСТЬ ─────────────────────────────────────
        JPanel bottomPanel = new JPanel(new BorderLayout(8, 8));
        bottomPanel.setBackground(new Color(25, 35, 55));
        bottomPanel.setPreferredSize(new Dimension(0, 240));

        JPanel bookForm = new JPanel(new FlowLayout(FlowLayout.LEFT, 10, 10));
        bookForm.setBackground(new Color(20, 28, 48));

        userCombo = new JComboBox<>();
        userCombo.setPreferredSize(new Dimension(150, 26));
        JTextField passportField = new JTextField("AB123456", 10);
        JTextField seatField = new JTextField("12A", 5);
        JButton bookBtn = styledButton("✔  Забронировать", new Color(180, 80, 30));

        bookForm.add(whiteLabel("Пассажир:"));
        bookForm.add(userCombo);
        bookForm.add(whiteLabel("Паспорт:"));
        bookForm.add(passportField);
        bookForm.add(whiteLabel("Место:"));
        bookForm.add(seatField);
        bookForm.add(bookBtn);

        bookBtn.addActionListener(e -> {
            int row = flightsTable.getSelectedRow();
            if (row < 0) {
                JOptionPane.showMessageDialog(this, "Сначала выберите рейс из таблицы!");
                return;
            }
            int flightId = (int) flightsModel.getValueAt(row, 0);
            double price  = (double) flightsModel.getValueAt(row, 4);
            int userIdx = userCombo.getSelectedIndex();
            if (userIdx < 0) return;

            // БЕЗ ПАТТЕРНА: прямой SQL-запрос прямо в обработчике кнопки
            try {
                String query = "INSERT INTO bookings (user_id, flight_id, booking_date, status, total_amount, passport_number, seat_number) VALUES (?, ?, CURDATE(), 'CONFIRMED', ?, ?, ?)";
                PreparedStatement ps = connection.prepareStatement(query);
                // БЕЗ ПАТТЕРНА: получаем userId напрямую из БД по индексу
                Statement st = connection.createStatement();
                ResultSet rs = st.executeQuery("SELECT id, name FROM users LIMIT 100");
                List<Integer> ids = new ArrayList<>();
                while (rs.next()) ids.add(rs.getInt("id"));
                int userId = ids.get(userIdx);

                ps.setInt(1, userId);
                ps.setInt(2, flightId);
                ps.setDouble(3, price);
                ps.setString(4, passportField.getText());
                ps.setString(5, seatField.getText());
                ps.executeUpdate();

                loadBookings();
                JOptionPane.showMessageDialog(this,
                    "✅ Бронирование подтверждено!\n\n" +
                    "Рейс ID  : " + flightId + "\n" +
                    "Место    : " + seatField.getText() + "\n" +
                    "Сумма    : $" + price +
                    "\n\n[Сохранено в базе данных]");
            } catch (SQLException ex) {
                JOptionPane.showMessageDialog(this, "Ошибка БД: " + ex.getMessage());
            }
        });

        JPanel bookFormWrapper = titledPanel("📋  Забронировать", new Color(255, 160, 60));
        bookFormWrapper.add(bookForm, BorderLayout.CENTER);
        bookFormWrapper.setPreferredSize(new Dimension(0, 75));

        bookingsModel = new DefaultTableModel(
            new String[]{"ID", "Дата", "Статус", "Сумма ($)"}, 0) {
            public boolean isCellEditable(int r, int c) { return false; }
        };
        bookingsTable = new JTable(bookingsModel);
        styleTable(bookingsTable);

        JScrollPane bookingsScroll = new JScrollPane(bookingsTable);
        bookingsScroll.getViewport().setBackground(new Color(35, 47, 70));

        JPanel bookingsPanel = titledPanel("📜  История бронирований", new Color(80, 220, 130));
        bookingsPanel.add(bookingsScroll, BorderLayout.CENTER);

        bottomPanel.add(bookFormWrapper, BorderLayout.NORTH);
        bottomPanel.add(bookingsPanel, BorderLayout.CENTER);
        main.add(bottomPanel, BorderLayout.SOUTH);
    }

    // БЕЗ ПАТТЕРНА: прямой SQL-запрос для загрузки рейсов
    private void loadFlights() {
        flightsModel.setRowCount(0);
        try {
            Statement st = connection.createStatement();
            ResultSet rs = st.executeQuery("SELECT * FROM flights");
            while (rs.next()) {
                flightsModel.addRow(new Object[]{
                    rs.getInt("id"),
                    rs.getString("flight_number"),
                    rs.getString("origin"),
                    rs.getString("destination"),
                    rs.getDouble("base_price")
                });
            }
        } catch (SQLException e) {
            JOptionPane.showMessageDialog(this, "Ошибка загрузки рейсов: " + e.getMessage());
        }
    }

    // БЕЗ ПАТТЕРНА: прямой SQL-запрос для загрузки пользователей
    private void loadUsers() {
        userCombo.removeAllItems();
        try {
            Statement st = connection.createStatement();
            ResultSet rs = st.executeQuery("SELECT * FROM users");
            while (rs.next()) {
                userCombo.addItem(rs.getString("name"));
            }
        } catch (SQLException e) {
            JOptionPane.showMessageDialog(this, "Ошибка загрузки пользователей: " + e.getMessage());
        }
    }

    // БЕЗ ПАТТЕРНА: прямой SQL-запрос для истории бронирований
    private void loadBookings() {
        bookingsModel.setRowCount(0);
        try {
            Statement st = connection.createStatement();
            ResultSet rs = st.executeQuery("SELECT id, booking_date, status, total_amount FROM bookings");
            while (rs.next()) {
                bookingsModel.addRow(new Object[]{
                    rs.getInt("id"),
                    rs.getString("booking_date"),
                    rs.getString("status"),
                    rs.getDouble("total_amount")
                });
            }
        } catch (SQLException e) {
            JOptionPane.showMessageDialog(this, "Ошибка загрузки бронирований: " + e.getMessage());
        }
    }

    private JLabel whiteLabel(String text) {
        JLabel l = new JLabel(text);
        l.setForeground(Color.WHITE);
        l.setFont(new Font("Segoe UI", Font.PLAIN, 13));
        return l;
    }

    private JButton styledButton(String text, Color bg) {
        JButton btn = new JButton(text);
        btn.setBackground(bg);
        btn.setForeground(Color.WHITE);
        btn.setFocusPainted(false);
        btn.setFont(new Font("Segoe UI", Font.BOLD, 12));
        btn.setBorder(BorderFactory.createEmptyBorder(5, 12, 5, 12));
        return btn;
    }

    private JPanel titledPanel(String title, Color borderColor) {
        JPanel p = new JPanel(new BorderLayout());
        p.setBackground(new Color(28, 38, 58));
        p.setBorder(BorderFactory.createTitledBorder(
            BorderFactory.createLineBorder(borderColor, 1),
            title, 0, 0,
            new Font("Segoe UI", Font.BOLD, 12), borderColor));
        return p;
    }

    private void styleTable(JTable table) {
        table.setBackground(new Color(38, 50, 72));
        table.setForeground(new Color(220, 230, 255));
        table.setGridColor(new Color(55, 70, 100));
        table.setFont(new Font("Segoe UI", Font.PLAIN, 13));
        table.setRowHeight(26);
        table.setSelectionBackground(new Color(60, 100, 180));
        table.setSelectionForeground(Color.WHITE);
        JTableHeader header = table.getTableHeader();
        header.setBackground(new Color(18, 26, 46));
        header.setForeground(new Color(140, 190, 255));
        header.setFont(new Font("Segoe UI", Font.BOLD, 13));
    }
}