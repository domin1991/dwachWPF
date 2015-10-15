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
            var names = Enum.GetNames(e.NewValue.GetType());

            flagsControl.ReloadLabels(names);
        }

        private void ReloadLabels(string[] names)
        {
            _stackPanel.Children.Clear();
            foreach (var name in names)
            {
                _stackPanel.Children.Add(new CheckBox() { Content = name });
            }
        }

        public FlagsControl()
        {
            AddChild(_stackPanel);
        }
    }
}
