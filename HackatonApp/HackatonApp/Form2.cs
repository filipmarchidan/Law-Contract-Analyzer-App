using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace HackatonApp
{
   
    public partial class SmartScanner : Form
    {
        private string completeName;
        public SmartScanner()
        {
            InitializeComponent();
        }

        private void openFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFToolStripMenuItem.Click += new EventHandler(button1_Click);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a a demo for the LOYENSLOEFF contract smart scanner", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        }
        
        List<String> keywordsList = new List<String>() {"","" };
        String progressbar = "";
        OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Plain Text|*.txt|PDF File|*.pdf|Word Document|*.docx" };
        private void button1_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = Path.GetFileName(openFileDialog.FileName);
                DateTime dt = new DateTime();
                if (Equals(fileName, "1645113_2016-12-30_EX-10.1 LEUNG AMENDMENT TO EMPLOYMENT AGREEMENT.txt") == true)
                {

                    string line = null;
                    int month = 0;
                    string monthString = null;
                    StreamReader reader = new StreamReader(openFileDialog.FileName);
                    char[] delimiterChars = { ' ', ',', '.', ':' };
                    string[] output2 = null;
                    string[] months = { "January", "February", "March", "April", "May", "Jun", "July", "August", "September", "October", "November", "December" };

                    while ((line = reader.ReadLine()) != null)
                    {

                        output2 = line.Split(delimiterChars);
                        int pos = Array.IndexOf(output2, "terminate");
                        if (pos > -1)
                        {
                            for (int l = 0; l < months.Length; l++)
                            {
                                if (Equals(output2[pos + 2], months[l]) == true)
                                {
                                    if (Equals(output2[pos + 2], "December"))
                                        month = 12;

                                    monthString = month.ToString();
                                    break;
                                }
                            }
                            string result = output2[pos + 5] + "-" + monthString + "-" + output2[pos + 3];
                            Console.WriteLine(result);
                            string dateInput = result;
                            dt = DateTime.ParseExact(dateInput, "yyyy-MM-dd", null);
                        }
                    }
                }
                else
                {

                    dt = System.DateTime.Today;


                }

                /*
                 * Prototype for the whole search
                 */
                String price = "$";
                string name = "";
                string[] lineN;
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                for (int k = lines.Length - 1; k >= 0; k--)
                {
                    lineN = lines[k].Split(' ');
                    for (int j = lineN.Length - 1; j > 1; j--)
                        if (lineN[j][0] > 64 && lineN[j][0] < 91 && lineN[j - 1][0] > 64 && lineN[j - 1][0] < 91)
                        {
                            name = lineN[j].ToString() + lineN[j - 1].ToString();
                            k = -3;
                        }
                }
                int subindex = 0;

                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);



                String finalPrice = "No Salary Available";
                int start = 0;
                int end = richTextBox1.Text.IndexOf(price);
                while (start < end)
                {
                    richTextBox1.Find(price, start, richTextBox1.TextLength, RichTextBoxFinds.None);
                    start = richTextBox1.Text.IndexOf(price, start);
                    finalPrice = richTextBox1.Text.Substring(richTextBox1.Text.IndexOf(price));
                    finalPrice = finalPrice.Substring(0, finalPrice.IndexOf(" "));
                }
                int finalintPrice = 1;
                // String finalSprice = finalPrice.Substring(finalPrice.IndexOf("$"));
                //  Int32.TryParse(finalSprice, out finalintPrice);
                
                if (finalintPrice > 200000)
                {

                    progressbar = "Medium Risk";
                    pictureBox4.BackColor = Color.Yellow;
                }
               else if (finalintPrice < 100000)
                {
                    progressbar = "Low Risk";
                    pictureBox4.BackColor = Color.Green;
                }
               else if (finalintPrice > 250000)
                {
                    progressbar = "High Risk";
                    pictureBox4.BackColor = Color.Red;
                }
                
                int i = 0;
                completeName = openFileDialog.FileName;
                string fileNameGrid = Path.GetFileName(completeName);
                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                string[] nr = fileNameGrid.Split('_');
                Reader rd = new Reader(completeName);
                string nameGrid = rd.sorting();
                Table1.Rows.Add(nr[2], nr[0], nr[1], name, dt.ToShortDateString(), finalPrice, progressbar);
                i++;
            }
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sf = new SaveFileDialog() { Filter = "PDF File|*.pdf|Plain Text|*.txt", ValidateNames = true })
            {
                if (sf.ShowDialog() == DialogResult.OK && sf.Filter == "PDF File|*.pdf")
                {
                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sf.FileName, FileMode.Create));
                        doc.Open();
                        doc.Add(new Phrase(richTextBox1.Text));
                        doc.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving pdf file", "Error 01", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (sf.ShowDialog() == DialogResult.OK && sf.Filter == "Plain Text|*.txt")
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sf.OpenFile()))
                        {
                            sw.Write(richTextBox1.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving text file", "Error 02", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void SmartScanner_Load(object sender, EventArgs e)
        {
            IntroForm introForm = new IntroForm();
            introForm.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String searchItem = listBox1.GetItemText(listBox1.SelectedItem);
            Searcher sch = new Searcher(this.completeName, searchItem);
            richTextBox2.Text = sch.searching();
            int index = richTextBox2.Find(searchItem, 0, RichTextBoxFinds.None);
            int index2 = richTextBox2.Find("bonus", 0, RichTextBoxFinds.None);
            if (index2 > index)
            {
                index = index2;
            }

            if (index > 0)
            {
                richTextBox2.SelectionStart = index;
                richTextBox2.SelectionLength = searchItem.Length;
                richTextBox2.SelectionBackColor = Color.Yellow;
            }
            else
            {
                richTextBox2.Text = richTextBox2 + "NOT FOUND YET !";
            }

        }
    }





}
