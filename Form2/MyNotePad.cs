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
using System.Diagnostics;

namespace Form2
{
    public partial class MyNotePad : Form
    {
        //TabControl tabControl1 = new TabControl();
        string namef = "noName";
        public MyNotePad()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"D:\";//load vị trí mở file đầu tiên của dialog
            openFileDialog1.Title = "Mở File";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (richTextBox1.Modified)
            {
                if (MessageBox.Show("Bạn có muốn lưu tập tin này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string text = File.ReadAllText(openFileDialog1.FileName);
                richTextBox1.Text = text;
                //richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.UnicodePlainText);
                //có thể thay bằng code trên
                //StreamReader r = new StreamReader(openFileDialog1.FileName);
                //richTextBox1.Text = r.ReadToEnd();
                //r.Close();
                namef = Path.GetFileName(openFileDialog1.FileName);
                this.Text = namef + " - MyNotePad";
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (namef == "noName")
            {
                saveAsToolStripMenuItem_Click(null, null);
            }
            else
            {
                StreamWriter w = new StreamWriter(this.openFileDialog1.FileName);
                w.Write(richTextBox1.Text);
                w.Close();
                namef = Path.GetFileName(saveFileDialog1.FileName);
                this.Text = namef + " - MyNotePad";
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //richTextBox1.SaveFile(saveFileDialog1.FileName);
                StreamWriter w = new StreamWriter(this.saveFileDialog1.FileName);
                w.Write(richTextBox1.Text);
                w.Close();
                namef = Path.GetFileName(saveFileDialog1.FileName);
                this.Text = namef + " - MyNotePad";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                if (MessageBox.Show("Bạn có muốn lưu tập tin này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Times New Roman", 11, FontStyle.Regular), Brushes.Black, new PointF());
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pageSetupDialog1.ShowDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.WindowState = FormWindowState.Normal;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }


        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo == true)
            {
                richTextBox1.Undo();
                //richTextBox1.ClearUndo();
            }

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo == true)
            {
                richTextBox1.Redo();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TabPage tp = new TabPage();
            //RichTextBox rtb = new RichTextBox();
            //rtb.Dock = DockStyle.Fill;
            //tp.Controls.Add(rtb);
            //tabControl1.TabPages.Add(tp);
            if (richTextBox1.Modified)
            {
                if (MessageBox.Show("Bạn có muốn lưu tập tin này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
            richTextBox1.Clear();
            this.Text = "noName - MyNotePad";
        }

        private void MyNotePad_Load(object sender, EventArgs e)
        {
            //namef = Path.GetFileName(openFileDialog1.FileName);
            this.Text = namef + " - " + this.Text;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                this.Text = namef + "* - MyNotePad";
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(null, null);
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(null, null);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(null, null);
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            selectAllToolStripMenuItem_Click(null, null);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tác quyền thuộc Nguyễn Nhật Thuận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
    }
}
