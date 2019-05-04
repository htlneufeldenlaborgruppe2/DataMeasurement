#include "paulvha_SCD30.h"
#define scd_debug 0

/* Put your SSID & Password */
const char* ssid = "raspi";  // Enter SSID here
const char* password = "20raspi19!";  //Enter Password here

int measurePin = 34;
int ledPower = 13;

int sensorPinLDR = A0; // select the input pin for LDR

int sensorValueLDR = 0; // variable to store the value coming from the sensor


unsigned int samplingTime = 280;
unsigned int deltaTime = 40;
unsigned int sleepTime = 9680;

float voMeasured = 0;
float calcVoltage = 0;
float dustDensity = 0;

float co2=0;
float temp=0;
float humidity=0;

double interruptCount=0;

/* Put IP Address details */

SCD30 airSensor;



void setup() {
  Serial.begin(9600);
setup_dust();
setup_co2_hum_temp();
Serial2.begin(9600, SERIAL_8N1, 16, 17);


  
}
void loop() {
  loop_co2_hum_temp();
  measure_dust();

  if(Serial.available()>0){
    if(Serial.read()!=-1){
      Serial.print("dust:" + String(dustDensity));
      sensorValueLDR=analogRead(sensorPinLDR);
      Serial.print(";ldr:"+String(sensorValueLDR));
      Serial.print(";humidity:"+String(humidity));
      Serial.print(";co2:"+String(co2));
      Serial.print(";temp:"+String(temp));
      Serial.println();
      
    }
  }
}


void measure_dust(){
  digitalWrite(ledPower,LOW);
  delayMicroseconds(samplingTime);

  voMeasured = analogRead(measurePin);

  delayMicroseconds(deltaTime);
  digitalWrite(ledPower,HIGH);
  delayMicroseconds(sleepTime);

  calcVoltage = voMeasured*(3.3/1024);
  dustDensity = 0.17*calcVoltage-0.1;

  if ( dustDensity < 0)
  {
    dustDensity = 0.00;
  }  
}

void setup_dust(){
    pinMode(ledPower, OUTPUT);
  digitalWrite(ledPower,HIGH);
}

void setup_co2_hum_temp(){
  Wire.begin();
  airSensor.setDebug(scd_debug);

  airSensor.begin(Wire); //This will cause readings to occur every two seconds
}
void loop_co2_hum_temp(){
  if (airSensor.dataAvailable())
  {
    //Serial.print("co2(ppm):");
    co2=airSensor.getCO2();
    //Serial.print(co2);

    //Serial.print(" temp(C):");
    temp=airSensor.getTemperature();
    //Serial.println(temp);

    //Serial.print(" humidity(%):");
    humidity=airSensor.getHumidity();
    //Serial.println(humidity);

    //Serial.println();
  }
}
