using System;
using BrailleCell;

public class TxtTranslator
{
	// 27 cells/line, 22 cell lines/page NOW 30 CELL LINES/PAGE
	// 54 dots/line, 66 dot lines/page NOW 90 DOT LINES/PAGE
	private const int CELLS_PER_LINE = 27;
	private const int LINES_PER_PAGE = 30;

	private int currentPageNum;
	private int currentLineNum;
	private List<BrailleCell> cellLines = new List<BrailleCell>(22);


	public TxtTranslator() {
		currentPageNum = 0;
		currentLineNum = 0;
	}	

	private incrementLine() {
		currentLineNum++;
		if (currentLineNum >= 22) {
			currentLineNum -= 22;
			currentPageNum++;
			// ROLL PAPER
        }
    }

	public readFile(string filePath) {

    }

		/*read txt file fn
		 * read a word, see if there's enough room in the current line, then add it
		 * if there's not enough room in the current line, add it to the next line
		 * WATCH CAPITALIZATION AND SPECIAL CHARS
		 * 
		 */

		/*read text box fn
		 * read the entire textbox then plan it
		 * 
		 */

		/*print fn
		 * send line by line
		 * wait for confirmation before sending the next line
		 * if end of page is reached, wait for page switch
		 * 
		 */

		/*translate top line char fn
		 * given a character, return a binary value "0bxx" with the top line of the Braille character (backwards?)
		 */

		// translate middle line char fn

		// translate bottom line char fn

		/*private untranslated page
		 * list of 22 strings
		 * each string is the line (no more than 27 chars)
		 */

	private void planPage(String inputStr) {
		List<String> words = inputStr.Split(' ');
    }

	private void translate(Char inputChar) {
		switch (inputChar) {
			// add cases
        }
    }

	public void translate(String inputStr) {
		
    }
}
