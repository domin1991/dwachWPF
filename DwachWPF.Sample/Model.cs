using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DwachWPF.Sample
{
    public class Model : INotifyPropertyChanged
    {
        private FirstEnum _flags;
        private SecondEnum _personProperty;
        private Level _level = Level.Normal;

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

        public Model()
        {
            Flags = (FirstEnum.SecondFlag | FirstEnum.FifthFlag);
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
