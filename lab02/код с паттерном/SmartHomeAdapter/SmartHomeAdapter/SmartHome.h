#pragma once
#include <string>

// ============================================================
//  ÑÒÎÐÎÍÍÈÅ ÓÑÒÐÎÉÑÒÂÀ (íåñîâìåñòèìûå èíòåðôåéñû)
// ============================================================

class PhilipsHue {
public:
    void setLightState(bool state) {}
    std::string getLightInfo() {
        return "PhilipsHue: ÿðêîñòü 80%, öâåò ò¸ïëûé áåëûé";
    }
};

class XiaomiKettle {
public:
    void startHeating() {}
    void stopHeating() {}
    std::string getDeviceData() {
        return "XiaomiKettle: òåìïåðàòóðà 95C, ñòàòóñ íàãðåâ";
    }
};

class SamsungTV {
public:
    void powerOn() {}
    void powerOff() {}
    std::string queryStatus() {
        return "SamsungTV: êàíàë 1, ãðîìêîñòü 30";
    }
};

// ============================================================
//  ÈÍÒÅÐÔÅÉÑ
// ============================================================

class ISmartDevice {
public:
    virtual void turnOn() = 0;
    virtual void turnOff() = 0;
    virtual std::string getStatus() = 0;
    virtual std::string getName() = 0;
    virtual ~ISmartDevice() = default;
};

// ============================================================
//  ÀÄÀÏÒÅÐÛ
// ============================================================

class PhilipsHueAdapter : public ISmartDevice {
    PhilipsHue device;
public:
    void turnOn()  override { device.setLightState(true); }
    void turnOff() override { device.setLightState(false); }
    std::string getStatus() override { return device.getLightInfo(); }
    std::string getName()   override { return "Ëàìïà Philips Hue"; }
};

class XiaomiKettleAdapter : public ISmartDevice {
    XiaomiKettle device;
public:
    void turnOn()  override { device.startHeating(); }
    void turnOff() override { device.stopHeating(); }
    std::string getStatus() override { return device.getDeviceData(); }
    std::string getName()   override { return "×àéíèê Xiaomi"; }
};

class SamsungTVAdapter : public ISmartDevice {
    SamsungTV device;
public:
    void turnOn()  override { device.powerOn(); }
    void turnOff() override { device.powerOff(); }
    std::string getStatus() override { return device.queryStatus(); }
    std::string getName()   override { return "Òåëåâèçîð Samsung"; }
};

// ============================================================
//  ÊÎÍÒÐÎËËÅÐ
// ============================================================

class SmartHomeController {
public:
    std::string currentDevice;
    bool isRunning = false;

    void controlDevice(ISmartDevice* device, const std::string& command) {
        currentDevice = device->getName();
        if (command == "on") { device->turnOn();  isRunning = true; }
        else if (command == "off") { device->turnOff(); isRunning = false; }
    }

    std::string getStatus(ISmartDevice* device) {
        return device->getStatus();
    }
};
