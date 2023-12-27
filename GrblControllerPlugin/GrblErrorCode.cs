namespace GrblControllerPlugin
{
    public class GrblErrorCode
    {
        public int ErrorCode { get; private set; }
        public string Message { get; private set; }

        public GrblErrorCode(int code)
        {
            ErrorCode = code;
            Message = ParseErrorCode(code);
        }
        public static string ParseErrorCode(int code)
        {
            switch(code)
            {
                case 1: return "GCode Command letter was not found.";
                case 2: return "GCode Command value invalid or missing.";
                case 3: return "Grbl '$' not recognized or supported.";
                case 4: return "Negative value for an expected positive value.";
                case 5: return "Homing fail. Homing not enabled in settings.";
                case 6: return "Min step pulse must be greater than 3usec.";
                case 7: return "EEPROM read failed. Default values used.";
                case 8: return "Grbl '$' command Only valid when Idle.";
                case 9: return "GCode commands invalid in alarm or jog state.";
                case 10: return "Soft limits require homing to be enabled.";
                case 11: return "Max characters per line exceeded. Ignored.";
                case 12: return "Grbl '$' setting exceeds the maximum step rate.";
                case 13: return "Safety door opened and door state initiated.";
                case 14: return "Build info or start-up line > EEPROM line length.";
                case 15: return "Jog target exceeds machine travel, ignored.";
                case 16: return "Jog Cmd missing '=' or has prohibited GCode";
                case 17: return "Laser mode requires PWM output.";
                case 20: return "Unsupported or invalid GCode command.";
                case 21: return "> 1 GCode command in a modal group in block.";
                case 22: return "Feed rate has not yet been set or is undefined.";
                case 23: return "GCode command requires an integer value.";
                case 24: return "> 1 GCode command using axis words found.";
                case 25: return "Repeated GCode word found in block.";
                case 26: return "No axis words found in command block.";
                case 27: return "Line number value is invalid.";
                case 28: return "GCode Cmd missing a required value word.";
                case 29: return "G59.x WCS are not supported.";
                case 30: return "G53 only valid with G0 and G1 motion modes.";
                case 31: return "Unneeded Axis words found in block.";
                case 32: return "G2/G3 arcs need >= 1 in-plane axis word.";
                case 33: return "Motion command target is invalid.";
                case 34: return "Arc radius value is invalid.";
                case 35: return "G2/G3 arcs need >= 1 in-plane offset word.";
                case 36: return "Unused value words found in block.";
                case 37: return "G43.1 offset not assigned to tool length axis.";
                case 38: return "Tool number gretaer than max value.";
                default:
                    return $"Uknown error code {code}";
            }
            return $"Uknown error code {code}";
        }
    }
}
