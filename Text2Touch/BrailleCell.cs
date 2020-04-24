using System;

public class BrailleCell
{
	/*  myTL - - myTR
	 *  myML - - myMR
	 *  myBL - - myBR
	 */
	private bool myTL, myTR;
	private bool myML, myMR;
	private bool myBL, myBR;

	public BrailleCell(char inputChar, bool capsEnabled) {
		int unicodeVal = Int(inputChar);

		// default to false
		myTL = false; myTR = false;
		myML = false; myMR = false;
		myBL = false; myBR = false;

		// decode by if each dot is present
		if (unicodeVal >= 0x30 && unicodeVal <= 0x39) { // DIGIT: CHANGE

        }

		switch(Int(inputChar)) {
			default:
				myTL = 1;
				myTR = 1;
				myML = 1;
				myMR = true;
				myBL = true;
				myBR = true;
        }
	}

	public bool requiresCaps(char inputChar) {
		if (Int(inputChar) >= 65 && Int(inputChar) <= 90) {
			return true;
        } else {
			return false;
        }
    }

	private swap(bool dot1, bool dot2) {
		if (dot1 == true) {
			dot1 = dot2;
			dot2 = true;
        } else {
			dot1 = dot2;
			dot2 = false;
        }
    }

	private flip() {
		swap(myTL, myTR);
		swap(myML, myMR);
		swap(myBL, myBR);
    }


}
