#include "paulvha_SCD30.h"
#define scd_debug 0

/* Put your SSID & Password */
const char* ssid = "raspi";  // Enter SSID here
const char* password = "20raspi19!";  //Enter Password here

int measurePin = 34;
int ledPower = 13;
//int reedPin = 27;

int sensorPinLDR = A0; // select the input pin for LDR

int sensorValueLDR = 0; // variable to store the value coming from the sensor


unsigned int samplingTime = 280;
unsigned int deltaTime = 40;
unsigned int sleepTime = 9680;
//unsigned int countDoor = 0;
//unsigned int reedTemp = 1;

double voMeasured = 0;
double calcVoltage = 0;
double dustDensity = 0;
//double milliseconds = millis();

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

  //reed();
  

  if(Serial.available()>0){
    if(Serial.read()!=-1){
      Serial.print("dust:" + String(dustDensity));
      sensorValueLDR=analogRead(sensorPinLDR);
      Serial.print(";ldr:"+String(sensorValueLDR));
      Serial.print(";humidity:"+String(humidity));
      Serial.print(";co2:"+String(co2));
      Serial.print(";temp:"+String(temp));
      Serial.print(";noise:0.00");
      //Serial.print(";door:"+String(countDoor));
      Serial.println();
      //resetDoor();
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

  calcVoltage = voMeasured*(5000/1024);
  dustDensity = 0.17*calcVoltage-0.1;

  if ( dustDensity < 0)
  {
    dustDensity = 0.00;
  } 
}

void resetDoor() {
  countDoor = 0;
}

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
