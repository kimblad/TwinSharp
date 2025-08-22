using System.Drawing.Drawing2D;

namespace TwinSharpControls
{
    public partial class BarberPole : UserControl
    {
        float speed = 2.0f;
        float y1;
        float oneRibbonHeight = 10.0f;
        const float ribbonAngle = (float)(30.0 * (Math.PI / 180.0));

        float y2;
        float rightSideYoffset;


        public BarberPole()
        {
            InitializeComponent();

            //Set styles to avoid flickering during redraw.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);


            CalculateStripeDimensions();
        }


        public double Speed 
        { 
            get => speed * 10.0; 
            set => speed = (float)value / 10.0f; 
        }


        private void BarberPole_Paint(object sender, PaintEventArgs e)
        {
            using var path = new GraphicsPath();
            path.AddLine(0, y1, Width, y1 + rightSideYoffset);
            path.AddLine(Width, y1 + rightSideYoffset, Width, y1 + rightSideYoffset + oneRibbonHeight);
            path.AddLine(Width, y1 + oneRibbonHeight + rightSideYoffset, 0, y1 + oneRibbonHeight);


            using var path2 = new GraphicsPath();
            path2.AddLine(0, y2, Width, y2 + rightSideYoffset);
            path2.AddLine(Width, y2 + rightSideYoffset, Width, y2 + rightSideYoffset + oneRibbonHeight);
            path2.AddLine(Width, y2 + oneRibbonHeight + rightSideYoffset, 0, y2 + oneRibbonHeight);


            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            e.Graphics.FillPath(Brushes.Red, path);
            e.Graphics.FillPath(Brushes.Blue, path2);

            //If we are unlucky, speed variable changes between these two calls.
            //Then ribbons will appear smashed together.
            //Make a local copy.
            float speedCopy = speed;
            y1 -= speedCopy;
            y2 -= speedCopy;


            if (y1 > Height)
                y1 = -rightSideYoffset - oneRibbonHeight;
            else if (y1 < -rightSideYoffset - oneRibbonHeight)
                y1 = Height;

            if (y2 > Height)
                y2 = -rightSideYoffset - oneRibbonHeight;
            else if (y2 < -rightSideYoffset - oneRibbonHeight)
                y2 = Height;
        }

        public void UpdatePole()
        {
            Invalidate();
        }

        private void BarberPole_Resize(object sender, EventArgs e)
        {
            CalculateStripeDimensions();
        }

        private void CalculateStripeDimensions()
        {
            //Calculate the offset of the right side of the ribbon.
            rightSideYoffset = (float)Math.Sin(ribbonAngle) * Width;

            y1 = -rightSideYoffset;

            oneRibbonHeight = (Height + rightSideYoffset) / 3.0f;
            y2 = -rightSideYoffset + ((Height + rightSideYoffset + oneRibbonHeight) / 2.0f);

        }
    }
}


