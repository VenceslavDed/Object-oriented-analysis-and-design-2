import tkinter as tk
from tkinter import ttk, messagebox
from abc import ABC, abstractmethod


#  Интерфейс IDeliveryStrategy

class IDeliveryStrategy(ABC):
    @abstractmethod
    def Calculate(self, weight: float, distance: float) -> float:
        pass



#   Стратегии


class CourierStrategy(IDeliveryStrategy):
    def Calculate(self, weight: float, distance: float) -> float:
        return 200 + weight * 50 + distance * 10


class PostStrategy(IDeliveryStrategy):
    def Calculate(self, weight: float, distance: float) -> float:
        return 100 + weight * 30 + distance * 5


class ExpressStrategy(IDeliveryStrategy):
    def Calculate(self, weight: float, distance: float) -> float:
        return 500 + weight * 80 + distance * 20


class PickupStrategy(IDeliveryStrategy):
    def Calculate(self, weight: float, distance: float) -> float:
        return 0.0



#  Контекст  DeliveryCalculator

class DeliveryCalculator:
    def __init__(self):
        self.weight: float = 0.0
        self.distance: float = 0.0
        self._strategy: IDeliveryStrategy = CourierStrategy()

    def SetStrategy(self, s: str):
        strategies = {
            "Курьер":    CourierStrategy(),
            "Почта":     PostStrategy(),
            "Экспресс":  ExpressStrategy(),
            "Самовывоз": PickupStrategy(),
        }
        self._strategy = strategies[s]

    def Calculate(self) -> float:
        return self._strategy.Calculate(self.weight, self.distance)



#  Цвета

ACCENT       = "#2c3e50"
ACCENT_HOVER = "#3d5166"
CARD_SEL     = "#ddeeff"
CARD_NORMAL  = "#f7f7f7"
BORDER_SEL   = "#2c3e50"
BORDER_NRM   = "#cccccc"

OPTIONS = [
    ("Курьер",    "Доставка курьером до двери"),
    ("Почта",     "Обычная почтовая доставка"),
    ("Экспресс",  "Срочная приоритетная доставка"),
    ("Самовывоз", "Забрать самостоятельно — бесплатно"),
]



#  GUI

class App(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("Калькулятор доставки  (с паттерном Стратегия)")
        self.resizable(False, False)
        self.configure(bg="#f0f0f0")

        self.calculator = DeliveryCalculator()
        self.selected   = "Курьер"
        self.card_frames = {}

        self._build_ui()

    # ----------------------------------------------------------

    def _build_ui(self):
        # Заголовок
        header = tk.Frame(self, bg=ACCENT, pady=14)
        header.pack(fill="x")
        tk.Label(header,
                 text="Калькулятор стоимости доставки",
                 font=("Segoe UI", 15, "bold"),
                 bg=ACCENT, fg="white").pack()
        tk.Label(header,
                 text="с паттерном Стратегия",
                 font=("Segoe UI", 9),
                 bg=ACCENT, fg="#aab7c4").pack()

        # Внешняя карточка
        outer = tk.Frame(self, bg="white",
                         highlightbackground="#ddd", highlightthickness=1)
        outer.pack(padx=20, pady=16, fill="both")

        tk.Label(outer, text="Выберите способ доставки",
                 font=("Segoe UI", 10, "bold"),
                 bg="white", fg="#333").pack(anchor="w", padx=16, pady=(12, 6))

        # Карточки
        for name, subtitle in OPTIONS:
            self._make_card(outer, name, subtitle)

        ttk.Separator(outer, orient="horizontal").pack(fill="x", padx=16, pady=12)

        # Поля ввода
        fields = tk.Frame(outer, bg="white")
        fields.pack(padx=16, fill="x")

        tk.Label(fields, text="Вес посылки (кг):",
                 font=("Segoe UI", 10), bg="white",
                 width=18, anchor="w").grid(row=0, column=0, pady=6, sticky="w")
        self.weight_var = tk.StringVar()
        tk.Entry(fields, textvariable=self.weight_var,
                 font=("Segoe UI", 11), width=14,
                 relief="solid").grid(row=0, column=1, pady=6, sticky="w")

        tk.Label(fields, text="Расстояние (км):",
                 font=("Segoe UI", 10), bg="white",
                 width=18, anchor="w").grid(row=1, column=0, pady=6, sticky="w")
        self.distance_var = tk.StringVar()
        tk.Entry(fields, textvariable=self.distance_var,
                 font=("Segoe UI", 11), width=14,
                 relief="solid").grid(row=1, column=1, pady=6, sticky="w")

        ttk.Separator(outer, orient="horizontal").pack(fill="x", padx=16, pady=8)

        # Кнопка
        tk.Button(outer,
                  text="   Рассчитать стоимость   ",
                  font=("Segoe UI", 11, "bold"),
                  bg=ACCENT, fg="white",
                  activebackground=ACCENT_HOVER,
                  relief="flat", cursor="hand2",
                  command=self._on_calculate).pack(pady=(0, 12))

        # Блок результата
        self.result_frame = tk.Frame(outer, bg="#eaf4ea",
                                     highlightbackground="#5cb85c",
                                     highlightthickness=1)
        self.result_frame.pack(padx=16, pady=(0, 16), fill="x")
        self.result_frame.pack_forget()

        self.result_label = tk.Label(self.result_frame, text="",
                                     font=("Segoe UI", 13, "bold"),
                                     bg="#eaf4ea", fg="#2d6a2d", pady=10)
        self.result_label.pack()

    # ----------------------------------------------------------

    def _make_card(self, parent, name: str, subtitle: str):
        is_first = (name == "Курьер")
        bg     = CARD_SEL   if is_first else CARD_NORMAL
        border = BORDER_SEL if is_first else BORDER_NRM

        frame = tk.Frame(parent, bg=bg,
                         highlightbackground=border,
                         highlightthickness=2,
                         cursor="hand2",
                         pady=10, padx=12)
        frame.pack(fill="x", padx=16, pady=3)
        self.card_frames[name] = frame

        # Цветная полоска слева (показывает выбор)
        bar = tk.Frame(frame, bg=ACCENT if is_first else bg,
                       width=4)
        bar.pack(side="left", fill="y", padx=(0, 10))
        frame._bar = bar

        # Текстовый блок
        text_col = tk.Frame(frame, bg=bg)
        text_col.pack(side="left", fill="x", expand=True)

        lbl_name = tk.Label(text_col, text=name,
                            font=("Segoe UI", 11, "bold"),
                            bg=bg, fg="#1a1a1a", anchor="w")
        lbl_name.pack(anchor="w")

        lbl_sub = tk.Label(text_col, text=subtitle,
                           font=("Segoe UI", 9),
                           bg=bg, fg="#777", anchor="w")
        lbl_sub.pack(anchor="w")

        # Галочка / кружок справа
        ind = tk.Label(frame,
                       text="✔" if is_first else "○",
                       font=("Segoe UI", 14),
                       bg=bg,
                       fg=ACCENT if is_first else "#aaa")
        ind.pack(side="right")
        frame._indicator = ind

        # Привязка клика ко всем дочерним виджетам
        for w in [frame, bar, text_col, lbl_name, lbl_sub, ind]:
            w.bind("<Button-1>", lambda e, n=name: self._select(n))

    # ----------------------------------------------------------

    def _select(self, name: str):
        if self.selected == name:
            return
        self.selected = name
        self.result_frame.pack_forget()

        for n, frame in self.card_frames.items():
            active = (n == name)
            bg     = CARD_SEL   if active else CARD_NORMAL
            border = BORDER_SEL if active else BORDER_NRM

            frame.config(bg=bg, highlightbackground=border)
            frame._bar.config(bg=ACCENT if active else bg)
            frame._indicator.config(
                text="✔" if active else "○",
                fg=ACCENT if active else "#aaa",
                bg=bg
            )
            self._set_bg_recursive(frame, bg)

        self.calculator.SetStrategy(name)

    def _set_bg_recursive(self, widget, bg):
        try:
            widget.config(bg=bg)
        except tk.TclError:
            pass
        for child in widget.winfo_children():
            self._set_bg_recursive(child, bg)

    # ----------------------------------------------------------

    def _on_calculate(self):
        try:
            w = float(self.weight_var.get().replace(",", "."))
            d = float(self.distance_var.get().replace(",", "."))
            if w < 0 or d < 0:
                raise ValueError
        except ValueError:
            messagebox.showerror(
                "Ошибка ввода",
                "Введите корректные числа (≥ 0) для веса и расстояния."
            )
            return

        self.calculator.SetStrategy(self.selected)
        self.calculator.weight   = w
        self.calculator.distance = d
        cost = self.calculator.Calculate()

        self.result_label.config(
            text=f"{self.selected}:  {cost:,.0f} руб."
        )
        self.result_frame.pack(padx=16, pady=(0, 16), fill="x")


if __name__ == "__main__":
    app = App()
    app.mainloop()