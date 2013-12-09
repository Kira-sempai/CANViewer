using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CANViewer
{
    public class MessageParser
    {
        public static byte ReadByte(string message, int start)
        {
            return Convert.ToByte(message.Substring(start, 2), 16);
        }
        public static decimal ReadTemperature(byte[] data, int start)
        {
            return (decimal)((short)(data[start] + data[start + 1] * 256)) / 10; ;
        }

        public static ParsedCANMessage Parse(string message)
        {
            ParsedCANMessage parsed = new ParsedCANMessage();

            if (message.Length > 8)
            {
                byte flags = ReadByte(message, 0);
                byte function = ReadByte(message, 2);
                parsed.Target = ReadByte(message, 4);
                byte type = ReadByte(message, 6);

                short length = Convert.ToInt16(message.Substring(8, 1), 10);

                if (message.Length == 9 + length * 2)
                {
                    byte[] bytes = new byte[length];
                    for (int i = 0; i < length; i++)
                    {
                        bytes[i] = Convert.ToByte(message.Substring(9 + i * 2, 2), 16);
                    }

                    parsed.Response = (flags & 0x10) > 0;
                    parsed.Exception = (flags & 0x8) > 0;
                    parsed.Reserved = (byte)(flags & 0x7);

                    switch (type)
                    {
                        case 0:
                            parsed.Type = "All";
                            break;
                        case 1:
                            parsed.Type = "Executor";
                            switch (function)
                            {
                                case 1:
                                    parsed.Function = "Is Id Occupied";
                                    break;
                                default:
                                    parsed.Function = "Unknown";
                                    break;
                            }
                            break;
                        case 2:
                            parsed.Type = "Outdoor Sensor";
                            switch (function)
                            {
                                case 1:
                                    parsed.Function = "Get Temperature";
                                    if (parsed.Response)
                                    {
                                        parsed.Temperature = ReadTemperature(bytes, 0);
                                    }
                                    break;
                                default:
                                    parsed.Function = "Unknown";
                                    break;
                            }
                            break;
                        case 3:
                            switch (function)
                            {
                                case 1:
                                    parsed.Function = "Give Way";
                                    parsed.Priority = bytes[0];
                                    parsed.Manager = bytes[1];
                                    break;
                                case 2:
                                    parsed.Function = "Proceed";
                                    parsed.Priority = bytes[0];
                                    parsed.Manager = bytes[1];
                                    break;
                                case 3:
                                    parsed.Function = "Set Cooling";
                                    parsed.Source = bytes[0];
                                    parsed.State = bytes[1];
                                    break;
                                case 4:
                                    parsed.Function = "Set Warm Up";
                                    parsed.Source = bytes[0];
                                    break;
                                case 5:
                                    parsed.Function = "Get Needed Temperature";

                                    if (parsed.Response)
                                    {
                                        parsed.Temperature = ReadTemperature(bytes, 0);
                                        parsed.Manager = bytes[2];
                                    }
                                    else
                                    {
                                        parsed.Source = bytes[0];
                                    }
                                    break;
                                default:
                                    parsed.Function = "Unknown";
                                    break;
                            }
                            parsed.Type = "Consumer";
                            break;
                        case 4:
                            parsed.Type = "Heating Manager";
                            break;
                        case 5:
                            parsed.Type = "Room Sensor";
                            break;
                        case 6:
                            switch (function)
                            {
                                case 0:
                                    parsed.Function = "Request Temperature";
                                    parsed.Temperature = ReadTemperature(bytes, 0);
                                    parsed.Manager = bytes[2];
                                    break;
                                case 1:
                                    parsed.Function = "Get Temperature";
                                    if (parsed.Response)
                                    {
                                        parsed.Temperature = ReadTemperature(bytes, 0);
                                    }
                                    break;
                                case 2:
                                    parsed.Function = "Get Properties";
                                    if (parsed.Response)
                                    {
                                        parsed.BoilerType = bytes[0];
                                        parsed.Power = bytes[1] + bytes[2] * 256;
                                        parsed.Priority = bytes[3];
                                        parsed.WorkTime = bytes[4] + bytes[5] * 256;
                                    }
                                    break;
                                case 3:
                                    parsed.Function = "Request Power";
                                    parsed.Power = bytes[0] + bytes[1] * 256;
                                    parsed.Temperature = ReadTemperature(bytes, 2);
                                    break;
                                case 4:
                                    parsed.Function = "Get Current Power";
                                    if (parsed.Response)
                                    {
                                        parsed.Power = bytes[0];
                                    }
                                    break;
                                case 5:
                                    parsed.Function = "Get Work Time";
                                    if (parsed.Response)
                                    {
                                        parsed.WorkTime = bytes[0] + bytes[1] * 256;
                                    }
                                    break;
                                default:
                                    parsed.Function = "Unknown";
                                    break;
                            }
                            parsed.Type = "Heat Source";
                            break;
                        case 7:
                            parsed.Type = "Heat Accumulator";
                            break;
                        case 8:
                            parsed.Type = "Extended Controller";
                            break;
                        case 9:
                            parsed.Type = "Extension Controller";
                            break;
                        case 10:
                            parsed.Type = "Monitoring Device";
                            break;
                        case 11:
                            parsed.Type = "Controller";

                            switch (function)
                            {
                                case 1:
                                    parsed.Function = "I am here";
                                    break;
                                case 2:
                                    parsed.Function = "Get Id";
                                    break;
                                case 3:
                                    parsed.Function = "Get Active Program List";
                                    break;
                                case 4:
                                    parsed.Function = "Add New Program";
                                    break;
                                case 5:
                                    parsed.Function = "Remove Program";
                                    break;
                                case 6:
                                    parsed.Function = "Get System DateTime";
                                    if (parsed.Response)
                                    {
                                        parsed.Datetime = new DateTime(bytes[0] + bytes[1] * 256, bytes[2], bytes[3], bytes[4], bytes[5], bytes[6]);
                                    }
                                    break;
                                case 7:
                                    parsed.Function = "SET_SYSTEM_DATE_TIME";
                                    break;
                                default:
                                    parsed.Function = "Unknown";
                                    break;
                            }

                            break;
                        case 12:
                            parsed.Type = "Heating Circuit";
                            break;
                        default:
                            parsed.Type = "Unknown(" + type + ")";
                            break;
                    }
                }
                else
                {
                    parsed.Error = "Body truncated (" + message + ")";
                }
            }
            else
            {
                parsed.Error = "Header trucated (" + message + ")";
            }

            return parsed;
        }
    }
}
