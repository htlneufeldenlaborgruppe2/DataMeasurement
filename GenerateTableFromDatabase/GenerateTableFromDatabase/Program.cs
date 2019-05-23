using GenerateTableFromDatabase.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTableFromDatabase
{
    class Program
    {
        static void Main(string[] args)
        {

            var csv = new StringBuilder();
            csv.Append("sep=;\ntemp;hum;co2;vol;volpp;bright;brightClass;dust;time;group;deviceId;room;seatsBehind;class;countPersons;teacher1;teacher2;isSupp;\n");
            using (var context = new sqlprobeEntities4())
            {
                //csv.Append(context.Nachricht.First().Temperatur,context.Nachricht)

                List<Message> nachrichten = context.Message.ToList();
                List<Room> rooms = context.Room.ToList();
                List<RoomSubject> roomSubjects = context.RoomSubject.ToList();

                //nachrichten.Where(n=>n.deviceID==)
                foreach (var item in nachrichten)
                {
                    string className;
                    string roomNR = item.Device.Room_Device.Where(n => item.timesent > n.ValidFrom && item.timesent < n.ValidUntil).Select(n => n.Room.RoomNr).First();
                    int hour = GetHourFromMessage(item.timesent);

                    if (hour != 0)
                        className = roomSubjects.Where(n => n.Hour == hour && n.Room.RoomNr == roomNR).Select(n => n.Class.ClassName).First();
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

        private static int GetHourFromMessage(DateTime? item)
        {
            // es geht ned weil Convert.ToDateTime(h:mm) immer des heutige datum nimmt do muas ma des Datum von item eine duan
            if (item != null)
            {
                DateTime dt = (DateTime)item;
                int hour = dt.Hour;
                int minute = dt.Minute;
               // DateTime test = Convert.ToDateTime(dt.Month + "/" + dt.Day + "/" + dt.Year + " 8:15:00");

                if (Convert.ToDateTime("8:15") <= dt
                    && Convert.ToDateTime("9:05") >= dt)
                {
                    return 1;
                }
                else if (Convert.ToDateTime("9:05") <= dt
                    && Convert.ToDateTime("9:55") >= dt)
                {
                    return 2;
                }
                else if (Convert.ToDateTime("10:10") <= dt
                    && Convert.ToDateTime("11:00") >= dt)
                {
                    return 3;
                }
                else if (Convert.ToDateTime("11:00") <= dt
                    && Convert.ToDateTime("11:50") >= dt)
                {
                    return 4;
                }
                else if (Convert.ToDateTime("11:55") <= dt
                    && Convert.ToDateTime("12:45") >= dt)
                {
                    return 5;
                }
                else if (Convert.ToDateTime("12:45") <= dt
                    && Convert.ToDateTime("13:35") >= dt)
                {
                    return 6;
                }
                else if (Convert.ToDateTime("13:40") <= dt
                    && Convert.ToDateTime("14:30") >= dt)
                {
                    return 7;
                }
                else if (Convert.ToDateTime("14:35") <= dt
                    && Convert.ToDateTime("15:25") >= dt)
                {
                    return 8;
                }
                else if (Convert.ToDateTime("15:30") <= dt
                    && Convert.ToDateTime("16:20") >= dt)
                {
                    return 9;
                }
                
            }

            return 0;
        }
    }
}

