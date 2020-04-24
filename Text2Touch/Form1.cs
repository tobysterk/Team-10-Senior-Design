using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        private void chooseFileButton_Click(object sender, EventArgs e) {
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netframework-4.8
            string filePath = "unsuccessful";
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = @"c:\Users\tobys\Desktop";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    sendTextButton.Enabled = true;

                    ////Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream)) {
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
            fileNameLabel.Text = filePath;
        }

        private void sendTextButton_Click(object sender, EventArgs e) {
            if (fileRadioButton.Checked) {
                // read file (path stored in fileNameLabel.Text)
                throw new System.NotImplementedException("File translation not working yet");
            } else if (manualRadioButton.Checked) {
                planFile(textEntryBox.Text);
                // send each int


                textEntryBox.Text = "";
            } else {
                // throw exception
                throw new System.ApplicationException("Both radio buttons can't be checked");
            }
        }

        private const int CELLS_PER_LINE = 27;
        private const int LINES_PER_PAGE = 30;

        private int currentCellLine = 0;
        private List<String> untranslatedPage = new List<String>(LINES_PER_PAGE);

        private int currentDotLine = 0;
        private List<int> translatedPage = new List<int>(LINES_PER_PAGE * 3);

        private void planFile(String fileContents) {
            setupUntranslatedPage(fileContents); // untranslated giant line list with reversed words
            
            // from each char of each line, get the top, middle, and bottom and put it into a list of ints
            translatePage();

            // translatedPage is set up with the characters in reverse order

        }

        private void setupUntranslatedPage(string fileContents) {
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
                    translatedBotLine += getMidLine(character);
                }

                // write to translatedPage
                translatedPage[3*line]     = int.Parse(translatedTopLine); // 0, 3, 6, 9, etc.
                translatedPage[3*line + 1] = int.Parse(translatedMidLine); // 1, 4, 7, 10, etc.
                translatedPage[3*line + 2] = int.Parse(translatedMidLine); // 2, 5, 8, 11, etc.
            }
        }

        private String getTopLine(char character) {
            switch(character) {
                case 'a':
                    ;
                    break;
                default:
                    ;
                    break;
            }
            return "11";
        }

        private String getMidLine(char character) {

            return "11";
        }

        private String getBotLine(char character) {

            return "11";
        }

        private String returnText = "Text sent: Test text";

        //private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e) {
        //    returnText = serialPort1.ReadLine();
        //}

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
