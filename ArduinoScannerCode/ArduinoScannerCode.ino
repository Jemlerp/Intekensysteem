#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         9
#define SS_PIN          10

MFRC522 mfrc522(SS_PIN, RST_PIN);

MFRC522::MIFARE_Key key;

String getsu = "";
unsigned long times;
unsigned long timesOfScan;

void setup() {  
  Serial.begin(9600);
  while(!Serial);
  SPI.begin();
  mfrc522.PCD_Init();
  for (byte i = 0; i < 6; i++) {
     key.keyByte[i] = 0xFF; //change deze
  }
}

void dump_byte_array(byte *buffer, byte bufferSize) {
    String newGetsu = "";
    times = millis();

    for (byte i = 0; i < bufferSize; i++) {
        newGetsu += (buffer[i] < 0x10 ? " 0" : " ");
        newGetsu += (buffer[i]); }
            
    if(newGetsu != getsu){
          Serial.println(newGetsu);
          getsu = newGetsu; }      
}

void loop() {  
   if(times+1000 < millis()){
    getsu = "";
   }  
   if(!mfrc522.PICC_IsNewCardPresent()){ return;} // is er überhaupt een kaart
   if(!mfrc522.PICC_ReadCardSerial()){ return;}
   dump_byte_array(mfrc522.uid.uidByte, mfrc522.uid.size);   
}
