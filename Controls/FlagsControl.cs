using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private static void SourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flagsControl = d as FlagsControl;

            flagsControl.ReloadLabels(e.NewValue);
        }

        private void ReloadLabels(object value)
        {
            var names = Enum.GetNames(value.GetType());
            var selected = value.ToString();

            _stackPanel.Children.Clear();
            foreach (var name in names)
            {
                var checkBox = new CheckBox() { Content = name };
                checkBox.IsChecked = selected.Contains(name);
                checkBox.Checked += CheckBoxChange;
                checkBox.Unchecked += CheckBoxChange;

                _stackPanel.Children.Add(checkBox);
            }
        }

        private void CheckBoxChange(object sender, RoutedEventArgs e)
        {
            var selectedFlags =_stackPanel.Children
                .OfType<CheckBox>()
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
