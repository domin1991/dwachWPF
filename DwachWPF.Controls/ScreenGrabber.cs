using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DwachWPF.Controls
{
    public class ScreenGrabber : Canvas
    {
        private Point _dragPosition;
        private Point _selectionRectangleBegin;
        private Point _selectionRectangleEnd;

        private Rectangle _selectionRectangle = new Rectangle();

        private Rectangle _leftRectangle = new Rectangle();
        private Rectangle _topRectangle = new Rectangle();
        private Rectangle _rightRectangle = new Rectangle();
        private Rectangle _bottomRectangle = new Rectangle();

        private List<Rectangle> _maskRectangles = new List<Rectangle>();

        public double MaskOpacity
        {
            set
            {
                _maskRectangles.ForEach(x => x.Opacity = value);
            }
        }

        public Brush MaskColor
        {
            set
            {
                _maskRectangles.ForEach(x => x.Fill = value);
            }
        }



        public bool IsScreenshotMode
        {
            get { return (bool)GetValue(IsScreenshotModeProperty); }
            set { SetValue(IsScreenshotModeProperty, value); }
        }
        
        public static readonly DependencyProperty IsScreenshotModeProperty =
            DependencyProperty.Register("IsScreenshotMode", typeof(bool), typeof(ScreenGrabber), new PropertyMetadata(propertyChangedCallback: ModeChanged));

        private static void ModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var screenGrabber = d as ScreenGrabber;
            bool isVisible = (bool)e.NewValue;
            screenGrabber.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public ICommand SavePngCommand
        {
            get { return (ICommand)GetValue(SavePngCommandProperty); }
            set { SetValue(SavePngCommandProperty, value); }
        }

        public static readonly DependencyProperty SavePngCommandProperty =
            DependencyProperty.Register("SavePngCommand", typeof(ICommand), typeof(ScreenGrabber), new PropertyMetadata());
        

        public ScreenGrabber()
        {
            Visibility = Visibility.Hidden;
            Background = Brushes.Transparent;

            PrepareMask();

            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;

            Cursor = Cursors.Cross;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                SetRectangle(_bottomRectangle, new Point(), new Point(ActualWidth, ActualHeight));
                return;
            }
            var currentPosition = e.GetPosition(this);

            _selectionRectangleBegin = new Point(Math.Min(_dragPosition.X, currentPosition.X), Math.Min(_dragPosition.Y, currentPosition.Y));
            _selectionRectangleEnd = new Point(Math.Max(_dragPosition.X, currentPosition.X), Math.Max(_dragPosition.Y, currentPosition.Y));

            RedrawMask(_selectionRectangleBegin, _selectionRectangleEnd);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _dragPosition = e.GetPosition(this);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(this) as UIElement;
            var png = GetPng(parent, new Rect(_selectionRectangleBegin, _selectionRectangleEnd));

            if (SavePngCommand != null)
            {
                SavePngCommand.Execute(png);
            }

            RedrawMask(new Point(), new Point());
            Visibility = Visibility.Hidden;
            IsScreenshotMode = false;
        }

        private void PrepareMask()
        {
            _selectionRectangle.Stroke = Brushes.Black;
            _selectionRectangle.StrokeThickness = 1.0;

            _maskRectangles.Add(_leftRectangle);
            _maskRectangles.Add(_topRectangle);
            _maskRectangles.Add(_rightRectangle);
            _maskRectangles.Add(_bottomRectangle);

            _maskRectangles.ForEach(x =>
            {
                Children.Add(x);
            });

            Children.Add(_selectionRectangle);
        }

        private void RedrawMask(Point begin, Point end)
        {
            SetRectangle(_selectionRectangle, begin, end);

            SetRectangle(_topRectangle, new Point(0.0, 0.0), new Point(ActualWidth, begin.Y));
            SetRectangle(_bottomRectangle, new Point(0.0, end.Y), new Point(ActualWidth, ActualHeight));
            SetRectangle(_leftRectangle, new Point(0.0, begin.Y), new Point(begin.X, end.Y));
            SetRectangle(_rightRectangle, new Point(end.X, begin.Y), new Point(ActualWidth, end.Y));
        }

        private void SetRectangle(Rectangle rectangle, Point begin, Point end)
        {
            var delta = end - begin;

            SetLeft(rectangle, begin.X);
            SetTop(rectangle, begin.Y);
            rectangle.Width = delta.X;
            rectangle.Height = delta.Y;
        }

        private byte[] GetPng(UIElement source, Rect rect)
        {
            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;

            rect = rect != null ? rect : new Rect(new Point(0, 0), new Point(actualWidth, actualHeight));

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)actualWidth, (int)actualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTarget.Render(source);

            var croppedBitmap = new CroppedBitmap(renderTarget, new Int32Rect((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));

            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(croppedBitmap));

            using (MemoryStream outputStream = new MemoryStream())
            {
                pngEncoder.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}


