using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DwachWPF.Controls
{
    public class FlagsControl : UserControl
    {
        private StackPanel _stackPanel = new StackPanel();

        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(FlagsControl), new PropertyMetadata(propertyChangedCallback: SourceChange));



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FlagsControl), new PropertyMetadata(Orientation.Vertical, propertyChangedCallback: OrientationChange));

        private static void OrientationChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flagsControl = d as FlagsControl;
            flagsControl._stackPanel.Orientation = (Orientation)e.NewValue;
        }

        private static void SourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flagsControl = d as FlagsControl;

            flagsControl.ReloadLabels(e.NewValue);
        }

        private void ReloadLabels(object value)
        {
            var isFlag = value.GetType().GetCustomAttributes(typeof(FlagsAttribute), false).Any();

            var names = Enum.GetNames(value.GetType());
            var selected = value.ToString();

            _stackPanel.Children.Clear();

            foreach (var name in names)
            {
                ToggleButton checkBox;
                if (isFlag)
                {
                    checkBox = new CheckBox();
                }
                else
                {
                    checkBox = new RadioButton();
                }

                checkBox.Content = name;
                checkBox.Click += CheckBoxChange;
                _stackPanel.Children.Add(checkBox);
            }

            _stackPanel.Children
                .OfType<ToggleButton>()
                .Where(x => selected.Contains((string)x.Content)).ToList().ForEach(x => x.IsChecked = true);

        }

        private void CheckBoxChange(object sender, RoutedEventArgs e)
        {
            var selectedFlags = _stackPanel.Children
                .OfType<ToggleButton>()
                .Where(x => x.IsChecked == true)
                .Select(x => x.Content);
            var stringEnum = string.Join(", ", selectedFlags);
            if (string.IsNullOrWhiteSpace(stringEnum))
            {
                Source = Activator.CreateInstance(Source.GetType());
            }
            else
            {
                Source = Enum.Parse(Source.GetType(), stringEnum);
            }
        }

        public FlagsControl()
        {
            AddChild(_stackPanel);
        }
    }
}
