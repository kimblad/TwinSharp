using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TwinSharpControls
{
    [DefaultEvent("Click")]
    public partial class NcButton : UserControl
    {
        protected bool mousedown = false;
        protected bool mouseover = false;


        bool blinkEnabled = false;
        int blinkInterval = 500;
        bool blinkSignalToPaint = false;

        bool drawAsDown = false;

        int roundTopRadius = 5;
        int roundBottomRadius = 5;



        readonly System.Windows.Forms.Timer blinker;
        public NcButton()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.Dpi;
            this.SetStyle(ControlStyles.StandardDoubleClick, false);
            BackColor = ColorTranslator.FromHtml("#2D2D2D");
            ForeColor = ColorTranslator.FromHtml("#9F9F9F");

            if (new FontConverter().ConvertFromInvariantString("Bahnschrift, 16pt") is Font font)
                Font = font;

            TextAlign = ContentAlignment.MiddleCenter;
            TextColor = Color.FromArgb(255, 240, 240, 239);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            blinker = new System.Windows.Forms.Timer();
            blinker.Interval = blinkInterval;
            blinker.Enabled = blinkEnabled;
            blinker.Tick += Blinker_Tick;
        }

        [Category("Appearance")]
        [Description("Rounded corner radius on top of control")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public bool DrawAsDown
        {
            get { return drawAsDown; }
            set
            {

                if (drawAsDown != value)
                {
                    drawAsDown = value;
                    Invalidate();
                }

            }
        }

        [Category("Appearance")]
        [Description("Rounded corner radius on top of control")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public int RoundTopRadius
        {
            get { return roundTopRadius; }
            set
            {

                if (roundTopRadius != value)
                {
                    roundTopRadius = value;
                    Invalidate();
                }

            }
        }

        [Category("Appearance")]
        [Description("Corner radius on bottom of control")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public int RoundBottomRadius
        {
            get { return roundBottomRadius; }
            set
            {
                if (roundBottomRadius != value)
                {
                    roundBottomRadius = value;
                    Invalidate();
                }
            }
        }


        Color backColorGradient1 = Color.CadetBlue;
        public Color BackColorGradient1
        {
            get => backColorGradient1;
            set
            {
                if (backColorGradient1 != value)
                {
                    backColorGradient1 = value;
                    Invalidate();
                }
            }
        }

        Color backColorGradient2 = Color.SteelBlue;
        public Color BackColorGradient2
        {
            get => backColorGradient2;
            set
            {
                if (backColorGradient2 != value)
                {
                    backColorGradient2 = value;
                    Invalidate();
                }
            }
        }




        protected void PaintBackBox(Graphics g)
        {
            if (Parent != null)
            {
                if (Parent.BackColor != Color.Transparent)
                    g.Clear(Parent.BackColor);
                else
                    g.Clear(SystemColors.Control);
            }
            else
                g.Clear(Color.White);

            DrawBox(g, Bounds, RoundTopRadius, RoundBottomRadius);
        }

        private void DrawBox(Graphics g, Rectangle bounds, int roundTopRadius, int roundBottomRadius)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            bounds = new Rectangle(0, 0, bounds.Width, bounds.Height);



            var p1 = new Point(bounds.X, bounds.Y);
            var p2 = new Point(bounds.Right, bounds.Bottom);

            //using (SolidBrush brush = new SolidBrush(color))
            using (var linBrush = new LinearGradientBrush(p1, p2, backColorGradient1, backColorGradient2))
            {
                FillRoundRectangle(g, linBrush, bounds, roundTopRadius, roundBottomRadius);
            }

        }

        private void FillRoundRectangle(Graphics g, LinearGradientBrush brush, Rectangle rect, int topRadius, int bottomRadius)
        {
            if (g == null)
                return;

            GraphicsPath path = new GraphicsPath();


            if (topRadius <= 0 && bottomRadius <= 0)
            {
                // just a square
                g.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
            }
            else // the rectangle is rounded in some way
            {
                path.AddLine(rect.X + topRadius, rect.Y, rect.X + rect.Width - (topRadius * 2), rect.Y);

                // topright fillet
                if (topRadius > 0)
                    path.AddArc(rect.X + rect.Width - (topRadius * 2), rect.Y, topRadius * 2, topRadius * 2, 270, 90);

                path.AddLine(rect.X + rect.Width, rect.Y + topRadius, rect.X + rect.Width, rect.Y + rect.Height - (bottomRadius * 2));

                // bottomright fillet
                if (bottomRadius > 0)
                    path.AddArc(rect.X + rect.Width - (bottomRadius * 2), rect.Y + rect.Height - (bottomRadius * 2), bottomRadius * 2, bottomRadius * 2, 0, 90);


                path.AddLine(rect.X + rect.Width - (bottomRadius * 2), rect.Y + rect.Height, rect.X + bottomRadius, rect.Y + rect.Height);

                // bottomleft fillet
                if (bottomRadius > 0)
                    path.AddArc(rect.X, rect.Y + rect.Height - (bottomRadius * 2), bottomRadius * 2, bottomRadius * 2, 90, 90);

                path.AddLine(rect.X, rect.Y + rect.Height - (bottomRadius * 2), rect.X, rect.Y + topRadius);

                // topleft fillet
                if (topRadius > 0)
                    path.AddArc(rect.X, rect.Y, topRadius * 2, topRadius * 2, 180, 90);


                path.CloseFigure();

                g.FillPath(brush, path);
            }
        }

        public bool BlinkEnabled
        {
            get => blinkEnabled;
            set
            {
                blinkEnabled = value;
                blinker.Enabled = value;

                if(!value)
                {
                    blinkSignalToPaint = false;
                    Invalidate();
                }
            }
        }

        private void Blinker_Tick(object? sender, EventArgs e)
        {
            blinkSignalToPaint = !blinkSignalToPaint;
            Invalidate();
        }

        private void HMIButton_Paint(object sender, PaintEventArgs e)
        {
            bool sunken = SunkenState || drawAsDown;


            PaintBackBox(e.Graphics);
            DrawLabel(e.Graphics, Font, Bounds, Text, sunken, TextColor);
        
            if(sunken || blinkSignalToPaint)
            {
                e.Graphics.FillRectangle(Brushes.LawnGreen, 3, 3, 6, 6);
            }
        }


        private void DrawLabel(Graphics g, Font font, Rectangle bounds, string text, bool sunken, Color captionTextColor)
        {
            if (string.IsNullOrEmpty(text))
                return;

            //Split text into lines
            text = text.TrimEnd();
            string[] lines = text.Split(["\r\n", "\n"], StringSplitOptions.None);


            float oneHeight = g.MeasureString("A", font).Height;
            float totalHeight = oneHeight * lines.Length;


            float top = (float)(bounds.Height - totalHeight) * 0.5f;
            if(top < 6)
                top = 6;

            if (sunken)
            {
                top += 2;
            }


            foreach (var line in lines)
            {
                // Compute  width and height of one line in label
                var txtsize = g.MeasureString(line, font, bounds.Width * 3); //*3 if font slightly out of bounds


                // Compute top left of label default to center center.
                float left = (float)(bounds.Width - txtsize.Width) * 0.5f;


                if (sunken)
                {
                    left += 2;
                }


                using (Brush brush = new SolidBrush(captionTextColor))
                    g.DrawString(line, font, brush, left, top);

                //Move to new line.
                top += oneHeight;
            }
        }



       




        [Category("Appearance")]
        [Description("Caption")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        [AllowNull]
        public override string Text 
        { 
            get => base.Text;
            set 
            { 
                base.Text = value; 
                Invalidate(); 
            } 
        }
       


        ContentAlignment _textAlign;
        [Description("The alignment of the text that will be displayed on the control"), Category("Appearance")]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set => _textAlign = value;
        }


        private Color textColor;
        public Color TextColor
        {
            get => textColor;
            set => textColor = value;
        }




        [DefaultValue(typeof(Font), "Arial, 18pt")]
        [AllowNull]
        public override Font Font
        {
            get => base.Font;
            set 
            {
                base.Font = value;
                Invalidate(); 
            }
        }

        [DefaultValue(typeof(Color), "#2D2D2D")]
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }


        [DefaultValue(typeof(Color), "#9F9F9F")]
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            bool s = SunkenState;
            if (!mousedown)
            {
                mousedown = true;
                if (SunkenState != s)
                    Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected bool SunkenState => mouseover && mousedown; // if the button should be drawn in a down state

        protected override void OnMouseEnter(EventArgs e)
        {
            bool s = SunkenState;
            if (!mouseover)
            {
                mouseover = true;
                if (SunkenState != s)
                    Invalidate();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            bool s = SunkenState;
            if (mouseover)
            {
                mouseover = false;
                if (SunkenState != s)
                    Invalidate();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            //If a messagebox is shown in MouseDown we will not get the OnMouseUp event.
            //But we will get this event.
            bool s = SunkenState;
            if (mousedown || mouseover)
            {
                mousedown = false;
                if (SunkenState != s)
                    Invalidate();
            }

            base.OnLostFocus(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            bool s = SunkenState;
            if (mousedown)
            {
                mousedown = false;
                if (SunkenState != s)
                    Invalidate();
            }
            base.OnMouseUp(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {


        }



        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
            base.OnSizeChanged(e);
        }
    }
}
