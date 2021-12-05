using Buttplug;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using static Buttplug.ServerMessage.Types;

namespace Butthesda_Wpf
{
    class DeviceConfiguration : ViewModelBase, IEquatable<DeviceConfiguration>
    {
        [Flags]
        public enum BodyPart
        {
            Head = 0x0001,
            Body = 0x0002,
            Breast = 0x0004,
            Belly = 0x0008,
            Feet = 0x0010,
            Mouth = 0x0020,
            Vaginal = 0x0040,
            Clit = 0x0080,
            Anal = 0x0100
        }

        [Flags]
        public enum EventType
        {
            Shock = 1,
            Damage = 2,
            Penetrate = 4,
            Vibrate = 8,
            Equip = 16
        }

        public class Target
        {
            public enum CommandType
            {
                Vibrate,
                Linear,
                Rotate
            }

            public CommandType Command
            {
                get;
                set;
            }

            public string Name
            {
                get
                {
                    return $"{Command} - {Index+1}";
                }
            }

            public int Index { get; set; }

            public class Row : ViewModelBase
            {
                private bool _Shock;

                public bool Shock
                {
                    get { return _Shock; }
                    set { { _Shock = value; NotifyPropertyChanged(); } }
                }

                private bool _Damage;

                public bool Damage
                {
                    get { return _Damage; }
                    set { { _Damage = value; NotifyPropertyChanged(); } }
                }

                private bool _Penetrate;

                public bool Penetrate
                {
                    get { return _Penetrate; }
                    set { { _Penetrate = value; NotifyPropertyChanged(); } }
                }

                private bool _Vibrate;

                public bool Vibrate
                {
                    get { return _Vibrate; }
                    set { { _Vibrate = value; NotifyPropertyChanged(); } }
                }

                private bool _Equip;

                public bool Equip
                {
                    get { return _Equip; }
                    set { { _Equip = value; NotifyPropertyChanged(); } }
                }

                public string Name { get; set; }

            }
            public Row Head { get; set; } = new Row();
            public Row Body { get; set; } = new Row();
            public Row Breast { get; set; } = new Row();
            public Row Belly { get; set; } = new Row();
            public Row Feet { get; set; } = new Row();
            public Row Mouth { get; set; } = new Row();
            public Row Vaginal { get; set; } = new Row();
            public Row Clit { get; set; } = new Row();
            public Row Anal { get; set; } = new Row();
        }

        public List<Target> Targets { get; } = new List<Target>();


        private Target currentTarget;

        public Target CurrentTarget
        {
            get { return currentTarget; }
            set { currentTarget = value;
                NotifyPropertyChanged();
            }
        }

        public ButtplugClientDevice Device { get; private set; }

        public DeviceConfiguration(ButtplugClientDevice device)
        {
            this.Device = device;
            ButtplugMessageAttributes atts;
            if (device.AllowedMessages.TryGetValue(MessageAttributeType.VibrateCmd, out atts))
            {
                for(int i = 0; i< atts.FeatureCount; i++)
                {
                    Targets.Add(new Target { Command = Target.CommandType.Vibrate, Index = i });
                }
            }

            if (device.AllowedMessages.TryGetValue(MessageAttributeType.LinearCmd, out atts))
            {
                for (int i = 0; i < atts.FeatureCount; i++)
                {
                    Targets.Add(new Target { Command = Target.CommandType.Linear, Index = i });
                }
            }

            if (device.AllowedMessages.TryGetValue(MessageAttributeType.RotateCmd, out atts))
            {
                for (int i = 0; i < atts.FeatureCount; i++)
                {
                    Targets.Add(new Target { Command = Target.CommandType.Rotate, Index = i });
                }
            }
        }

        public bool Equals([AllowNull] DeviceConfiguration other)
        {
            return this.Device.Equals(other.Device);
        }
    }
}
