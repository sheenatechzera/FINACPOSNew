using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace FinacPOS
{
   
        public class MultiColumnComboBox : ComboBox
        {
            private Color _BackColorEven = Color.White;
            private Color _BackColorOdd = Color.White;

            public MultiColumnComboBox()
            {
                DrawMode = DrawMode.OwnerDrawVariable;
            }

            public new DrawMode DrawMode
            {
                get
                {
                    return base.DrawMode;
                }
                set
                {
                    if (value != DrawMode.OwnerDrawVariable)
                    {
                        throw new NotSupportedException("Needs to be DrawMode.OwnerDrawVariable");
                    }
                    base.DrawMode = value;
                }
            }

            public new ComboBoxStyle DropDownStyle
            {
                get
                {
                    return base.DropDownStyle;
                }
                set
                {
                    if (value == ComboBoxStyle.Simple)
                    {
                        throw new NotSupportedException("ComboBoxStyle.Simple not supported");
                    }
                    base.DropDownStyle = value;
                }
            }

            protected override void OnDataSourceChanged(EventArgs e)
            {
                base.OnDataSourceChanged(e);

                InitializeColumns();
            }

            protected override void OnValueMemberChanged(EventArgs e)
            {
                base.OnValueMemberChanged(e);

                InitializeValueMemberColumn();
            }

            protected override void OnDropDown(EventArgs e)
            {
                base.OnDropDown(e);
                this.DropDownWidth = (int)CalculateTotalWidth();
            }

            const int columnPadding = 5;
            private float[] columnWidths = new float[0];
            private String[] columnNames = new String[0];
            private int ValueMemberColumnIndex = 0;

            private void InitializeColumns()
            {
                if (this.DataSource != null)
                {
                    PropertyDescriptorCollection propertyDescriptorCollection = DataManager.GetItemProperties();

                    columnWidths = new float[propertyDescriptorCollection.Count];
                    columnNames = new String[propertyDescriptorCollection.Count];

                    for (int colIndex = 0; colIndex < propertyDescriptorCollection.Count; colIndex++)
                    {
                        String name = propertyDescriptorCollection[colIndex].Name;
                        columnNames[colIndex] = name;
                    }
                }
            }

            private void InitializeValueMemberColumn()
            {
                int colIndex = 0;
                foreach (String columnName in columnNames)
                {
                    if (String.Compare(columnName,ValueMember, true, CultureInfo.CurrentUICulture) == 0)
                    {
                        ValueMemberColumnIndex = colIndex;
                        break;
                    }
                    colIndex++;
                }
            }
            //****************color*************************
            public Color BackColorEven
            {
                get
                {
                    return _BackColorEven;
                }
                set
                {
                    _BackColorEven = value;
                }
            }

            public Color BackColorOdd
            {
                get
                {
                    return _BackColorOdd;
                }
                set
                {
                    _BackColorOdd = value;
                }
            }
            //*********************************************

            private float CalculateTotalWidth()
            {
                float totWidth = 0;
                foreach (int width in columnWidths)
                {
                    totWidth += (width + columnPadding);
                }
                return totWidth + SystemInformation.VerticalScrollBarWidth;
            }

            protected override void OnMeasureItem(MeasureItemEventArgs e)
            {
                base.OnMeasureItem(e);

                if (DesignMode)
                    return;

                for (int colIndex = 0; colIndex < columnNames.Length; colIndex++)
                {
                    string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[colIndex]));
                    SizeF sizeF = e.Graphics.MeasureString(item, Font);
                    columnWidths[colIndex] = Math.Max(columnWidths[colIndex], sizeF.Width);
                }

                float totWidth = CalculateTotalWidth();

                e.ItemWidth = (int)totWidth;
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                SolidBrush brush1 = new SolidBrush(this.ForeColor);
                base.OnDrawItem(e);

                if (DesignMode)
                    return;

                e.DrawBackground();

                Rectangle boundsRect = e.Bounds;
                int lastRight = 0;
                //*****************
                Color brushForeColor;
                if ((e.State & DrawItemState.Selected) == 0)
                {
                    // Item is not selected. Use BackColorOdd & BackColorEven
                    Color backColor;
                    backColor = Convert.ToBoolean(e.Index % 2) ? _BackColorOdd : _BackColorEven;
                    using (SolidBrush brushBackColor = new SolidBrush(backColor))
                    {
                        e.Graphics.FillRectangle(brushBackColor, e.Bounds);
                    }
                    brushForeColor = Color.Black;
                }
                else
                {
                    // Item is selected. Use ForeColor = White
                    brushForeColor = Color.White;
                }

                //*****************
                using (Pen linePen = new Pen(Color.Black))
                {
                    using (SolidBrush brush = new SolidBrush(brushForeColor))
                    {
                        if (columnNames.Length == 0)
                        {
                            e.Graphics.DrawString(Convert.ToString(Items[e.Index]), Font, brush, boundsRect);
                        }
                        else
                        {
                            for (int colIndex = 1; colIndex < columnNames.Length; colIndex++)
                            {
                                string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[colIndex]));
                                boundsRect.X = lastRight;
                                boundsRect.Width = (int)columnWidths[colIndex] + columnPadding;
                                lastRight = boundsRect.Right;
                                if (colIndex == ValueMemberColumnIndex)
                                {
                                    using (Font boldFont = new Font(Font, FontStyle.Bold))
                                    {
                                        //e.Graphics.DrawString(item, boldFont, brush, boundsRect);
                                    }
                                }
                                else
                                {
                                    e.Graphics.DrawString(item, Font, brush, boundsRect);
                                }

                                if (e.State != DrawItemState.Selected)
                                {
                                    // if the item is not selected draw it with a different color
                                    //e.Graphics.DrawString(item, Font, new SolidBrush(Color.Black), boundsRect);
                                }
                                else
                                {
                                    // if the item is selected draw it with a different color
                                    //e.Graphics.DrawString(item, Font, new SolidBrush(Color.White), boundsRect);
                                }
                                if (colIndex < columnNames.Length - 1)
                                {
                                    e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);
                                }
                            }
                        }
                    }
                }
                e.DrawFocusRectangle();
            }


        }
    
}
