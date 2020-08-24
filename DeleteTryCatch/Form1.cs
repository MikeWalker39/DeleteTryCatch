using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeleteTryCatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();
            
            StringBuilder sb = new StringBuilder();

            int lineNumber = 0;

            List<string> lines = s.Split('\n').ToList();


            bool skipOneLine = false;
            bool skipTilClose = false;

            string buffer = "";

            foreach(string line in lines)
            {

                if (skipOneLine)
                {
                    buffer = "";

                    skipOneLine = false;

                    continue;
                }

                if (skipTilClose)
                {
                    buffer = "";

                    if (line.Contains("}"))
                    {
                        skipTilClose = false;
                    }

                    continue;                    
                }

                if (line.Trim() == "try")
                {
                    sb.AppendLine(buffer);

                    buffer = "";

                    skipOneLine = true;

                    continue;
                }

                if (line.Trim() == "catch (Exception ex)")
                {
                    skipTilClose = true;

                    continue;
                }
                                
                sb.AppendLine(buffer);

                buffer = line;
            }

            sb.AppendLine(buffer);

            StringBuilder sb2 = new StringBuilder();

            bool wasBlank = false;

           

            foreach(string str in sb.ToString().Split('\n'))
            {
                bool isBlank = false;

                if (str.Trim().Length == 0)
                {
                    isBlank = true;
                }

                if (wasBlank == true && isBlank == true) continue;

                wasBlank = isBlank;

                sb2.AppendLine(str);
            }
                

            Clipboard.SetText(sb2.ToString().Replace("\r", ""));

            rtbCatch.Text = sb2.ToString();
        }
    }
}

