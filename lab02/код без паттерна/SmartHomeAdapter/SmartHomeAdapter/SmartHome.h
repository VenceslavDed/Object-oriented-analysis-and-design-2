#pragma once
#include <string>

// ============================================================
//  СТОРОННИЕ УСТРОЙСТВА (несовместимые интерфейсы)
// ============================================================

class PhilipsHue {
public:
    void setLightState(bool state) {}
    std::string getLightInfo() {
        return "PhilipsHue: яркость 80%, цвет тёплый белый";
    }
};

class XiaomiKettle {
public:
    void startHeating() {}
    void stopHeating() {}
    std::string getDeviceData() {
        return "XiaomiKettle: температура 95C, статус нагрев";
    }
};

class SamsungTV {
public:
    void powerOn() {}
    void powerOff() {}
    std::string queryStatus() {
        return "SamsungTV: канал 1, громкость 30";
    }
};

// ============================================================
//  КОНТРОЛЛЕР БЕЗ ПАТТЕРНА
//  Знает о каждом устройстве напрямую через if/else
// ============================================================

class SmartHomeController {
public:
    std::string currentDevice;
    bool isRunning = false;

    void controlDevice(const std::string& id, const std::string& command) {
        currentDevice = id;
        if (id == "light") {
            PhilipsHue light;
            if (command == "on")  light.setLightState(true);
            if (command == "off") light.setLightState(false);
            isRunning = (command == "on");
        }
        else if (id == "kettle") {
            XiaomiKettle kettle;
            if (command == "on")  kettle.startHeating();
            if (command == "off") kettle.stopHeating();
            isRunning = (command == "on");
        }
        else if (id == "tv") {
            SamsungTV tv;
            if (command == "on")  tv.powerOn();
            if (command == "off") tv.powerOff();
            isRunning = (command == "on");
        }
    }

    std::string getStatus(const std::string& id) {
        if (id == "light") { PhilipsHue l;   return l.getLightInfo(); }
        if (id == "kettle") { XiaomiKettle k; return k.getDeviceData(); }
        if (id == "tv") { SamsungTV tv;   return tv.queryStatus(); }
        return "Неизвестное устройство";
    }
};