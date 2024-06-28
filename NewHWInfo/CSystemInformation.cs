using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewHWInfo
{
    public class CSystemInformation
    {
        public static string Get_OS()
        {
            ManagementObjectSearcher searcherOS = new ("SELECT Caption FROM Win32_OperatingSystem");

            foreach (ManagementObject os in searcherOS.Get())
            {
                string sOSName = os["Caption"].ToString();
                return sOSName;
            }
            return "Unknown";
        }

        public static string Get_CPU()
        {
            ManagementObjectSearcher searcherCPU = new
                ("SELECT Name, Manufacturer, MaxClockSpeed FROM Win32_Processor");

            foreach (ManagementObject cp in searcherCPU.Get())
            {
                string sCPU = ($"{cp["Name"]} ({cp["Manufacturer"]}), " +
                    $"Максимальная частота ЦП: {cp["MaxClockSpeed"]} MHz");
                return sCPU;
            }
            return "Unknown";
        }

        public static string Get_RAM()
        {
            ManagementObjectSearcher searcherRAM = new
                ("SELECT * FROM Win32_PhysicalMemory");

            int stickCount = 0;
            long totalRAMSize = 0;
            string[,] ramDetails = new string[searcherRAM.Get().Count, 5];
            foreach (ManagementObject ram in searcherRAM.Get())
            {
                ramDetails[stickCount, 0] = $"Планка {stickCount + 1}";
                ramDetails[stickCount, 1] = $"Объём: {Convert.ToInt64(ram["Capacity"]) / 1024 / 1024 / 1024} GB";
                int memoryType = Convert.ToInt32(ram["MemoryType"]);
                string ddrType = GetDDRType(memoryType);
                ramDetails[stickCount, 2] = $"Тип DDR: {ddrType}";
                ramDetails[stickCount, 3] = $"Скорость: {ram["Speed"]} МГц";
                string manufacturerCode = Convert.ToString(ram["Manufacturer"]);
                string manufacturerName = GetManufacturerName(manufacturerCode);
                ramDetails[stickCount, 4] = $"Производитель: {manufacturerName}";

                stickCount++;
                totalRAMSize += Convert.ToInt64(ram["Capacity"]);
            }

            string RAMquery = string.Empty;
            for (int i = 0; i < stickCount; i++)
            {
                RAMquery += ramDetails[i, 0] + Environment.NewLine;
                RAMquery += ramDetails[i, 1] + Environment.NewLine;
                RAMquery += ramDetails[i, 2] + Environment.NewLine;
                RAMquery += ramDetails[i, 3] + Environment.NewLine;
                RAMquery += ramDetails[i, 4] + Environment.NewLine;
                RAMquery += Environment.NewLine;
            }

            RAMquery += $"Всего установлено планок ОЗУ: {stickCount}" + Environment.NewLine;
            RAMquery += $"Общая память ОЗУ: {totalRAMSize / 1024 / 1024 / 1024} GB";

            return RAMquery;

        }

        static string GetManufacturerName(string code)
        {
            switch (code)
            {
                case "0":
                    return "Generic";
                case "1":
                    return "Samsung";
                case "2":
                    return "Ramaxel";
                case "5":
                    return "Hynix";
                case "7":
                    return "Kingston";
                case "16":
                    return "Micron";
                case "0215":
                    return "Kingston";
                default:
                    return "No name";
            }
        }

        static string GetDDRType(int type)
        {
            switch (type)
            {
                case 20:
                    return "DDR";
                case 21:
                    return "DDR2";
                case 24:
                    return "DDR3";
                case 26:
                    return "DDR4";
                default:
                    return "Unknown";
            }
        }

        public static string Get_Display()
        {
            int dispCount = 0;
            ManagementObjectSearcher searcherMonitor = new
                ("SELECT * FROM Win32_DesktopMonitor");

            string[,] dispDetails = new string[searcherMonitor.Get().Count, 4];
            foreach (ManagementObject disp in searcherMonitor.Get())
            {
                dispDetails[dispCount, 0] = $"Ид подключенного монитора: {dispCount + 1}";
                dispDetails[dispCount, 1] = $"Имя монитора: {Convert.ToString(disp["Name"])}";
                dispDetails[dispCount, 2] = $"Разрешение дисплея: {Convert.ToInt64(disp["ScreenWidth"])}" +
                    $"x{Convert.ToInt64(disp["ScreenHeight"])} px";
                dispDetails[dispCount, 3] = $"Производитель: {disp["MonitorManufacturer"]}";

                dispCount++;
            }
            string DISPquery = string.Empty;
            for (int i = 0; i < dispCount; i++)
            {
                DISPquery += dispDetails[i, 0] + Environment.NewLine;
                DISPquery += dispDetails[i, 1] + Environment.NewLine;
                DISPquery += dispDetails[i, 2] + Environment.NewLine;
                DISPquery += dispDetails[i, 3] + Environment.NewLine;
                DISPquery += Environment.NewLine;
            }

            DISPquery += $"Всего подключено дисплеев: {dispCount}" + Environment.NewLine;

            return DISPquery;
        }
    }
}
