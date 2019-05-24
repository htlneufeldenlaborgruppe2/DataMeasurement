using GenerateTableFromDatabase.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTableFromDatabase
{
    class Program
    {
        static int timeOffset = DateTime.Now.Hour - (int)DateTime.UtcNow.Hour;
        static void Main(string[] args)
        {

            var csv = new StringBuilder();
            csv.Append("sep=;\ntemp;hum;co2;vol;volpp;bright;brightClass;dust;time;group;deviceId;room;seatsBehind;class;countPersons;teacher1;teacher2;isSupp;\n");
            using (var context = new sqlprobeEntities6())
            {
                //csv.Append(context.Nachricht.First().Temperatur,context.Nachricht)

                List<Message> nachrichten = context.Message.ToList();
                List<Room> rooms = context.Room.ToList();
                List<RoomSubject> roomSubjects = context.RoomSubject.ToList();
                List<Class> classes = context.Class.ToList();

                //nachrichten.Where(n=>n.deviceID==)
                foreach (var item in nachrichten)
                {
                    string className = "";
                    string roomNR = item.Device.Room_Device.Where(n => item.timesent > n.ValidFrom && item.timesent < n.ValidUntil).Select(n => n.Room.RoomNr).First();
                    string roomID = rooms.Where(n => n.RoomNr == roomNR).Select(n => n.Room_ID).First().ToString();
                    int hour = GetHourFromMessage(item.timesent);


                    if (hour != 0 && roomSubjects.Where(n => n.Hour == hour && n.fk_RoomID.ToString() == roomID).Count() != 0)
                    {
                            className = roomSubjects.Where(n => n.Hour == hour && n.fk_RoomID.ToString() == roomID).Select(n => n.Class.ClassName).First();
                            int breakHour = GetBreakHourFromClass(item.timesent, className);
                    }
                    else
                        className = "empty";
                    //                           temp           hum                 co2              vol         volpp       bright        bC            dust              time                        devID         room       seatsb         class  
                    csv.Append(String.Concat(item.temp, ";", item.humidity, ";", item.co2, ";", item.noise, ";", '0', ";", item.ldr, ";", '0', ";", item.dust, ";", item.timesent.ToString(), ";", '1', ";", item.deviceID, ";", roomNR, ";", '0', ";", className, ";", '0', ";", '0', ";", '0', ";", '0', ";", "\n"));
                }
            }

            using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\erg.csv"))
            {
                writer.WriteLine(csv);
            }
        }

        private static int GetBreakHourFromClass(DateTime dt,string className)
        {
           // if(DayOfWeek.Monday)

            using (var context = new sqlprobeEntities6())
            {
                List<Class> classes = context.Class.ToList();
                if (dt.DayOfWeek == DayOfWeek.Monday)
                   // return classes.Where(n => n.ClassName == className).Select(n => n.BreakMon).First();
            }
            return 0;
        }

        private static int GetHourFromMessage(DateTime? item)
        {
            // es geht ned weil Convert.ToDateTime(h:mm) immer des heutige datum nimmt do muas ma des Datum von item eine duan

            if (item != null)
            {
                DateTime dt = (DateTime)item;
                int hour = dt.Hour + timeOffset;
                int minute = dt.Minute;
                // DateTime test = Convert.ToDateTime(dt.Month + "/" + dt.Day + "/" + dt.Year + " 8:15:00");


                IFormatProvider culture = new CultureInfo("ast", true);

                if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 08:15:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 09:05:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 1;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 09:05:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 09:55:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 2;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 10:10:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 11:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 3;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 11:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 11:50:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 4;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 11:55:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 12:45:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 5;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 12:45:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 13:35:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 6;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 13:40:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 14:30:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 7;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 14:35:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 15:25:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 8;
                }
                else if (DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 15:30:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) <= dt
                    && DateTime.ParseExact(dt.Day.ToString("00") + "/" + dt.Month.ToString("00") + "/" + dt.Year.ToString("0000") + " 16:20:00.000", "dd/MM/yyyy HH:mm:ss.fff", culture) >= dt)
                {
                    return 9;
                }

            }

            return 0;
        }
    }
}

