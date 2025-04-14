using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bindr
{
    public class LoadingAnimation : Control
    {
        private readonly Timer animationTimer;
        private float angle;
        private readonly int dotCount = 8;
        private readonly float dotRadius = 5f;
        private readonly float circleRadius = 20f;

        public LoadingAnimation()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            Size = new Size(60, 60);
            BackColor = Color.FromArgb(0, 255, 255, 255);

            animationTimer = new Timer
            {
                Interval = 50
            };
            animationTimer.Tick += AnimationTimer_Tick;
            DoubleBuffered = true; // Ensure smooth updates
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            angle = (angle + 5) % 360;
            Invalidate(true); // Force full repaint
            Update(); // Ensure immediate redraw
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && !DesignMode)
            {
                animationTimer.Start();
            }
            else
            {
                animationTimer.Stop();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float centerX = Width / 2f;
            float centerY = Height / 2f;

            for (int i = 0; i < dotCount; i++)
            {
                float dotAngle = angle + (i * 360f / dotCount);
                float rad = dotAngle * (float)Math.PI / 180;
                float x = centerX + (float)Math.Cos(rad) * circleRadius;
                float y = centerY + (float)Math.Sin(rad) * circleRadius;

                // Pulse effect: scale dot size based on position
                float scale = 1f + 0.5f * (float)Math.Sin(rad + angle * Math.PI / 180);
                float size = dotRadius * scale;

                // Color gradient: cycle through hues
                Color dotColor = Color.FromArgb(255,
                    (int)(127 + 127 * Math.Sin(rad)),
                    (int)(127 + 127 * Math.Cos(rad)),
                    (int)(127 - 127 * Math.Sin(rad)));

                using (Brush brush = new SolidBrush(dotColor))
                {
                    e.Graphics.FillEllipse(brush, x - size, y - size, size * 2, size * 2);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}