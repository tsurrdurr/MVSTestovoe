using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Packing
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            time.Elapsed += Time_Elapsed;
        }

        public Command PackingCommand => _packingCommand ?? (_packingCommand = new Command(Pack));
        public Command UnpackingCommand => _unpackingCommand ?? (_unpackingCommand = new Command(Unpack));
        public Command BytifyCommand => _bytifyCommand ?? (_bytifyCommand = new Command(TextToBytes));
        public Command UnbytifyCommand => _unbytifyCommand ?? (_unbytifyCommand = new Command(BytesToText));

        public string PackedText
        {
            get => _packedText;
            set
            {
                if (_packedText != value)
                {
                    _packedText = value;
                    OnPropertyChanged(nameof(PackedText));
                }
            }
        }

        public string UnpackedText
        {
            get => _unpackedText;
            set
            {
                if (_unpackedText != value)
                {
                    _unpackedText = value;
                    OnPropertyChanged(nameof(UnpackedText));
                }
            }
        }

        public string BytesText
        {
            get => _bytesText;
            set
            {
                if (_bytesText != value)
                {
                    _bytesText = value;
                    OnPropertyChanged(nameof(BytesText));
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                    time.Enabled = true;
                }
            }
        }

        private Timer time = new Timer
        {
            Interval = 2000,
            Enabled = false
        };

        private Command _packingCommand;
        private Command _unpackingCommand;
        private Command _bytifyCommand;
        private Command _unbytifyCommand;

        private string _packedText;
        private string _unpackedText;
        private string _bytesText;
        private string _status;
        private const string invalidInputMessage = "Неверный ввод";

        private void TextToBytes()
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(UnpackedText);
                BytesText = BytesToBitRows(bytes);
            }
            catch (Exception ex)
            {
                Status = invalidInputMessage;
            }
        }

        private void BytesToText()
        {
            try
            {
                var bytes = BitRowsToBytes(BytesText);
                UnpackedText = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                if (ex is FormatException) Status = ex.Message;
                else Status = invalidInputMessage;
            }
        }

        private void Pack()
        {
            try
            {
                var worker = new Packer();
                var bytes = worker.Encode(UnpackedText);
                PackedText = BytesToBitRows(bytes);
            }
            catch (Exception ex)
            {
                Status = invalidInputMessage;
            }
        }

        private void Unpack()
        {
            try
            {
                var worker = new Unpacker();
                var parsedBytes = BitRowsToBytes(PackedText);
                UnpackedText = worker.Decode(parsedBytes);
            }
            catch (Exception ex)
            {
                if (ex is FormatException) Status = ex.Message;
                else Status = invalidInputMessage;
            }
}

        private string BytesToBitRows(byte[] bytes)
        {
            string result = "";
            foreach (var @byte in bytes)
            {
                string bits = Convert.ToString(@byte, 2);
                while (bits.Length < 8) bits = "0" + bits;
                result += bits + "\r\n";
            }
            return result;
        }

        private byte[] BitRowsToBytes(string text)
        {
            string[] lines = text.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var parsedBytes = new List<byte>();
            foreach (var line in lines)
            {
                if (line.Length > 8) throw new FormatException("Значение длиной больше байта");
                var item = Convert.ToByte(line, 2);
                parsedBytes.Add(item);
            }
            return parsedBytes.ToArray();

        }

        private void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            Status = "";
            time.Enabled = false;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

        }
        #endregion
    }
}
