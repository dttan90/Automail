using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // DataTable
using System.Threading;
using BUS;
using DTO;
using System.IO;
using System.Diagnostics;

namespace GUI
{
    class AutomailGUI
    {
        // Lấy thông tin folder đang chạy chương trình, lưu file nguồn (AutomailFile) vào đây
        private static string FolderStartUp = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        // private static string FileRevisePD = "";

        private static AutomailBUS Automail = new AutomailBUS();

        static void Main(string[] args)
        {
            //// the path run revise PD application
            //FileRevisePD = FolderStartUp.Replace("file:\\", "") + @"\RevisePD\RevisePD.exe";

            int index = 1;
            if (checkAutomailLoad() == false)
            {
                // Không cập nhật automail
                DataTable AutoloadLast = ReadAutoloadLast();
                if (AutoloadLast.Rows.Count > 0 )
                {
                    string CreatedDate = ((DateTime)AutoloadLast.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd HH:mm");
                    Console.WriteLine(index + ". Automail updated: " + CreatedDate);
                }
                
                
            }
            else
            {
                LoadAutoMail();

                //// revise promise date 
                //Process.Start(FileRevisePD);
            }

            index = 2;
            while (true)
            {
                try
                {
                    // Sau 10 phút kiểm tra file trả về 1 lần. Nếu đã có dữ liệu mới thì cập nhật Automail
                    if ( (Convert.ToInt32(DateTime.Now.ToString("mm")) == 40) || (Convert.ToInt32(DateTime.Now.ToString("mm")) == 10) )
                    {
                        if (checkAutomailLoad() == false)
                        {
                            // Không cập nhật automail
                            DataTable AutoloadLast = ReadAutoloadLast();
                            string CreatedDate = ((DateTime)AutoloadLast.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd HH:mm");
                            Console.WriteLine(index + ". Automail updated: " + CreatedDate);
                        }
                        else
                        {
                            LoadAutoMail();

                            // // revise promise date
                            // Process.Start(FileRevisePD);
                        }

                        index++;
                    }

                    

                }
                catch
                {
                    Console.WriteLine("Error");
                }

                Thread.Sleep(60000);

            }            

            
        }

        public static void LoadAutoMail()
        {
            var count = Automail.AddVnso();
            var count2 = Automail.AddVnsoTotal();
            if (count > 0 && count2 > 0)
            {
                var check = Automail.AddAutoload();
                if (check)
                {
                    Console.WriteLine(count + " lines uploaded. DONE"); // 5
                }
                else
                {
                    Console.Write(count + " lines uploaded . Update Autoload failed");
                }
            }

            else
            {
                Console.WriteLine(" FAILED to update data");
            }
            
        }

        public void showData()
        {
            DataTable Data = new DataTable();
            Data = Automail.ReadAll();
            foreach (DataRow val in Data.Rows)
            {
                foreach (DataColumn column in Data.Columns)
                {
                    Console.WriteLine(column + ": " + val[column]);
                }

            }
        }

        public static DataTable ReadAutoloadLast()
        {
            DataTable Data = new DataTable();
            Data = Automail.ReadAutoloadItem();
            // var FileName = Data.Rows[0]["LOGDATA"];
            return Data;
            
        }

        // Hàm Lấy LogData (tên file đã update dữ liệu lần mới nhất) so sánh với file mới nhất hiện tại (trong folder Automail)
        // Nếu trùng tên thì k cập nhật dữ liệu
        // Nếu khác thì cập nhật dữ liệu mới
        public static bool checkAutomailLoad()
        {
            try
            {
                DataTable AutoloadLast = ReadAutoloadLast();
                if (AutoloadLast.Rows.Count == 0) return true;
                string LogData = AutoloadLast.Rows[0]["LOGDATA"].ToString();

                // Nếu không có logdata hoặc null thì trả về true
                if (string.IsNullOrEmpty(LogData))
                {
                    return true;
                }
                else
                {
                    var fileName = Automail.getZipName();
                    return (LogData == fileName) ? false : true;
                }
            }
            catch (Exception ex)
            {
                // xử lý lỗi tại đây
                Console.WriteLine(ex.ToString());
                Console.WriteLine("KHÔNG LẤY ĐƯỢC FILE ZIP");
            }
            
            return false;
        }

    }
}
