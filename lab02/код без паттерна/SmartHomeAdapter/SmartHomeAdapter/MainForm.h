#pragma once
#include "SmartHome.h"


namespace NoPattern {

    using namespace System;
    using namespace System::Windows::Forms;
    using namespace System::Drawing;
    using namespace System::Drawing::Drawing2D;

    // ── Карточка устройства ──────────────────────────────────
    public ref class DeviceCard : public Panel {
    public:
        String^ deviceName;
        String^ deviceId;
        bool    isOn;
        int     deviceType; // 0=лампа 1=чайник 2=телевизор

        DeviceCard(String^ name, String^ id, int type) {
            deviceName = name;
            deviceId = id;
            deviceType = type;
            isOn = false;
            this->Size = Drawing::Size(160, 160);
            this->BackColor = Color::FromArgb(40, 40, 40);
            this->Cursor = Cursors::Hand;
            this->Paint += gcnew PaintEventHandler(this, &DeviceCard::OnPaint);
        }

        void SetState(bool on) {
            isOn = on;
            this->Invalidate();
        }

    private:
        void OnPaint(Object^ sender, PaintEventArgs^ e) {
            Graphics^ g = e->Graphics;
            g->SmoothingMode = SmoothingMode::AntiAlias;

            Color bg = isOn
                ? Color::FromArgb(50, 50, 30)
                : Color::FromArgb(40, 40, 40);
            g->Clear(bg);

            Color border = isOn
                ? Color::FromArgb(255, 200, 50)
                : Color::FromArgb(80, 80, 80);
            Pen^ pen = gcnew Pen(border, 2);
            g->DrawRectangle(pen, 1, 1, this->Width - 3, this->Height - 3);

            if (deviceType == 0) DrawLamp(g);
            if (deviceType == 1) DrawKettle(g);
            if (deviceType == 2) DrawTV(g);

            SolidBrush^ textBrush = gcnew SolidBrush(
                isOn ? Color::FromArgb(255, 220, 80) : Color::FromArgb(160, 160, 160));
            Drawing::Font^ f = gcnew Drawing::Font("Segoe UI", 9.0f, FontStyle::Bold);
            StringFormat^ sf = gcnew StringFormat();
            sf->Alignment = StringAlignment::Center;
            sf->LineAlignment = StringAlignment::Far;
            g->DrawString(deviceName, f, textBrush,
                RectangleF(0, 0, (float)this->Width, (float)this->Height - 8), sf);

            String^ status = isOn ? "ВКЛ" : "ВЫКЛ";
            Color statusColor = isOn
                ? Color::FromArgb(100, 220, 100)
                : Color::FromArgb(120, 120, 120);
            SolidBrush^ sb = gcnew SolidBrush(statusColor);
            Drawing::Font^ fs = gcnew Drawing::Font("Segoe UI", 8.0f);
            StringFormat^ sf2 = gcnew StringFormat();
            sf2->Alignment = StringAlignment::Center;
            g->DrawString(status, fs, sb,
                RectangleF(0, (float)this->Height - 22, (float)this->Width, 20), sf2);
        }

        void DrawLamp(Graphics^ g) {
            int cx = this->Width / 2;
            if (isOn) {
                for (int r = 55; r >= 20; r -= 5) {
                    int alpha = (55 - r) * 6;
                    SolidBrush^ glow = gcnew SolidBrush(Color::FromArgb(alpha, 255, 220, 50));
                    g->FillEllipse(glow, cx - r, 28 - r + 20, r * 2, r * 2);
                }
            }
            Color bulbColor = isOn
                ? Color::FromArgb(255, 240, 100)
                : Color::FromArgb(80, 80, 80);
            SolidBrush^ bulb = gcnew SolidBrush(bulbColor);
            g->FillEllipse(bulb, cx - 22, 25, 44, 44);
            SolidBrush^ base = gcnew SolidBrush(Color::FromArgb(120, 120, 120));
            g->FillRectangle(base, cx - 10, 67, 20, 8);
            g->FillRectangle(base, cx - 8, 75, 16, 5);
            g->FillRectangle(base, cx - 6, 80, 12, 4);
        }

        void DrawKettle(Graphics^ g) {
            int cx = this->Width / 2;
            if (isOn) {
                Pen^ steam = gcnew Pen(Color::FromArgb(180, 200, 200, 220), 2);
                steam->DashStyle = DashStyle::Dot;
                g->DrawLine(steam, cx - 8, 28, cx - 10, 18);
                g->DrawLine(steam, cx, 25, cx, 15);
                g->DrawLine(steam, cx + 8, 28, cx + 10, 18);
            }
            Color bodyColor = isOn
                ? Color::FromArgb(220, 120, 60)
                : Color::FromArgb(80, 80, 80);
            SolidBrush^ body = gcnew SolidBrush(bodyColor);
            array<Point>^ pts = gcnew array<Point> {
                Point(cx - 22, 70), Point(cx - 25, 38),
                    Point(cx + 25, 38), Point(cx + 22, 70)
            };
            g->FillPolygon(body, pts);
            array<Point>^ spout = gcnew array<Point> {
                Point(cx + 22, 45), Point(cx + 38, 40),
                    Point(cx + 36, 50), Point(cx + 20, 55)
            };
            g->FillPolygon(body, spout);
            Pen^ handle = gcnew Pen(Color::FromArgb(100, 100, 100), 3);
            g->DrawArc(handle, cx - 38, 38, 20, 30, -30, 180);
            SolidBrush^ lid = gcnew SolidBrush(Color::FromArgb(100, 100, 100));
            g->FillRectangle(lid, cx - 18, 33, 36, 7);
        }

        void DrawTV(Graphics^ g) {
            int cx = this->Width / 2;
            Color screenColor = isOn
                ? Color::FromArgb(40, 120, 200)
                : Color::FromArgb(30, 30, 30);
            SolidBrush^ screen = gcnew SolidBrush(screenColor);
            g->FillRectangle(screen, cx - 35, 28, 70, 45);
            Pen^ frame = gcnew Pen(Color::FromArgb(100, 100, 100), 2);
            g->DrawRectangle(frame, cx - 35, 28, 70, 45);
            if (isOn) {
                SolidBrush^ white = gcnew SolidBrush(Color::FromArgb(200, 255, 255, 255));
                Drawing::Font^ f = gcnew Drawing::Font("Segoe UI", 7.0f);
                StringFormat^ sf = gcnew StringFormat();
                sf->Alignment = StringAlignment::Center;
                sf->LineAlignment = StringAlignment::Center;
                g->DrawString("▶  CH 1", f, white,
                    RectangleF((float)(cx - 35), 28, 70, 45), sf);
            }
            SolidBrush^ stand = gcnew SolidBrush(Color::FromArgb(100, 100, 100));
            g->FillRectangle(stand, cx - 4, 73, 8, 10);
            g->FillRectangle(stand, cx - 16, 83, 32, 5);
            Color ledColor = isOn
                ? Color::FromArgb(50, 255, 50)
                : Color::FromArgb(255, 50, 50);
            SolidBrush^ led = gcnew SolidBrush(ledColor);
            g->FillEllipse(led, cx + 28, 66, 5, 5);
        }
    };


    // ── Главная форма ────────────────────────────────────────
    public ref class MainForm : public Form {

        SmartHomeController* controller;

        DeviceCard^ cardLight;
        DeviceCard^ cardKettle;
        DeviceCard^ cardTV;

        Button^ btnOn;
        Button^ btnOff;
        Label^ lblTitle;
        Label^ lblSelected;

        DeviceCard^ selectedCard;
        String^ selectedId;

    public:
        MainForm() {
            controller = new SmartHomeController();
            selectedId = "light";
            InitializeComponent();
        }

    protected:
        ~MainForm() {
            delete controller;
        }

    private:
        void InitializeComponent() {
            this->Text = L"Умный дом — Без паттерна";
            this->Size = Drawing::Size(580, 360);
            this->StartPosition = FormStartPosition::CenterScreen;
            this->BackColor = Color::FromArgb(28, 28, 28);
            this->Font = gcnew Drawing::Font("Segoe UI", 9.5f);
            this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
            this->MaximizeBox = false;

            lblTitle = gcnew Label();
            lblTitle->Text = "Умный дом — без паттерна";
            lblTitle->Font = gcnew Drawing::Font("Segoe UI", 12.0f, FontStyle::Bold);
            lblTitle->ForeColor = Color::FromArgb(220, 220, 220);
            lblTitle->Location = Point(20, 15);
            lblTitle->AutoSize = true;

            lblSelected = gcnew Label();
            lblSelected->Text = "Выберите устройство";
            lblSelected->ForeColor = Color::FromArgb(140, 140, 140);
            lblSelected->Location = Point(20, 45);
            lblSelected->AutoSize = true;

            cardLight = gcnew DeviceCard("Лампа Philips", "light", 0);
            cardKettle = gcnew DeviceCard("Чайник Xiaomi", "kettle", 1);
            cardTV = gcnew DeviceCard("Телевизор Samsung", "tv", 2);

            cardLight->Location = Point(20, 75);
            cardKettle->Location = Point(200, 75);
            cardTV->Location = Point(380, 75);

            cardLight->Click += gcnew EventHandler(this, &MainForm::OnSelectLight);
            cardKettle->Click += gcnew EventHandler(this, &MainForm::OnSelectKettle);
            cardTV->Click += gcnew EventHandler(this, &MainForm::OnSelectTV);

            selectedCard = cardLight;
            HighlightSelected();

            btnOn = gcnew Button();
            btnOn->Text = "Включить";
            btnOn->Location = Point(20, 255);
            btnOn->Size = Drawing::Size(130, 40);
            btnOn->BackColor = Color::FromArgb(76, 175, 80);
            btnOn->ForeColor = Color::White;
            btnOn->FlatStyle = FlatStyle::Flat;
            btnOn->FlatAppearance->BorderSize = 0;
            btnOn->Font = gcnew Drawing::Font("Segoe UI", 10.0f, FontStyle::Bold);
            btnOn->Click += gcnew EventHandler(this, &MainForm::OnOn);

            btnOff = gcnew Button();
            btnOff->Text = "Выключить";
            btnOff->Location = Point(165, 255);
            btnOff->Size = Drawing::Size(130, 40);
            btnOff->BackColor = Color::FromArgb(244, 67, 54);
            btnOff->ForeColor = Color::White;
            btnOff->FlatStyle = FlatStyle::Flat;
            btnOff->FlatAppearance->BorderSize = 0;
            btnOff->Font = gcnew Drawing::Font("Segoe UI", 10.0f, FontStyle::Bold);
            btnOff->Click += gcnew EventHandler(this, &MainForm::OnOff);

            this->Controls->Add(lblTitle);
            this->Controls->Add(lblSelected);
            this->Controls->Add(cardLight);
            this->Controls->Add(cardKettle);
            this->Controls->Add(cardTV);
            this->Controls->Add(btnOn);
            this->Controls->Add(btnOff);
        }

        void HighlightSelected() {
            cardLight->BackColor = Color::FromArgb(40, 40, 40);
            cardKettle->BackColor = Color::FromArgb(40, 40, 40);
            cardTV->BackColor = Color::FromArgb(40, 40, 40);
            selectedCard->BackColor = Color::FromArgb(55, 55, 55);
            selectedCard->Invalidate();
            lblSelected->Text = "Выбрано: " + selectedCard->deviceName;
        }

        void OnSelectLight(Object^ sender, EventArgs^ e) {
            selectedCard = cardLight;
            selectedId = "light";
            HighlightSelected();
        }

        void OnSelectKettle(Object^ sender, EventArgs^ e) {
            selectedCard = cardKettle;
            selectedId = "kettle";
            HighlightSelected();
        }

        void OnSelectTV(Object^ sender, EventArgs^ e) {
            selectedCard = cardTV;
            selectedId = "tv";
            HighlightSelected();
        }

        void OnOn(Object^ sender, EventArgs^ e) {
            using namespace System::Runtime::InteropServices;
            IntPtr ptr = Marshal::StringToHGlobalAnsi(selectedId);
            std::string sid((const char*)ptr.ToPointer());
            Marshal::FreeHGlobal(ptr);
            controller->controlDevice(sid, "on");
            selectedCard->SetState(true);
        }

        void OnOff(Object^ sender, EventArgs^ e) {
            using namespace System::Runtime::InteropServices;
            IntPtr ptr = Marshal::StringToHGlobalAnsi(selectedId);
            std::string sid((const char*)ptr.ToPointer());
            Marshal::FreeHGlobal(ptr);
            controller->controlDevice(sid, "off");
            selectedCard->SetState(false);
        }
    };
}