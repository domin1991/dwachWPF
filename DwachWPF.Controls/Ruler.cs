using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DwachWPF.Controls
{
    public partial class Ruler : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Pen borderPen = new Pen(Brush, 1.0);
            Pen pen = new Pen(Brush, 1.0);

            drawingContext.DrawRectangle(null, borderPen, new Rect(new Point(0.0, 0.0), new Point(ActualWidth, ActualHeight)));
            DrawSmallSteps(drawingContext, pen);
            DrawSteps(drawingContext, pen);
        }

        private void DrawSmallSteps(DrawingContext drawingContext, Pen pen)
        {
            for (decimal i = Start; i < Stop; i += SmallStep)
            {
                double x = ((double)((i - Start) / (Stop - Start)) * ActualWidth);

                drawingContext.DrawLine(pen, new Point(x, ActualHeight), new Point(x, ActualHeight * 0.8));
            }
        }

        private void DrawSteps(DrawingContext drawingContext, Pen pen)
        {
            for (decimal i = Start; i < Stop; i += Step)
            {
                double x = ((double)((i - Start) / (Stop - Start)) * ActualWidth);

                drawingContext.DrawLine(pen, new Point(x, ActualHeight), new Point(x, ActualHeight * 0.6));

                FormattedText value = new FormattedText(i.ToString(CultureInfo.CurrentCulture),
                            CultureInfo.CurrentCulture,
                            FlowDirection.LeftToRight,
                            new Typeface("Arial"),
                            PtToDip((int)(ActualHeight / 3)),
                            Brush);
                value.SetFontWeight(FontWeights.Regular);
                value.TextAlignment = TextAlignment.Center;

                drawingContext.DrawText(value, new Point(x, ActualHeight * 0.1));
            }
        }

        private double PtToDip(int pt)
        {
            return (pt * 96.0 / 72.0);
        }
        
    }
}
