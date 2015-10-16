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
        private SecondEnum _personProperty;
        private Level _level = Level.Normal;
        private bool _isScreenshotMode;

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

        public ICommand TakeScreenShotCommand { get; set; }

        public ICommand SaveScreenshotCommand { get; set; }

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

        public Model()
        {
            Flags = (FirstEnum.SecondFlag | FirstEnum.FifthFlag);

            TakeScreenShotCommand = new ActionCommand<object>(x => IsScreenshotMode = true);
            SaveScreenshotCommand = new ActionCommand<byte[]>(SaveScreenshot);
        }

        private void SaveScreenshot(byte[] png)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Image Files | *.png";
            if (dialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(dialog.FileName);
                if(fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                using (var fileStream = new FileStream(dialog.FileName, FileMode.CreateNew))
                {
                    fileStream.Write(png, 0, png.Count());
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
