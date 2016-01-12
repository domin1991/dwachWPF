using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DwachWPF.Controls
{
    public partial class Plot
    {


        public Brush Background
        {
            get { return ( Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof( Brush), typeof(Plot), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));



        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Plot), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));



        public double BorderThickness
        {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(Plot), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));





        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(Plot), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));






        public Rect Range
        {
            get { return (Rect)GetValue(RangeProperty); }
            set { SetValue(RangeProperty, value); }
        }
        public static readonly DependencyProperty RangeProperty =
            DependencyProperty.Register("Range", typeof(Rect), typeof(Plot), new FrameworkPropertyMetadata(new Rect (-10.0, -10.0, 20, 20), FrameworkPropertyMetadataOptions.AffectsRender));

        
    }
}
