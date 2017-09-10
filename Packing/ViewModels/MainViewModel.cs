using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packing
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public Command PackingCommand => _packingCommand ?? (_packingCommand = new Command(Pack));
        public Command UnpackingCommand => _unpackingCommand ?? (_unpackingCommand = new Command(Unpack));
        public Command BytifyCommand => _bytifyCommand ?? (_bytifyCommand = new Command(TextToBytes));
        public Command UnbytifyCommand => _unbytifyCommand ?? (_unbytifyCommand = new Command(BytesToText));

        public string PackedText
        {
            get => _packedText;
            set
            {
                if(_packedText != value)
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

        private Command _packingCommand;
        private Command _unpackingCommand;
        private Command _bytifyCommand;
        private Command _unbytifyCommand;

        private string _packedText;
        private string _unpackedText;
        private string _bytesText;

        private void TextToBytes()
        {
            var bytes = Encoding.UTF8.GetBytes(UnpackedText);
            BytesText = BytesToBitRows(bytes);
        }

        private void BytesToText()
        {
            var bytes = BitRowsToBytes(BytesText);
            UnpackedText = Encoding.UTF8.GetString(bytes);
        }

        private void Pack()
        {
            var worker = new Packer();
            var bytes = worker.Encode(UnpackedText);
            PackedText = BytesToBitRows(bytes);
        }

        private void Unpack()
        {
            var worker = new Unpacker();
            var parsedBytes = BitRowsToBytes(PackedText);
            UnpackedText = worker.Decode(parsedBytes);
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
                try
                {
                    var item = Convert.ToByte(line, 2);
                    parsedBytes.Add(item);
                }
                catch (Exception ex)
                {
                    // TODO: handle
                }
            };
            return parsedBytes.ToArray();
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
