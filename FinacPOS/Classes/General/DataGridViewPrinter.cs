using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;

namespace FinacPOS
{
    class DataGridViewPrinter
    {
        // The DataGridView Control which will be printed
        private DataGridView TheDataGridView;
        // The PrintDocument to be used for printing
        private PrintDocument ThePrintDocument;
        // Determine if the report will be
        // printed in the Top-Center of the page
        private bool IsCenterOnPage;
        // Determine if the page contain title text
        private bool IsWithTitle;
        // The title text to be printed
        // in each page (if IsWithTitle is set to true)
        private string TheTitleText;
        private string TheHeading;
        // The font to be used with the title
        // text (if IsWithTitle is set to true)
        private string TheSchoolName;
        private string TheAddress;
        private string ThePhone;
        private string label1;
        private string label2;
        private string label3;
        private string label4;

        private string TheDate;
        private Font TheTitleFont;
        //private Image TheImage;
        // The color to be used with the title
        // text (if IsWithTitle is set to true)
        private Color TheTitleColor;
        // Determine if paging is used
        private bool IsWithPaging;

        // A static parameter that keep track
        // on which Row (in the DataGridView control)
        // that should be printed
        static int CurrentRow;

        static int PageNumber;

        private int PageWidth;
        private int PageHeight;
        private int LeftMargin;
        private int TopMargin;
        private int RightMargin;
        private int BottomMargin;


        // A parameter that keep track
        // on the y coordinate of the page,
        // so the next object to be printed
        // will start from this y coordinate
        private float CurrentY;

        private float RowHeaderHeight;
        private List<float> RowsHeight;
        private List<float> ColumnsWidth;
        private float TheDataGridViewWidth;

        // Maintain a generic list to hold start/stop
        // points for the column printing
        // This will be used for wrapping
        // in situations where the DataGridView will not
        // fit on a single page
        private List<int[]> mColumnPoints;
        private List<float> mColumnPointsWidth;
        private int mColumnPoint;

        // The class constructor
        public DataGridViewPrinter(DataGridView aDataGridView, PrintDocument aPrintDocument, bool CenterOnPage, bool WithTitle, string aTitleText, string aSchoolName, string aAddress, string aPhone, Font aTitleFont, Color aTitleColor, bool WithPaging, string Date, string Label1, string Label2, string Label3, string Label4)
        {
            TheDataGridView = aDataGridView;
            ThePrintDocument = aPrintDocument;
            if (aPrintDocument.DocumentName == "Ledger Details")
            {
                TheTitleText = aPrintDocument.DocumentName;
                TheHeading = aTitleText;
            }
            else
            {
                TheTitleText = aTitleText;
            }
            label1 = Label1;
            label2 = Label2;
            label3 = Label3;
            label4 = Label4;
            IsCenterOnPage = CenterOnPage;
            IsWithTitle = WithTitle;
            TheDate = Date;
            TheSchoolName = aSchoolName;
            TheAddress = aAddress;
            ThePhone = aPhone;
            TheTitleFont = aTitleFont;
            TheTitleColor = aTitleColor;
            IsWithPaging = WithPaging;
        


            PageNumber = 0;

            RowsHeight = new List<float>();
            ColumnsWidth = new List<float>();

            mColumnPoints = new List<int[]>();
            mColumnPointsWidth = new List<float>();

            // Claculating the PageWidth and the PageHeight
            if (!ThePrintDocument.DefaultPageSettings.Landscape)
            {
                PageWidth =
                  ThePrintDocument.DefaultPageSettings.PaperSize.Width;
                PageHeight =
                  ThePrintDocument.DefaultPageSettings.PaperSize.Height;
            }
            else
            {
                PageHeight =
                  ThePrintDocument.DefaultPageSettings.PaperSize.Width;
                PageWidth =
                  ThePrintDocument.DefaultPageSettings.PaperSize.Height;
            }

            // Claculating the page margins

            LeftMargin = 10;
            RightMargin = 10;
            

            TopMargin = ThePrintDocument.DefaultPageSettings.Margins.Top;

            BottomMargin = ThePrintDocument.DefaultPageSettings.Margins.Bottom;

            // First, the current row to be printed
            // is the first row in the DataGridView control
            CurrentRow = 0;




        }


        private void Calculate(Graphics g)
        {
            if (PageNumber == 0)
            // Just calculate once
            {
                SizeF tmpSize = new SizeF();
                Font tmpFont;
                float tmpWidth;

                TheDataGridViewWidth = 0;
                for (int i = 0; i < TheDataGridView.Columns.Count; i++)
                {
                    tmpFont = TheDataGridView.ColumnHeadersDefaultCellStyle.Font;
                    if (tmpFont == null)
                        // If there is no special HeaderFont style,
                        // then use the default DataGridView font style
                        tmpFont = TheDataGridView.DefaultCellStyle.Font;

                    tmpSize = g.MeasureString(TheDataGridView.Columns[i].HeaderText, tmpFont);

                    tmpWidth = tmpSize.Width;





                    for (int j = 0; j < TheDataGridView.Rows.Count; j++)
                    {
                        tmpFont = TheDataGridView.Rows[j].DefaultCellStyle.Font;
                        if (tmpFont == null)
                            // If the there is no special font style of the
                            // CurrentRow, then use the default one associated
                            // with the DataGridView control

                            tmpFont = TheDataGridView.DefaultCellStyle.Font;

                        tmpSize = g.MeasureString("Anything", tmpFont);
                  

                        tmpSize = g.MeasureString(TheDataGridView.Rows[j].Cells[i].EditedFormattedValue.ToString(), tmpFont);



                        if (tmpSize.Width > tmpWidth)
                            tmpWidth = tmpSize.Width;

                        tmpSize = g.MeasureString(TheDataGridView.Rows[j].Cells[i].Size.ToString(), tmpFont);
                        RowHeaderHeight = TheDataGridView.Rows[j].Cells[i].Size.Height;

                        RowsHeight.Add(RowHeaderHeight);
                    }
                    if (TheDataGridView.Columns[i].Visible)
                        TheDataGridViewWidth += tmpWidth;
                    ColumnsWidth.Add(tmpWidth);
                }

                // Define the start/stop column points
                // based on the page width and
                // the DataGridView Width
                // We will use this to determine
                // the columns which are drawn on each page
                // and how wrapping will be handled
                // By default, the wrapping will occurr
                // such that the maximum number of
                // columns for a page will be determine
                int k;

                int mStartPoint = 0;
                for (k = 0; k < TheDataGridView.Columns.Count; k++)
                    if (TheDataGridView.Columns[k].Visible)
                    {
                        mStartPoint = k;
                        break;
                    }

                int mEndPoint = TheDataGridView.Columns.Count;
                for (k = TheDataGridView.Columns.Count - 1; k >= 0; k--)
                    if (TheDataGridView.Columns[k].Visible)
                    {
                        mEndPoint = k + 1;
                        break;
                    }

                float mTempWidth = TheDataGridViewWidth;
                float mTempPrintArea = (float)PageWidth - (float)LeftMargin -
                    (float)RightMargin;

                // We only care about handling
                // where the total datagridview width is bigger
                // then the print area
                if (TheDataGridViewWidth > mTempPrintArea)
                {
                    mTempWidth = 0.0F;
                    for (k = 0; k < TheDataGridView.Columns.Count; k++)
                    {
                        if (TheDataGridView.Columns[k].Visible)
                        {
                            mTempWidth += ColumnsWidth[k];
                            // If the width is bigger
                            // than the page area, then define a new
                            // column print range
                            if (mTempWidth > mTempPrintArea)
                            {
                                mTempWidth -= ColumnsWidth[k];
                                mColumnPoints.Add(new int[] { mStartPoint, mEndPoint });
                                mColumnPointsWidth.Add(mTempWidth);
                                mStartPoint = k;
                                mTempWidth = ColumnsWidth[k];
                            }
                        }
                        // Our end point is actually
                        // one index above the current index
                        mEndPoint = k + 1;
                    }
                }
                // Add the last set of columns
                mColumnPoints.Add(new int[] { mStartPoint, mEndPoint });
                mColumnPointsWidth.Add(mTempWidth);
                mColumnPoint = 0;
            }



        }
        // The funtion that print the title, page number, and the header row
        private void DrawHeader(Graphics g)
        {
            CurrentY = (float)TopMargin;

            // Printing the page number (if isWithPaging is set to true)
            if (IsWithPaging)
            {
                PageNumber++;
                string PageString = "Page:" + PageNumber.ToString();

                StringFormat PageStringFormat = new StringFormat();
                PageStringFormat.Trimming = StringTrimming.Word;
                PageStringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                PageStringFormat.Alignment = StringAlignment.Far;



                Font PageStringFont = new Font("Times New Roman", 10, FontStyle.Regular, GraphicsUnit.Point);

                RectangleF PageStringRectangle = new RectangleF((float)LeftMargin, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(PageString, PageStringFont).Height);

                g.DrawString(PageString, PageStringFont, new SolidBrush(Color.Black), PageStringRectangle, PageStringFormat);

                CurrentY += g.MeasureString(PageString, PageStringFont).Height;




            }

            // Printing the title (if IsWithTitle is set to true)
            if (IsWithTitle)
            {
                StringFormat TitleFormat = new StringFormat();
                TitleFormat.Trimming = StringTrimming.Word;
                TitleFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

                StringFormat TitleFormatSchool = new StringFormat();
                if (IsCenterOnPage)
                    TitleFormatSchool.Alignment = StringAlignment.Center;
                else
                    TitleFormatSchool.Alignment = StringAlignment.Near;
                TitleFormatSchool.Trimming = StringTrimming.Word;
                TitleFormatSchool.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
               
                CurrentY = CurrentY + 20;
                Font heading = new Font("Times New Roman", 20, FontStyle.Bold);
                Font heading1 = new Font("Times New Roman", 12, FontStyle.Bold);
                Font heading2 = new Font("Times New Roman", 9, FontStyle.Regular);
                Font heading4 = new Font("Times New Roman", 12, FontStyle.Underline);
                
                RectangleF TitleRectangleSchool = new RectangleF((float)LeftMargin, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheSchoolName, heading).Height);
                g.DrawString(TheSchoolName, heading, Brushes.Black, TitleRectangleSchool, TitleFormatSchool);
                CurrentY += g.MeasureString(TheSchoolName, heading).Height;
                RectangleF TitleRectangleAddress = new RectangleF((float)LeftMargin, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheAddress, heading1).Height);
                CurrentY = CurrentY + 20;
                g.DrawString(TheAddress, heading1, Brushes.Black, TitleRectangleAddress, TitleFormatSchool);
                CurrentY += g.MeasureString(TheAddress, heading1).Height;
                CurrentY = CurrentY + 20;
                RectangleF TitleRectanglePhone = new RectangleF((float)LeftMargin, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, heading1).Height);
                g.DrawString("Phone :" + ThePhone, heading2, Brushes.Black, TitleRectanglePhone, TitleFormatSchool);
                CurrentY += g.MeasureString(ThePhone, heading2).Height;
                CurrentY = CurrentY + 20;
                RectangleF TitleRectangleDate = new RectangleF((float)LeftMargin, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, heading1).Height);
                g.DrawString(TheDate, heading2, Brushes.Black, TitleRectangleDate, TitleFormatSchool);
                CurrentY += g.MeasureString(TheDate, heading2).Height;

                if (IsCenterOnPage)
                    TitleFormat.Alignment = StringAlignment.Center;
                else
                    TitleFormat.Alignment = StringAlignment.Near;

         
                RectangleF TitleRectangle = new RectangleF((float)LeftMargin, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, heading4).Height);

                g.DrawString(TheTitleText, heading4, new SolidBrush(TheTitleColor), TitleRectangle, TitleFormat);

                CurrentY += g.MeasureString(TheTitleText, heading4).Height;

                RectangleF HeadingRectangle = new RectangleF((float)LeftMargin, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, heading4).Height);

                g.DrawString(TheHeading, heading1, new SolidBrush(TheTitleColor), HeadingRectangle, TitleFormat);

                CurrentY += g.MeasureString(TheHeading, heading1).Height;
                CurrentY = CurrentY + 20;

                if (TheTitleText == "Cash Book" || TheTitleText == "Bank Book")
                {
                    RectangleF Titlelabel4 = new RectangleF((float)100, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, heading1).Height);
                    g.DrawString(label4, heading1, Brushes.Black, Titlelabel4, TitleFormat);
                }


                CurrentY += g.MeasureString(label4, heading1).Height;
                CurrentY = CurrentY + 20;
            }

            // Calculating the starting x coordinate
            // that the printing process will start from
            float CurrentX = (float)LeftMargin;
            if (IsCenterOnPage)
                CurrentX += (((float)PageWidth - (float)RightMargin -
                  (float)LeftMargin) - mColumnPointsWidth[mColumnPoint]) / 2.0F;

            // Setting the HeaderFore style
            Color HeaderForeColor =
                  TheDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;
            if (HeaderForeColor.IsEmpty)

                // If there is no special HeaderFore style,
                // then use the default DataGridView style
                HeaderForeColor = TheDataGridView.DefaultCellStyle.ForeColor;
            SolidBrush HeaderForeBrush = new SolidBrush(HeaderForeColor);

            // Setting the HeaderBack style
            Color HeaderBackColor =
                  TheDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            if (HeaderBackColor.IsEmpty)
                // If there is no special HeaderBack style,
                // then use the default DataGridView style
                HeaderBackColor = TheDataGridView.DefaultCellStyle.BackColor;
            SolidBrush HeaderBackBrush = new SolidBrush(HeaderBackColor);

            // Setting the LinePen that will
            // be used to draw lines and rectangles
            // (derived from the GridColor property
            // of the DataGridView control)
            Pen TheLinePen = new Pen(TheDataGridView.GridColor, 1);

            // Setting the HeaderFont style
            Font HeaderFont = TheDataGridView.ColumnHeadersDefaultCellStyle.Font;
            if (HeaderFont == null)
                // If there is no special HeaderFont style,
                // then use the default DataGridView font style
                HeaderFont = TheDataGridView.DefaultCellStyle.Font;

            // Calculating and drawing the HeaderBounds        
            RectangleF HeaderBounds = new RectangleF(CurrentX, CurrentY,
                mColumnPointsWidth[mColumnPoint], RowHeaderHeight);
            g.FillRectangle(HeaderBackBrush, HeaderBounds);

            // Setting the format that will be
            // used to print each cell of the header row
            StringFormat CellFormat = new StringFormat();
            CellFormat.Trimming = StringTrimming.Word;
            CellFormat.FormatFlags = StringFormatFlags.NoWrap |
               StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            // Printing each visible cell of the header row
            RectangleF CellBounds;
            float ColumnWidth = 0; ;
            for (int i = (int)mColumnPoints[mColumnPoint].GetValue(0);
                i < (int)mColumnPoints[mColumnPoint].GetValue(1); i++)
            {
                // If the column is not visible then ignore this iteration
                if (!TheDataGridView.Columns[i].Visible) continue;
                ColumnWidth = ColumnsWidth[i];

                // Check the CurrentCell alignment
                // and apply it to the CellFormat
                if (TheDataGridView.ColumnHeadersDefaultCellStyle.
                           Alignment.ToString().Contains("Right"))
                    CellFormat.Alignment = StringAlignment.Far;
                else if (TheDataGridView.ColumnHeadersDefaultCellStyle.
                         Alignment.ToString().Contains("Center"))
                    CellFormat.Alignment = StringAlignment.Center;
                else
                    CellFormat.Alignment = StringAlignment.Near;

                CellBounds = new RectangleF(CurrentX, CurrentY,
                             ColumnWidth, RowHeaderHeight);

                // Printing the cell text
                g.DrawString(TheDataGridView.Columns[i].HeaderText,
                             HeaderFont, HeaderForeBrush,
                   CellBounds, CellFormat);

                // Drawing the cell bounds
                // Draw the cell border only if the HeaderBorderStyle is not None
                if (TheDataGridView.RowHeadersBorderStyle !=
                                DataGridViewHeaderBorderStyle.None)
                    g.DrawRectangle(TheLinePen, CurrentX, CurrentY, ColumnWidth,
                        RowHeaderHeight);

                CurrentX += ColumnWidth;






            }




            CurrentY += RowHeaderHeight;
            //RectangleF CellBounds;
            //   float ColumnWidth = 0;


        }

        // The function that print a bunch of rows that fit in one page
        // When it returns true, meaning that
        // there are more rows still not printed,
        // so another PagePrint action is required
        // When it returns false, meaning that all rows are printed
        // (the CureentRow parameter reaches
        // the last row of the DataGridView control)
        // and no further PagePrint action is required
        private bool DrawRows(Graphics g)
        {


            // Setting the LinePen that will be used to draw lines and rectangles
            // (derived from the GridColor property of the DataGridView control)
            Pen TheLinePen = new Pen(TheDataGridView.GridColor, 1);

            // The style paramters that will be used to print each cell
            Font RowFont;
            Color RowForeColor;
            Color RowBackColor;

            SolidBrush RowForeBrush;
            SolidBrush RowBackBrush;
            SolidBrush RowAlternatingBackBrush;

            // Setting the format that will be used to print each cell
            StringFormat CellFormat = new StringFormat();
            CellFormat.Trimming = StringTrimming.Word;
            CellFormat.FormatFlags = StringFormatFlags.NoWrap |
                                     StringFormatFlags.LineLimit;

            // Printing each visible cell
            RectangleF RowBounds;
            float CurrentX;
            float ColumnWidth;
            while (CurrentRow < TheDataGridView.Rows.Count)
            {
                // Print the cells of the CurrentRow only if that row is visible
                if (TheDataGridView.Rows[CurrentRow].Visible)
                {
                    // Setting the row font style
                    RowFont = TheDataGridView.Rows[CurrentRow].DefaultCellStyle.Font;


                    // If the there is no special font style of the CurrentRow,
                    // then use the default one associated with the DataGridView control
                    if (RowFont == null)

                        RowFont = TheDataGridView.DefaultCellStyle.Font;

                    // Setting the RowFore style


                    RowForeColor =
                      TheDataGridView.Rows[CurrentRow].DefaultCellStyle.ForeColor;


                    // If the there is no special RowFore style of the CurrentRow,
                    // then use the default one associated with the DataGridView control 
                    //commented
                    //if (RowForeColor.IsEmpty)
                    //    RowForeColor = TheDataGridView.DefaultCellStyle.ForeColor;
                    RowForeBrush = new SolidBrush(Color.Black);

                    // Setting the RowBack (for even rows) and the RowAlternatingBack
                    // (for odd rows) styles
                    RowBackColor =
                      TheDataGridView.Rows[CurrentRow].DefaultCellStyle.BackColor;
                    // If the there is no special RowBack style of the CurrentRow,
                    // then use the default one associated with the DataGridView control
                    if (RowBackColor.IsEmpty)
                    {
                        RowBackBrush = new SolidBrush(
                              TheDataGridView.DefaultCellStyle.BackColor);
                        RowAlternatingBackBrush = new
                            SolidBrush(
                            TheDataGridView.AlternatingRowsDefaultCellStyle.BackColor);
                    }
                    // If the there is a special RowBack style of the CurrentRow,
                    // then use it for both the RowBack and the RowAlternatingBack styles
                    else
                    {
                        RowBackBrush = new SolidBrush(Color.LightGray);
                        RowAlternatingBackBrush = new SolidBrush(RowBackColor);
                    }

                    // Calculating the starting x coordinate
                    // that the printing process will
                    // start from
                    CurrentX = (float)LeftMargin;
                    if (IsCenterOnPage)
                        CurrentX += (((float)PageWidth - (float)RightMargin -
                            (float)LeftMargin) -
                            mColumnPointsWidth[mColumnPoint]) / 2.0F;

                    // Calculating the entire CurrentRow bounds                
                    RowBounds = new RectangleF(CurrentX, CurrentY,
                        mColumnPointsWidth[mColumnPoint], 20);

                    // Filling the back of the CurrentRow
                    if (CurrentRow % 2 == 0)
                        g.FillRectangle(RowBackBrush, RowBounds);
                    else
                        g.FillRectangle(RowAlternatingBackBrush, RowBounds);

                    // Printing each visible cell of the CurrentRow                
                    for (int CurrentCell = (int)mColumnPoints[mColumnPoint].GetValue(0);
                        CurrentCell < (int)mColumnPoints[mColumnPoint].GetValue(1);
                        CurrentCell++)
                    {
                        //Commented

                        if (TheDataGridView[CurrentCell, CurrentRow].Style.Font != null)
                        {
                            RowFont = TheDataGridView[CurrentCell, CurrentRow].Style.Font;
                        }
                        else if (TheDataGridView.Rows[CurrentRow].DefaultCellStyle.Font != null)
                        {
                            RowFont = TheDataGridView.Rows[CurrentRow].DefaultCellStyle.Font;
                        }
                        else
                        {
                            RowFont = TheDataGridView.DefaultCellStyle.Font;
                        }

                        if (TheDataGridView[CurrentCell, CurrentRow].Style.BackColor != null)
                        {
                            RowBackColor = TheDataGridView[CurrentCell, CurrentRow].Style.BackColor;
                            RowBackBrush = new SolidBrush(RowBackColor);
                        }


                        // If the cell is belong to invisible
                        // column, then ignore this iteration
                        if (!TheDataGridView.Columns[CurrentCell].Visible) continue;

                        // Check the CurrentCell alignment
                        // and apply it to the CellFormat
                        if (TheDataGridView.Columns[CurrentCell].DefaultCellStyle.
                                Alignment.ToString().Contains("Right"))
                            CellFormat.Alignment = StringAlignment.Far;
                        else if (TheDataGridView.Columns[CurrentCell].DefaultCellStyle.
                                Alignment.ToString().Contains("Center"))
                            CellFormat.Alignment = StringAlignment.Center;
                        else
                            CellFormat.Alignment = StringAlignment.Near;

                        ColumnWidth = ColumnsWidth[CurrentCell];
                        RectangleF CellBounds = new RectangleF(CurrentX, CurrentY,
                            ColumnWidth, RowsHeight[CurrentRow]);

                        g.FillRectangle(RowBackBrush, CurrentX, CurrentY,
                                ColumnWidth, RowsHeight[CurrentRow]);

                        // Printing the cell text
                        g.DrawString(
                          TheDataGridView.Rows[CurrentRow].Cells[CurrentCell].
                          EditedFormattedValue.ToString(), RowFont, RowForeBrush,
                          CellBounds, CellFormat);


                        // Drawing the cell bounds
                        // Draw the cell border only
                        // if the CellBorderStyle is not None


                        if (TheDataGridView.CellBorderStyle !=
                                    DataGridViewCellBorderStyle.None)
                            g.DrawRectangle(TheLinePen, CurrentX, CurrentY,
                                  ColumnWidth, RowsHeight[CurrentRow]);

                        CurrentX += ColumnWidth;
                    }
                    CurrentY += RowsHeight[CurrentRow];



                    // Checking if the CurrentY is exceeds the page boundries
                    // If so then exit the function and returning true meaning another
                    // PagePrint action is required
                    if ((int)CurrentY > (PageHeight - TopMargin - BottomMargin))
                    {
                        CurrentRow++;
                        return true;
                    }
                }
                CurrentRow++;




            }


            StringFormat Format = new StringFormat();


            Format.Alignment = StringAlignment.Near;
            Format.Trimming = StringTrimming.Word;
            Format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
 


            Font headingNew = new Font("Times New Roman", 9, FontStyle.Regular);
            Font headingBold = new Font("Times New Roman", 9, FontStyle.Bold);
            CurrentY = CurrentY + 20;
            CurrentX = 100;
            if (TheTitleText == "Outstanding Report")
            {
                RectangleF Total = new RectangleF((float)CurrentX, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, headingBold).Height);
                g.DrawString("Total", headingBold, Brushes.Black, Total, Format);

                CurrentX = CurrentX + g.MeasureString("Total     ", headingBold).Width + 5;
            }



            RectangleF TitleTotalLabel1 = new RectangleF((float)CurrentX, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, headingBold).Height);
            g.DrawString(label1, headingBold, Brushes.Black, TitleTotalLabel1, Format);

            CurrentX = CurrentX + g.MeasureString(label1, headingBold).Width + 5;
            RectangleF TitleTotallabel2 = new RectangleF((float)CurrentX, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, headingBold).Height);
            g.DrawString(label2, headingBold, Brushes.Black, TitleTotallabel2, Format);

            CurrentX = CurrentX + g.MeasureString(label2, headingBold).Width + 5;
            if (label1 == "" && label2 == "")
            {
                RectangleF Titlelabel3 = new RectangleF((float)600, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, headingBold).Height);
                g.DrawString(label3, headingBold, Brushes.Black, Titlelabel3, Format);
            }
            else
            {
                RectangleF TitleClosing = new RectangleF((float)CurrentX, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, headingBold).Height);
                g.DrawString(label3, headingBold, Brushes.Black, TitleClosing, Format);
            }
            CurrentX = CurrentX + g.MeasureString(label2, headingBold).Width + 5;
            if (TheTitleText != "Cash Book" && TheTitleText != "Bank Book")
            {

                RectangleF TitleTotallabel4 = new RectangleF((float)CurrentX, CurrentY, (float)PageWidth - (float)RightMargin - (float)LeftMargin, g.MeasureString(TheTitleText, headingBold).Height);
                g.DrawString(label4, headingBold, Brushes.Black, TitleTotallabel4, Format);

            }
            CurrentY = CurrentY + 20;
            // }
            CurrentRow = 0;
            // Continue to print the next group of columns
            mColumnPoint++;

            if (mColumnPoint == mColumnPoints.Count)
            // Which means all columns are printed
            {
                mColumnPoint = 0;
                return false;
            }
            else
                return true;

        }

        // The method that calls all other functions
        public bool DrawDataGridView(Graphics g)
        {
            try
            {
                Calculate(g);
                DrawHeader(g);
                bool bContinue = DrawRows(g);
                return bContinue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation failed: " + ex.Message.ToString(),
                    Application.ProductName + " - Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
    }
}