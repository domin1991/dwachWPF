using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var namesAndDescriptions = GetNamesAndDescriptions(value);

            AddControls(isFlag, namesAndDescriptions);

            CheckSelected(value);

        }

        private void AddControls(bool isFlag, List<KeyValuePair<string, string>> namesAndDescriptions)
        {
            _stackPanel.Children.Clear();

            foreach (var nameAndDesctiption in namesAndDescriptions)
            {
                ToggleButton toggleButton;
                if (isFlag)
                {
                    toggleButton = new CheckBox();
                }
                else
                {
                    toggleButton = new RadioButton();
                }

                toggleButton.Name = nameAndDesctiption.Key;
                toggleButton.Content = nameAndDesctiption.Value;
                toggleButton.Click += CheckBoxChange;
                _stackPanel.Children.Add(toggleButton);
            }
        }

        private void CheckSelected(object value)
        {
            var selected = value.ToString();
            _stackPanel.Children
                .OfType<ToggleButton>()
                .Where(x => selected.Contains((string)x.Name)).ToList().ForEach(x => x.IsChecked = true);
        }

        private static List<KeyValuePair<string,string>> GetNamesAndDescriptions(object value)
        {
            return value.GetType().GetFields().Skip(1)
                            .Select(x =>
                            {
                                var descAttrib = x.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                                string description = descAttrib == null ? x.Name : ((DescriptionAttribute)descAttrib).Description;
                                return new KeyValuePair<string, string>(x.Name, description);
                            }).ToList();
        }

        private void CheckBoxChange(object sender, RoutedEventArgs e)
        {
            var selectedFlags = _stackPanel.Children
                .OfType<ToggleButton>()
                .Where(x => x.IsChecked == true)
                .Select(x => x.Name);

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
