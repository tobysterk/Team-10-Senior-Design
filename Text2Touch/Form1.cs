using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text2Touch {
    public partial class Text2TouchWindow : Form {

        // Lots of help from https://stackoverflow.com/questions/13794376/combo-box-for-serial-port
        public Text2TouchWindow() {
            InitializeComponent();
            this.Load += Text2TouchWindow_Load;
        }



        ~Text2TouchWindow() {
            serialPort1.Close();
            timer1.Stop();
        }

        void Text2TouchWindow_Load(object sender, EventArgs e) {
            PortSelectionComboBox.DataSource = SerialPort.GetPortNames();
        }

        private void PortSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            // close port if open
            if (serialPort1.IsOpen)
                closeSerial();
            // change port selection
            serialPort1.PortName = PortSelectionComboBox.SelectedItem.ToString();
        }

        private void PortConnectionButton_Click(object sender, EventArgs e) {
            if (!serialPort1.IsOpen) {
                openSerial();
            }
            else {
                closeSerial();
            }
        }

        private void openSerial() {
            try {
                // try opening the port
                serialPort1.Open();
                timer1.Start();
                connectionStatusLabel.Text = ("Status: Connection with "
                    + PortSelectionComboBox.SelectedItem.ToString() + " established");
                PortConnectionButton.Text = "Disconnect";
                PortSelectionComboBox.Enabled = false;
                fileRadioButton.Enabled = true;
                manualRadioButton.Enabled = true;
            }
            catch (System.IO.IOException) {
                connectionStatusLabel.Text = ("Status: Failure to connect to "
                    + PortSelectionComboBox.SelectedItem.ToString());
            }
        }

        private void closeSerial() {
            serialPort1.Close();
            timer1.Stop();
            connectionStatusLabel.Text = "Status: Disconnected";
            PortConnectionButton.Text = "Connect";
            PortSelectionComboBox.Enabled = true;
            chooseFileButton.Enabled = false;
            textEntryBox.Enabled = false;
            sendTextButton.Enabled = false;
            fileRadioButton.Checked = false;
            fileRadioButton.Enabled = false;
            manualRadioButton.Checked = false;
            manualRadioButton.Enabled = false;
        }

        private OpenFileDialog ofd;

        private void chooseFileButton_Click(object sender, EventArgs e) {
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netframework-4.8
            string filePath = "unsuccessful";
            using (ofd = new OpenFileDialog()) {
                ofd.InitialDirectory = @"c:\Users\tobys\Desktop";
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK) {
                    //Get the path of specified file
                    filePath = ofd.FileName;
                    sendTextButton.Enabled = true;


                }
            }
            fileNameLabel.Text = filePath;
        }

        private void sendTextButton_Click(object sender, EventArgs e) {
            if (fileRadioButton.Checked) {
                
                // Read the contents of the file into a stream
                var fileStream = ofd.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream)) {
                    planFile(reader.ReadToEnd());
                }

            }
            else if (manualRadioButton.Checked) {
                planFile(textEntryBox.Text);
                textEntryBox.Text = "";
            } else {
                // throw exception
                throw new System.ApplicationException("Both radio buttons can't be checked");
            }

            translatePage();

            // send
        }

        private const int CELLS_PER_LINE = 27;
        private const int LINES_PER_PAGE = 30;

        private int currentCellLine = 0;
        private List<String> untranslatedPage = new List<String>(LINES_PER_PAGE);

        private int currentDotLine = 0;
        private List<int> translatedPage = new List<int>(LINES_PER_PAGE * 3);

        private void planFile(String fileContents) {
            List<String> fileContentsSplit = fileContents.Split(' ').ToList();
            foreach (String word in fileContentsSplit) {
                if ((untranslatedPage[currentCellLine].Length + word.Length) >= CELLS_PER_LINE) {
                    currentCellLine++;
                }
                word.Reverse();
                untranslatedPage[currentCellLine] = word + untranslatedPage[currentCellLine];
            }
        }

        private void translatePage() {
            for (int line = 0; line < untranslatedPage.Count; line++) {

                String translatedTopLine = "";
                String translatedMidLine = "";
                String translatedBotLine = "";

                foreach (char character in untranslatedPage[line]) {
                    // translate each char


                    // append each char
                    translatedTopLine += getTopLine(character);
                    translatedMidLine += getMidLine(character);
                    translatedBotLine += getBotLine(character);
                }

                // write to translatedPage
                translatedPage[3*line]     = int.Parse(translatedTopLine); // 0, 3, 6, 9, etc.
                translatedPage[3*line + 1] = int.Parse(translatedMidLine); // 1, 4, 7, 10, etc.
                translatedPage[3*line + 2] = int.Parse(translatedMidLine); // 2, 5, 8, 11, etc.
            }
        }

        private String getTopLine(char character) {
            // translation via http://www.acb.org/tennessee/braille.html

            // Lists of characters whose top lines have dots in certain positions
            char[] onlyLeft = {'a', 'b', 'e', 'k', 'l', 'o', 'u', 'z', '1', '2', '5', '8'};
            char[] onlyRight = {'i', 'j', 's', 't', 'w', '9', '0'}; // also num and lit
            char[] none = {',', ';', ':', '.', '!', '(', ')', '?', '\"', '*', '\"', '\'', '-'}; // also let and cap

            // Reverse the character by switching the 1 and 0
            if (onlyLeft.Contains(character))
                return "01";
            else if (onlyRight.Contains(character))
                return "10";
            else if (none.Contains(character))
                return "00";
            else // default because the error char will be completely filled
                return "11";
        }

        private String getMidLine(char character) {
            // translation via http://www.acb.org/tennessee/braille.html

            char[] onlyLeft = {'b', 'f', 'i', 'l', 'p', 's', 'v', '2', '4', '6', '9', ',', ';', '?', '\"'};
            char[] onlyRight = {'d', 'e', 'n', 'o', 'y', 'z', '4', '5', '*', '\"'}; // also let, num, lit
            char[] none = {'a', 'c', 'k', 'm', 'u', 'x', '1', '3', '\'', '-'}; // also cap, numIndex

            // Reverse the character by switching the 1 and 0
            if (onlyLeft.Contains(character))
                return "01";
            else if (onlyRight.Contains(character))
                return "10";
            else if (none.Contains(character))
                return "00";
            else // default because the error char will be completely filled
                return "11";
        }

        private String getBotLine(char character) {
            // translation via http://www.acb.org/tennessee/braille.html

            char[] onlyLeft = {'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', ';', '*', '\''};
            char[] onlyRight = {'w', '.'}; // also let and cap
            char[] none = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ',', ':'}; // also num idx and lit

            // Reverse the character by switching the 1 and 0
            if (onlyLeft.Contains(character))
                return "01";
            else if (onlyRight.Contains(character))
                return "10";
            else if (none.Contains(character))
                return "00";
            else // default because the error char will be completely filled
                return "11";
        }

        private String returnText = "Text sent: Test text";

        private void timer1_Tick(object sender, EventArgs e) {
            returnTextLabel.Text = returnText;
        }

        private void KeyPressed(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                textEntryBox.Text = textEntryBox.Text.Substring(0, textEntryBox.Text.Length - 2);
                sendTextButton.PerformClick();
                //e.Handled = true;
            }
        }

        private void fileRadioButton_CheckedChanged(object sender, EventArgs e) {
            textEntryBox.Enabled = false;
            chooseFileButton.Enabled = true;
            if (fileNameLabel.Text != "No file chosen")
                sendTextButton.Enabled = true;
        }

        private void manualRadioButton_CheckedChanged(object sender, EventArgs e) {
            chooseFileButton.Enabled = false;
            fileNameLabel.Enabled = false;
            textEntryBox.Enabled = true;
            if (textEntryBox.Text != "")
                sendTextButton.Enabled = true;
        }

        private void textEntryBox_TextChanged(object sender, EventArgs e) {
            if (textEntryBox.Text != "")
                sendTextButton.Enabled = true;
            else
                sendTextButton.Enabled = false;
        }
    }
}
