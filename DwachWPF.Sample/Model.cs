using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DwachWPF.Sample
{
    public class Model : INotifyPropertyChanged
    {
        private FirstEnum _flags;
        private bool _isScreenshotMode;
        private Level _level = Level.Normal;
        private SecondEnum _personProperty;
        private List<double> _sampleData;
        private IEnumerable<List<double>> _sampleDatas;
        private Func<double, double> _sampleFunc;
        private IEnumerable<Func<double, double>> _sampleFuncs;
        private IEnumerable<object> _multiplePlotSource;

        public Model()
        {
            Flags = (FirstEnum.SecondFlag | FirstEnum.FifthFlag);

            TakeScreenShotCommand = new ActionCommand<object>(x => IsScreenshotMode = true);
            SaveScreenshotCommand = new ActionCommand<byte[]>(SaveScreenshot);
            var data = new List<List<double>>();
            data.Add(GenerateValues());
            SampleDatas = data;
            SampleData = GenerateValues();

            SampleFuncs = new List<Func<double, double>>() { x => 0.5 * x * x - 9 };
            SampleFunc =  x => 0.5 * x * x - 9 ;

            MultiplePlotSource = new List<object>() { SampleFunc, SampleData };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public FirstEnum Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
                NotifyPropertyChanged();
            }
        }
        public Level Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
                NotifyPropertyChanged();
            }
        }
        public SecondEnum PersonProperty
        {
            get
            {
                return _personProperty;
            }
            set
            {
                _personProperty = value;
                NotifyPropertyChanged();
            }
        }

        public List<double> SampleData
        {
            get
            {
                return _sampleData;
            }
            set
            {
                if (_sampleData != value)
                {
                    _sampleData = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public IEnumerable<List<double>> SampleDatas
        {
            get
            {
                return _sampleDatas;
            }
            set
            {
                if (_sampleDatas != value)
                {
                    _sampleDatas = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Func<double, double> SampleFunc
        {
            get
            {
                return _sampleFunc;
            }
            set
            {
                if (_sampleFunc != value)
                {
                    _sampleFunc = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public IEnumerable<Func<double, double>> SampleFuncs
        {
            get
            {
                return _sampleFuncs;
            }
            set
            {
                if (_sampleFuncs != value)
                {
                    _sampleFuncs = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public IEnumerable<object> MultiplePlotSource
        {
            get
            {
                return _multiplePlotSource;
            }

            set
            {
                if (_multiplePlotSource != value)
                {
                    _multiplePlotSource = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsScreenshotMode
        {
            get
            {
                return _isScreenshotMode;
            }

            set
            {
                if (_isScreenshotMode != value)
                {
                    _isScreenshotMode = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ICommand SaveScreenshotCommand { get; set; }
        public ICommand TakeScreenShotCommand { get; set; }

        private List<double> GenerateValues()
        {
            var result = new List<double>();
            for (int i = -10; i < 20; i++)
            {
                result.Add(i);
                result.Add(i);
            }
            return result;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void SaveScreenshot(byte[] png)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Image Files | *.png";
            if (dialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(dialog.FileName);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                using (var fileStream = new FileStream(dialog.FileName, FileMode.CreateNew))
                {
                    fileStream.Write(png, 0, png.Count());
                }
            }
        }
    }
}
