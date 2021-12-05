using Buttplug;
using System;
using System.Collections.ObjectModel;

namespace Butthesda_Wpf
{
    class ViewModel : ViewModelBase
    {
        public enum Game
        {
            Skyrim,
            SkyrimSE,
            SkyrimVR,
            Fallout4
        }

        private object clientLock = new object();
        public ButtplugClient Client { get; private set; }

        public ObservableCollection<DeviceConfiguration> Devices { get; private set; }
        
        private DeviceConfiguration _CurrentDevice;

        public DeviceConfiguration CurrentDevice
        {
            get { return _CurrentDevice; }
            set { 
                _CurrentDevice = value;
                NotifyPropertyChanged("CurrentDevice");
            }
        }

        private Game selectedGame = Game.SkyrimSE;

        public Game SelectedGame
        {
            get
            {
                return selectedGame;
            }
            set
            {
                selectedGame = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("SkyrimSelected");
                NotifyPropertyChanged("SkyrimSESelected");
                NotifyPropertyChanged("SkyrimVRSelected");
                NotifyPropertyChanged("Fallout4Selected");
            }
        }

        public bool SkyrimSelected
        {
            get { return SelectedGame == Game.Skyrim; }
            set
            {
                if (value)
                {
                    SelectedGame = Game.Skyrim;
                }
            }
        }

        public bool SkyrimSESelected
        {
            get { return SelectedGame == Game.SkyrimSE; }
            set
            {
                if (value)
                {
                    SelectedGame = Game.SkyrimSE;
                }
            }
        }

        public bool SkyrimVRSelected
        {
            get { return SelectedGame == Game.SkyrimVR; }
            set
            {
                if (value)
                {
                    SelectedGame = Game.SkyrimVR;
                }
            }
        }

        public bool Fallout4Selected
        {
            get { return SelectedGame == Game.Fallout4; }
            set
            {
                if (value)
                {
                    SelectedGame = Game.Fallout4;
                }
            }
        }


        public ViewModel()
        {
            this.Devices = new ObservableCollection<DeviceConfiguration>();

            this.StartScanCommand =new DelegateCommand((_o) => { this.StartScan(); }) { CanExecuteValue = true };
            this.StopScanCommand = new DelegateCommand((_o) => { this.StopScan(); });


            var connector = new ButtplugEmbeddedConnectorOptions { ServerName = "Butthesda - WPF" };

            Client = new ButtplugClient("Butthesda - WPF");

            Client.DeviceAdded += Client_DeviceAdded;
            Client.DeviceRemoved += Client_DeviceRemoved;

            Client.ConnectAsync(connector); //TODO handle errors when using a WS Server
        }

        public DelegateCommand StartScanCommand { get; private set; }
        public DelegateCommand StopScanCommand { get; private set; }

        public void StartScan() 
        {
            lock (clientLock)
            {
                this.Client.StartScanningAsync(); //TODO handle errors when using a WS Server
                this.StartScanCommand.CanExecuteValue = false;
                this.StopScanCommand.CanExecuteValue = true;
            }
        }

        public void StopScan()
        {
            lock (clientLock)
            {
                this.Client.StartScanningAsync(); //TODO handle errors when using a WS Server
                this.StartScanCommand.CanExecuteValue = true;
                this.StopScanCommand.CanExecuteValue = false;
            }
        }

        private void Client_DeviceRemoved(object sender, DeviceRemovedEventArgs e)
        {
            OnUIThread(() =>
            {
                this.Devices.Remove(new DeviceConfiguration(e.Device));
            });
        }

        private void Client_DeviceAdded(object sender, DeviceAddedEventArgs e)
        {
            OnUIThread(() =>
            {
                this.Devices.Add(new DeviceConfiguration(e.Device));
            });
        }

        
    }
}
