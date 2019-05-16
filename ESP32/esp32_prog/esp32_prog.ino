#include "paulvha_SCD30.h"
#define scd_debug 0

/* Put your SSID & Password */
const char* ssid = "raspi";  // Enter SSID here
const char* password = "20raspi19!";  //Enter Password here

int measurePin = 34;
int ledPower = 13;
int noisePin = //enter input pin
//int reedPin = 27;

int sensorPinLDR = A0; // select the input pin for LDR

int sensorValueLDR = 0; // variable to store the value coming from the sensor


unsigned int samplingTime = 280;
unsigned int deltaTime = 40;
unsigned int sleepTime = 9680;
unsigned int counterMulti = 0;
unsigned int counterDust = 0;
unsigned int counterLDR = 0;
unsigned int counterNoise = 0;

//unsigned int countDoor = 0;
//unsigned int reedTemp = 1;

double voMeasured = 0;
double calcVoltage = 0;
double dustDensity = 0;
double dustTemp = 0;
double noiseValue = 0;
double noiseValueTemp = 0;
double maxNoise = 0;
double minNoise = 1000000;
double milliseconds = millis();

float co2=0;
float temp=0;
float humidity=0;

string noiseAllValues = "";

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
  if(millis() - milliseconds > 500) {
  loop_co2_hum_temp();
  measure_dust();
  measure_ldr();
  measure_noise();
  milliseconds = millis();
  }
  //reed();
  

  if(Serial.available()>0){
    if(Serial.read()!=-1){
      Serial.print("dust:" + String(dustTemp/counterDust));
      Serial.print(";ldr:"+String(sensorValueLDR/counterLDR));
      Serial.print(";humidity:"+String(humidity/counterMulti));
      Serial.print(";co2:"+String(co2/counterMulti));
      Serial.print(";temp:"+String(temp/counterMulti));
      Serial.print(";noise:"+String(noiseValue/counterNoise);
      Serial.print(";noisemin:"+String(minNoise);
      Serial.print(";noisemax:"+String(maxNoise);
      Serial.print(";noisevalues:"+noiseAllValues);
      Serial.println();
      
      resetAll();
    }
  }
}

void resetAll() {
  dustTemp = 0;
  counterDust = 0;
  sensorValueLDR = 0;
  counterLDR = 0;
  humidity = 0;
  co2 = 0;
  temp = 0;
  counterMulti = 0;
  noiseValue = 0;
  counterNoise = 0;
}

void measure_noise() {
  counterNoise = counterNoise +1;
  noiseTemp = analogRead(noisePin);
  if(noiseTemp > maxNoise){
      maxNoise = noiseTemp;
    }
  else if(noiseTemp < minNoise) {
      minNoise = noiseTemp;
    }
  noiseValue = noiseValue + noiseTemp;
  noiseAllValues = noiseAllValues + String(noiseTemp) + "_";
}

void measure_ldr() {
  counterLDR = counterLDR + 1;
  sensorValueLDR= sensorValueLDR + analogRead(sensorPinLDR);
}

void measure_dust(){
  counterDust = counterDust + 1;
    digitalWrite(ledPower,LOW);
  delayMicroseconds(samplingTime);

  voMeasured = analogRead(measurePin);

  delayMicroseconds(deltaTime);
  digitalWrite(ledPower,HIGH);
  delayMicroseconds(sleepTime);

  calcVoltage = voMeasured*(5000/1024);
  dustDensity = 0.17*calcVoltage-0.1;

  if ( dustDensity < 0)
  {
    dustDensity = 0.00;
  } 
  dustTemp = dustTemp + dustDensity;
}

/*void resetDoor() {
  countDoor = 0;
}*/

void setup_dust(){
    pinMode(ledPower, OUTPUT);
  digitalWrite(ledPower,HIGH);
}

/*void setup_reed() {
    pinMode (reedPin, INPUT);
}*/

void setup_co2_hum_temp(){
  Wire.begin();
  airSensor.setDebug(scd_debug);

  airSensor.begin(Wire); //This will cause readings to occur every two seconds
}

/*void reed() {
  int reed_input;

  reed_input = digitalRead (reedPin);

  if(reed_input == 0 && reedTemp == 1) {
      countDoor = countDoor + 1;
  }
  reedTemp = reed_input;
}*/

void loop_co2_hum_temp(){
  if (airSensor.dataAvailable())
  {
    counterMulti = counterMulti +1;
    //Serial.print("co2(ppm):");
    co2=co2 + airSensor.getCO2();
    //Serial.print(co2);

    //Serial.print(" temp(C):");
    temp=temp + airSensor.getTemperature();
    //Serial.println(temp);

    //Serial.print(" humidity(%):");
    humidity=humidity + airSensor.getHumidity();
    //Serial.println(humidity);

    //Serial.println();
  }
}
