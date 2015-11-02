using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DwachWPF.Controls
{
    public partial class Ruler
    {
        public decimal Start
        {
            get { return (decimal)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(decimal), typeof(Ruler), new FrameworkPropertyMetadata(0.0m, FrameworkPropertyMetadataOptions.AffectsRender));



        public decimal Stop
        {
            get { return (decimal)GetValue(StopProperty); }
            set { SetValue(StopProperty, value); }
        }

        public static readonly DependencyProperty StopProperty =
            DependencyProperty.Register("Stop", typeof(decimal), typeof(Ruler), new FrameworkPropertyMetadata(100.0m, FrameworkPropertyMetadataOptions.AffectsRender));



        public decimal Step
        {
            get { return (Decimal)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }
        
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(decimal), typeof(Ruler), new FrameworkPropertyMetadata(10.0m, FrameworkPropertyMetadataOptions.AffectsRender));


        public decimal SmallStep
        {
            get { return (decimal)GetValue(SmallStepProperty); }
            set { SetValue(SmallStepProperty, value); }
        }
        
        public static readonly DependencyProperty SmallStepProperty =
            DependencyProperty.Register("SmallStep", typeof(decimal), typeof(Ruler), new FrameworkPropertyMetadata(2.0m, FrameworkPropertyMetadataOptions.AffectsRender));



        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }
        
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(Ruler), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));


        public Brush IndycatorBrush
        {
            get { return (Brush)GetValue(IndycatorBrushProperty); }
            set { SetValue(IndycatorBrushProperty, value); }
        }
        
        public static readonly DependencyProperty IndycatorBrushProperty =
            DependencyProperty.Register("IndycatorBrush", typeof(Brush), typeof(Ruler), new FrameworkPropertyMetadata(Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender));
    }
}
