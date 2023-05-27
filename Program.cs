using QuanLyKhoConsoleApp.ControllerWithView;
using QuanLyKhoConsoleApp.Model;
using System.Text;

namespace QuanLyKhoConsoleApp
{
    internal class Program
    {
        static AppDbContext dbContext = new AppDbContext();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("Phần Mềm Quản Lý Kho");
                Console.WriteLine("---------------------------");
                AssetControlerWithView aset = new AssetControlerWithView(appDbContext: dbContext);
                aset.Index();
            }
        }
    }
}