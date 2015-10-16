using System;
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

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
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
                var isChecked = selected.Contains(name);
                _stackPanel.Children.Add(new CheckBox() { Content = name, IsChecked = isChecked});
            }
        }

        public FlagsControl()
        {
            AddChild(_stackPanel);
        }
    }
}
