/*Variables for sending data, receiving data, storing unsigned values and measurement types*/
unsigned char UART_To_PC[7];     // declares an array of 7 characters
unsigned char Recieved_Data[15]; // Store received text
unsigned int Upper_REG=0;        //Holds ADC or Timer1's register upper value (16 bit integer)
unsigned int Lower_REG=0;        //Holds ADC or Timer1's register lower value (16 bit integer)
unsigned int Final_REG=0;        //Holds ADC or Timer1's Final Result, after bit shifting and 'OR'ing
int Cap_Res;                     //Indicate if Resistance or Capacitance is being measured
int cnt;                         // counter variable
unsigned char input;             // input is a variable to store a character received from the serial port
unsigned int Multiplier =0 ;
/****************************************/

/*Variables related to Resistance*/
unsigned char *Stages[4] ={"1stS","2ndS","3rdS","4thS"};  //declares an array of 4 char to store Stages numbers
unsigned int *Stages_bits[4] ={0,1,2,3};  //declares an array of 4 integers, where each value enables specific CS Stage
sbit INH at PORTB.B4; // Enabling bit for the Analog Switch
int stage_counter=0;   // Stage counter
/***********************************/

/* Variables related to Capacitance*/
sbit V_Charge at RA0_bit; // charge/dis-charge and set reference
unsigned int Num =0 ;  // Initialize number of overflows
unsigned int OverRange = 0; //over-range indicator
/*********************************/

/*Declaring Functions*/
void Initialize_ADC();
void Data_To_Send(); // Feedback the measurement data (either Resistance or Capacitance)
void Initialize_Timer();


void main() {

UART1_Init(9600); // Initialize UART module with a specific baud-rate
delay_ms(100);
INH = 1;          // Disable Analog Switch
CREN_bit = 1;        // enable the receive interrupt
RCIE_bit = 1;        // enable serial port receive interrupt
PEIE_bit = 1;        // enable peripheral interrupt enable bit
GIE_bit = 1;         // enable global interrupt enable bit (needed for any interrupt to be active)
while(1){
delay_ms(500);
while (!RCIF_bit){}

UART1_Read_Text(Recieved_Data, "\r", 15);// store received data

             if ( strcmp ( Recieved_Data, "Resistance" ) == 0 ) {

                Initialize_ADC();//Initialize and setup the necessary software configs.
                Cap_Res=1;// set sending Resistance option

                Anotherstage:
                delay_ms(5); // Delay before starting ADC conversion
                ADCON0.GO = 1;   // start conversion
                while(ADCON0.GO==1){}//wait until ADCON0<1> flips to 0

               //Combining ADC's Upper and Lower Register Bytes value
                Lower_REG=ADRESL;
                Upper_REG=ADRESH;
                Upper_REG=Upper_REG << 8;
                Final_REG=Upper_REG^Lower_REG;


                switch (stage_counter) {
                        case 0:
                                // if ADC value less than 1023 for current stage, then display result & end program
                               if(Final_REG<1023){Data_To_Send(); break;}

                               //else, enable and check with the next CS stage, and repeat conversion process
                               else { stage_counter++; PORTB=Stages_bits[stage_counter]; goto Anotherstage;}

                        case 1:

                              // if ADC value less than 1023 for current stage, then display result & end program
                              if(Final_REG<1023){Data_To_Send(); break;}

                              //else, enable and check with the next CS stage, and repeat conversion process
                              else { stage_counter++; PORTB=Stages_bits[stage_counter]; goto Anotherstage;}

                        case 2:

                             // if ADC value less than 1023 for current stage, then display result & end program
                             if(Final_REG<1023){Data_To_Send(); break;}

                             //else, enable and check with the next CS stage, and repeat conversion process
                             else { stage_counter++; PORTB=Stages_bits[stage_counter]; goto Anotherstage;}
                        case 3:

                              // if ADC value less than 1023 for current stage, then display result & end program
                             if(Final_REG<1023){Data_To_Send(); break;}

                             // Indicate that the measured value is out of range
                             else { UART1_Write_Text("Out of Range!");
                             UART1_Write(13); /*CR*/ 
                             INH = 1;    // Disable Analog Switch
                             ADCON0 = 0; // Turn off the ADC module
                             break;}

                        }
                        }
             else if ( strcmp ( Recieved_Data, "Capacitance" ) == 0 ) {
                    Initialize_Timer();//Initialize and setup the necessary software configs.
                    Cap_Res=0;// set sending Capacitance option
                    V_Charge = 1; //Charge capacitor through 30 kohm, and ON voltage reference
                    
                    T1CON= 0x01; // start timer

                       while(C2OUT_bit==0) {
                                  if (TMR1IF_bit){
                                   Num++;
                                   TMR1IF_bit =0; // Clear TMR2 interrupt flag
                                   }
                                   if(Num > 55){ OverRange = 1; break;} // Range, based on +10% tolerance
                                  }
                                  TMR1ON_bit = 0; // stop timer
                                  V_Charge = 0;//Off charging and discharge the capacitor
                       // if not OverRange, then print out the number of the over flows and current TMR2 value
                       if(!OverRange){
                                   //Combining ADC's Upper and Lower Register value
                                    Lower_REG=TMR1L;
                                    Upper_REG=TMR1H;
                                    Upper_REG=Upper_REG << 8;
                                    Final_REG=Upper_REG^Lower_REG;
                                    Data_To_Send();
                                 }
                       // if out of measureable range, print out so
                       if(OverRange){
                                  UART1_Write_Text("Out of Range!");
                                  UART1_Write(13); // CR
                                  C2ON_bit = 0; // OFF comparator 2
                                  }
                        }
                       else
                       {
                             UART1_Write_Text("Error!");
                             UART1_Write(13); // CR
                                  }
 UART1_Read();// discard any other data
  RCIF_bit =0;
  }
}
void Initialize_Timer(){

      TRISA = 0x06; // RA1(-) and RA2(+) as inputs, the rest outputs
      ANSEL = 0x06; // RA1(-) and RA2(+) as Analog inputs, the rest digital outputs
      V_Charge = 0; // RA0 off initially
      INTCON= 0xC0; //Enable global interrupt,Enable peripheral interrupt

      /* Configure Timer1 module*/
      TMR1IE_bit = 1;  // Enable Timer1 interrupt
      T1CON = 0;        // Prescaler 1:1, and Timer1 is off initially
      /*Initialize TMR1 register*/
      TMR1H = 0;
      TMR1L = 0;

      /* Configure Comparator module */
      C1ON_bit = 0; // OFF comparator 1
      C2ON_bit = 1; // ON comparator 2
      C2OE_bit = 0; //internal output
      C2POL_bit = 0; // non-inverted polarity

      C2R_bit=0; // C2VIN+ connects to C2IN+ pin (non-inverting terminal)
      Num=0; // Reset number of overflows
      OverRange = 0; // Reset OverRange indicator
      // C12IN1- pin of C2 connects to C2VIN- (Inverting Terminal)
      C2CH1_bit=0;
      C2CH0_bit=1;


}

void Initialize_ADC(){

     TRISB=0x00; // Set port B as outputs
     TRISA=0x28; // set RA3 and RA5 as inputs
     INH = 0;    // Enable Analog Switch
     PORTB=Stages_bits[0]; // Enable first CS stage
     stage_counter = 0;
     ANSEL=0x28; // set RA3 and RA5 as Analog inputs


     ADCON1.ADFM = 1;  // Set sampled Voltage value bits to right-justified
     ADCON1.VCFG0 = 1; // Set external Vref+ reference, RA2
     ADCON0=0xD1;      //setting conversion clock (FRC- 500kHz), select Analog channel(AN4),and Enable ADC module
     Delay_ms(5);       // Necessary delay for acquisition time "Datasheet"

}
void Data_To_Send(){

      if (Cap_Res == 1)
      {
        UART1_Write_Text(Stages[stage_counter]); // Send stage number
        IntToStr(Final_REG,UART_To_PC); // Convert Integer value to String
                Delay_ms(100);
        UART1_Write_Text(UART_To_PC); // Send and print sampled value
                Delay_ms(100);
        UART1_Write(13); // CR
        }
        else
        {
        UART1_Write_Text("F"); // Send text "F",indicate overFlow
        IntToStr(Num,UART_To_PC); // Convert Integer value to String
                Delay_ms(100);
        UART1_Write_Text(UART_To_PC); // Send and print sampled value
        strcpy(UART_To_PC,""); //Clear variable to load it with other data
                Delay_ms(100);
        UART1_Write_Text("R"); // Send text "R",indicate Register value
        WordToStr(Final_REG,UART_To_PC); // Convert unsigned Integer value to String
                Delay_ms(100);
        UART1_Write_Text(UART_To_PC); // Send and print sampled value
                Delay_ms(100);
        UART1_Write(13); // CR
        C2ON_bit = 0; // OFF comparator 2
        }
}