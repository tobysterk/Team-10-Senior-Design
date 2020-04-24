// reference: https://lastminuteengineers.com/a4988-stepper-motor-driver-arduino-tutorial/

// constant variables
const int xStepPin = 6;
const int xDirPin = 5; 
const int xStepsPerRevolution = 5;
const int yStepPin = 7;
const int yDirPin = 8;
const int yStepsPerRevolution = 5;
const int xMinSwitch = 9;
const int xMaxSwitch = 10;
const int yMinSwitch = 11;
const int yMaxSwitch = 12;

String cmd;
int xDir = 1; // 1 means (+) direction, 0 means (-) direction
int xCoord = 0;
int yDir = 1; // 1 means (+) direction, 0 means (-) direction
int yCoord = 0;

void setup() {
  Serial.begin(9600);
  pinMode(xStepPin, OUTPUT);
  pinMode(xDirPin, OUTPUT);
  pinMode(yStepPin, OUTPUT);
  pinMode(yDirPin, OUTPUT);
  pinMode(xMinSwitch, INPUT);
  pinMode(xMaxSwitch, INPUT);
  pinMode(yMinSwitch, INPUT);
  pinMode(yMaxSwitch, INPUT);
}

void loop() {
  checkForSerial();
  //testSwitches();
}

// method to check for and receive serial input
void checkForSerial(){
  // get input from serial 
  if(Serial.available() > 0){
    char in = Serial.read();
    if(in == '/'){
      Serial.println(" ");
      executeCmd(cmd);
      cmd = "";
    }
    else{
      Serial.print(in);
      cmd += in;
    }
  }
}

// method to move the arm along the x axis a certain amount of increments
// param: integer incr for number of increments/steps to move
void moveXMotor(int incr){
  // set motor direction
  if( xDir == 0){
    digitalWrite(xDirPin, HIGH);
  }
  else{
    digitalWrite(xDirPin, LOW);
  }
  // increment the position incr times
  for( int x = 0; x < incr; x++){
    if (digitalRead(xMaxSwitch) == HIGH){ 
      // change xDir to (-)
      xDir = 0;
      digitalWrite(xDirPin, HIGH);
    }
    else if (digitalRead(xMinSwitch) == HIGH){
      // change xDir to (+)
      xDir = 1;
      digitalWrite(xDirPin, LOW);
    }
    // spin motor slowly
    for( int y = 0; y < xStepsPerRevolution; y++){
        digitalWrite(xStepPin, HIGH);
        delayMicroseconds(2000);
        digitalWrite(xStepPin, LOW);
        delayMicroseconds(2000);
    }
    delay(300);
    xCoord++;
  }
}

// method to move the arm along the y axis a certain amount of increments
// param: integer incr for number of increments/steps to move
void moveYMotor(int incr){
  // set motor direction
  if( yDir == 1){
    digitalWrite(yDirPin, HIGH);
  }
  else{
    digitalWrite(yDirPin, LOW);
  }
  // increment the position incr times
  for( int x = 0; x < incr; x++){
    if (digitalRead(yMaxSwitch) == HIGH){ 
      // change yDir to (-)
      yDir = 0;
      digitalWrite(yDirPin, LOW);
    }
    else if (digitalRead(yMinSwitch) == HIGH){
      // change yDir to (+)
      yDir = 1;
      digitalWrite(yDirPin, HIGH);
    }
    // spin motor slowly
    for( int y = 0; y < yStepsPerRevolution; y++){
        digitalWrite(yStepPin, HIGH);
        delayMicroseconds(2000);
        digitalWrite(yStepPin, LOW);
        delayMicroseconds(2000);
    }
    delay(300);
    yCoord++;
  }
  
}

// method to move the arm to the zero position in the x and y directions
void zero(){
  // x direction
  xDir = 0; // movement is in the (-) direction, or toward x = 0
  while( digitalRead(xMinSwitch) == LOW){
    moveXMotor(1);
  }
  Serial.println("X = 0");
  xCoord = 0;
  // y direction
  yDir = 0; // movement is in the (-) direction, or toward y = 0
  while( digitalRead(yMinSwitch) == LOW){
    moveYMotor(1);
  }
  Serial.println("Y = 0");
  yCoord = 0;  
}

// method to move the arm to a specific (x,y) coordinate
// param: int newX, int newY
void moveToXY(int newX, int newY){
  // move to new x coordinate
  // if need to move in the (-) x direction
  if ((xCoord - newX) > 0){
    xDir = 0;
    while(xCoord != newX){
      moveXMotor(1);
    }
  }
  // if need to move in the (+)  x direction
  else if ((xCoord - newX) < 0){
    xDir = 1;
    while(xCoord != newX){
      moveXMotor(1);
    }
  }
  // move to new y coordinate
   // if need to move in the (-) y direction
  if ((yCoord - newY) > 0){
    yDir = 0;
    while(yCoord != newY){
      moveYMotor(1);
    }
  }
  // if need to move in the (+) y direction
  else if ((yCoord - newY) < 0){
    yDir = 1;
    while(yCoord != newY){
      moveYMotor(1);
    }
  }
}

// method to interpret and execute a command from the serial port
// param: String cmd
void executeCmd(String cmd){
  // moveX command moves the X direction motor
  if (cmd.substring(0,6) == "moveX "){
    // convert the rest of the command into an integer 
    int xIncr = cmd.substring(6).toInt();
    moveXMotor(xIncr);
  }
  // moveY command moves the Y direction motor
  else if (cmd.substring(0,6) == "moveY "){
    // convert the rest of the command into an integer 
    int yIncr = cmd.substring(6).toInt();
    moveYMotor(yIncr);
  }
  // coord command returns the coordinates of the arm
  else if (cmd == "coord"){
    Serial.print("(");
    Serial.print(xCoord);
    Serial.print(", ");
    Serial.print(yCoord);
    Serial.print(")");
    Serial.println();
  }
  // zero command moves the plotter to (0, 0)
 else if (cmd == "zero"){
    zero();
 }
 else if (cmd == "changeXDir"){
    changeXDir();
 }
 else if (cmd == "changeYDir"){
    changeYDir();
 }
 else{
  Serial.println("Invalid command");
 }
}

// method to change the direction of motion in the x direction
void changeXDir(){
  if(xDir == 0){
    xDir = 1;
  }
  else{
    xDir = 0;
  }
}

// method to change the direction of motion in the y direction
void changeYDir(){
  if(yDir == 0){
    yDir = 1;
  }
  else{
    yDir = 0;
  }
}

// method to test whether the limit switches are working
void testSwitches(){
    while( digitalRead(xMaxSwitch) == HIGH){
    Serial.println("xMax");
    delay(300);
  }
  while( digitalRead(xMinSwitch) == HIGH){
    Serial.println("xMin");
    delay(300);
  }
  while( digitalRead(yMaxSwitch) == HIGH){
    Serial.println("yMax");
    delay(300);
  }
  while( digitalRead(yMinSwitch) == HIGH){
    Serial.println("yMin");
    delay(300);
  }
}
