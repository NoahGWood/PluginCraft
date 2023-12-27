using System.Diagnostics;

namespace GrblControllerPlugin
{
    public class GrblSettings
    {
        public GrblDevice Device { get; set; } // Used to update settings
        public int StepPulse { get; set; } // 0
        public int StepIdleDelay { get; protected set; } // 1
        public int StepPortInvertMask { get; protected set; } // 2
        public int DirectionPortInvertMask { get; protected set; } // 3
        public bool StepEnableInvert { get; protected set; } // 4
        public bool LimitPinsInvert { get; protected set; } // 5
        public bool ProbePinInvert { get; protected set; } // 6
        public int StatusReportMask { get; protected set; } // 10
        public float JunctionDeviationMM { get; protected set; } // 11
        public float ArcToleranceMM { get; protected set; } // 12
        public bool ReportInches { get; protected set; } // 13
        public bool SoftLimits { get; protected set; } // 20 
        public bool HardLimits { get; protected set; } // 21
        public bool HomingCycle { get; protected set; } // 22
        public int HomingDirInvert { get; protected set; } // 23 
        public float HomingFeedMM { get; protected set; } // 24
        public float HomingSeekMM { get; protected set; } // 25
        public int HomingDebounceMS { get; protected set; } // 26
        public float HomingPulloffMM { get; protected set; } // 27
        public float MaxSpindleSpeedRPM { get; protected set; } // 30
        public float MinSpindleSpeedRPM { get; protected set; } // 31
        public bool LaserMode { get; protected set; } // 32
        public float XStepsMM { get; protected set; } // 100
        public float YStepsMM { get; protected set; } // 101
        public float ZStepsMM { get; protected set; } // 102
        public float XMaxRateMM { get; protected set; } // 110
        public float YMaxRateMM { get; protected set; } // 111
        public float ZMaxRateMM { get; protected set; } // 112
        public float XAccelMM { get; protected set; } // 120
        public float YAccelMM { get; protected set; } // 121
        public float ZAccelMM { get; protected set; } // 122
        public float XMaxTravelMM { get; protected set; } // 130
        public float YMaxTravelMM { get; protected set; } // 131
        public float ZMaxTravelMM { get; protected set; } // 132
        public GrblSettings(GrblDevice device) { this.Device = device; }

        static List<int> ints = [0, 1, 2, 3, 10, 23, 26];
        static List<int> floats = [11, 12, 24, 25, 27, 30, 31, 100, 101, 102, 110, 111, 112, 120, 121, 122, 130, 131, 132];
        static List<int> bools = [4, 5, 6, 13, 20, 21, 22, 32];

        private Stopwatch sw;
        public static GrblSettings GenSettings(GrblDevice device)
        {
            GrblSettings dv = new(device);
            dv.LoadSettings();
            return dv;
        }
        public void LoadSettings()
        {
            Device.WriteData("$$");
            sw = Stopwatch.StartNew();
            while (!Device.AllData.Contains("ok"))
            {
                if (sw.ElapsedMilliseconds > 3000)
                    break;
            }
            string settings = Device.AllData.Split("ok")[0];
            settings = settings.Trim();
            try
            {
                foreach (string setting in settings.Split('\n'))
                {
                    Console.WriteLine(setting);
                    string[] sTypeVal = setting.Split("=");
                    string stype = sTypeVal[0].Replace("$", "");
                    int type = int.Parse(stype);
                    if (ints.Contains(type))
                    {
                        SetSetting(type, int.Parse(sTypeVal[1]), false);
                    }
                    else if (floats.Contains(type))
                    {
                        SetSetting(type, float.Parse(sTypeVal[1]), false);
                    }
                    else if (bools.Contains(type))
                    {
                        SetSetting(type, sTypeVal[1] == "0" ? false : true, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading settings from device {Device.Name}: {ex.Message}");
            }
            Device.AllData = "";
        }
        public void SetSetting(int id, int value, bool hwupdate=true)
        {
            switch (id)
            {
                case 0:
                    StepPulse = value;
                    break;
                case 1:
                    StepIdleDelay = value;
                    break;
                case 2:
                    StepPortInvertMask = value;
                    break;
                case 3:
                    DirectionPortInvertMask = value;
                    break;
                case 10:
                    StatusReportMask = value;
                    break;
                case 23:
                    HomingDirInvert = value;
                    break;
                case 26:
                    HomingDebounceMS = value;
                    break;
                default:
                    Console.WriteLine($"Error: {id}={value} is not a valid int or mask setting.");
                    return;
            }
            if(hwupdate)
                SetSetting<int>(id, value);
        }
        public void SetSetting(int id, bool value, bool hwupdate = true)
        {
            switch (id)
            {
                case 4:
                    StepEnableInvert = value;
                    break;
                case 5:
                    LimitPinsInvert = value;
                    break;
                case 6:
                    ProbePinInvert = value;
                    break;
                case 13:
                    ReportInches = value;
                    break;
                case 20:
                    SoftLimits = value;
                    break;
                case 21:
                    HardLimits = value;
                    break;
                case 22:
                    HomingCycle = value;
                    break;
                case 32:
                    LaserMode = value;
                    break;
                default:
                    Console.WriteLine($"Error: {id}={value} is not a recognized boolean setting.");
                    return;
            }
            if(hwupdate)
                SetSetting<int>(id, (value) ? 1 : 0);
        }
        public void SetSetting(int id, float value, bool hwupdate = true)
        {
            switch (id)
            {
                case 11:
                    JunctionDeviationMM = value;
                    break;
                case 12:
                    ArcToleranceMM = value;
                    break;
                case 24:
                    HomingFeedMM = value;
                    break;
                case 25:
                    HomingSeekMM = value;
                    break;
                case 27:
                    HomingPulloffMM = value;
                    break;
                case 30:
                    MaxSpindleSpeedRPM = value;
                    break;
                case 31:
                    MinSpindleSpeedRPM = value;
                    break;
                case 100:
                    XStepsMM = value;
                    break;
                case 101:
                    YStepsMM = value;
                    break;
                case 102:
                    ZStepsMM = value;
                    break;
                case 110:
                    XMaxRateMM = value;
                    break;
                case 111:
                    YMaxRateMM = value;
                    break;
                case 112:
                    ZMaxRateMM = value;
                    break;
                case 120:
                    XAccelMM = value;
                    break;
                case 121:
                    YAccelMM = value;
                    break;
                case 122:
                    ZAccelMM = value;
                    break;
                case 130:
                    XMaxTravelMM = value;
                    break;
                case 131:
                    YMaxTravelMM = value;
                    break;
                case 132:
                    ZMaxTravelMM = value;
                    break;
                default:
                    Console.WriteLine($"Error: {id}={value} is not a recognized float setting.");
                    return;
            }
            if(hwupdate)
                SetSetting<float>(id, value);
        }
        private void SetSetting<T>(int id, T value)
        {
            Device.WriteData('$' + $"{id}={value}");
        }
    }
}
