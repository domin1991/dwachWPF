﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DwachWPF.Controls
{
    public partial class Plot : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Pen borderPen = new Pen(BorderBrush, BorderThickness);

            drawingContext.DrawRectangle(Background, borderPen, new Rect(new Point(0.0, 0.0), new Point(ActualWidth, ActualHeight)));

            DrawSource(drawingContext);
            ClipToBounds = true;
        }

        private void DrawSource(DrawingContext drawingContext)
        {
            var enumerableSource = Source as IEnumerable<object>;
            if (enumerableSource != null && enumerableSource.Any())
            {
                foreach (var obj in enumerableSource)
                {
                    DrawObject(drawingContext, obj);
                }
                return;
            }

            DrawObject(drawingContext, Source);
        }

        private void DrawObject(DrawingContext drawingContext, object obj)
        {
            var plotSource = obj as PlotSource;
            if (plotSource != null)
            {
                return;
            }

            var function = obj as Func<double, double>;
            if (function != null)
            {
                DrawFunction(drawingContext, function);
                return;
            }

            var enumerableDoubleSource = obj as IEnumerable<double>;
            if (enumerableDoubleSource != null)
            {
                DrawValues(drawingContext, enumerableDoubleSource);
                return;
            }
        }

        private void DrawValues(DrawingContext drawingContext, IEnumerable<double> values)
        {
            var enumerator = values.GetEnumerator();
            double x;
            double y;
            while (enumerator.MoveNext())
            {
                x = TranslateX(enumerator.Current);
                if (enumerator.MoveNext())
                {
                    y = TranslateY(enumerator.Current);

                    drawingContext.DrawEllipse(Brushes.Red, new Pen(Brushes.Red, 1.0), new Point(x, y), 1.0, 1.0);
                }
            }
        }

        private void DrawFunction(DrawingContext drawingContext, Func<double, double> function)
        {
            var args = GetArgs();
            Point? last = null;
            foreach (var arg in args)
            {
                var x = TranslateX(arg);
                var y = TranslateY(function(arg));

                if (last != null)
                {
                    drawingContext.DrawLine(new Pen(Brushes.Red, 1.0), last.Value, new Point(x, y));
                }
                last = new Point(x, y);
            }
        }

        private List<double> GetArgs()
        {
            var result = new List<double>();
            double step = (ActualWidth / Range.Width);
            for (int i = 0; i < ActualWidth; i++)
            {
                result.Add(((double)i / step) + Range.X);
            }
            return result;
        }

        private double TranslateX(double x)
        {
            return ((x - Range.X) * (ActualWidth / Range.Width));
        }

        private double TranslateY(double y)
        {
            return ActualHeight - ((y - Range.Y) * (ActualHeight / Range.Height));
        }
    }
}
