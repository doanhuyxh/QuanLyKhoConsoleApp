using QuanLyKhoConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoConsoleApp.ControllerWithView
{
    public class AssetControlerWithView
    {
        private readonly AppDbContext _appDbContext;
        public AssetControlerWithView(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Index()
        {
            Console.Clear();
            Console.WriteLine("Danh sách nguyên liệu\n");
            var ds = _appDbContext.Aseet.ToList();
            Console.WriteLine("{2, 3} {0,-20} {1}", "Tên nguyên liệu", "Số Lượng", "ID");
            Console.WriteLine("----------------------------------");
            foreach (var asset in ds)
            {
                Console.WriteLine("{2, 3} {0,-20} {1}", asset.Name, asset.Quantity, asset.Id);
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. Thêm nguyên liệu");
            Console.WriteLine("2. Sửa nguyên liệu");
            Console.WriteLine("3. Xoá nguyên liệu");
            Console.WriteLine("4. Nhập nguyên liệu");
            Console.WriteLine("5. Xuất nguyên liệu");
            Console.WriteLine("6. Lịch sử Nhập nguyên liệu");
            Console.WriteLine("7. Lịch sử Xuất nguyên liệu");
            Console.Write("Lựa chọn: ");
            int choice = int.Parse(Console.ReadLine() ?? "");
            switch (choice)
            {
                case 1:
                    this.AddAsset();
                    break;
                case 2:
                    this.EditAsset();
                    break;
                case 3:
                    this.DeleteAsset();
                    break;
                case 4:
                    this.warehouse();
                    break;
                case 5:
                    this.export();
                    break;
                case 6:
                    this.historywarehouse();
                    break;
                case 7:
                    this.historyexport();
                    break;
            }
        }

        private void AddAsset()
        {
            Console.Clear();
            Console.WriteLine("Khởi tạo nguyên liệu\n");
            Console.Write("Nhập tên Nguyên liệu: ");
            string Name = Console.ReadLine() ?? "";
            Console.Write("Nhập Số lượng ban đầu: ");
            int Quantity = int.Parse(Console.ReadLine() ?? "0");

            Aseet aseet = new Aseet();
            aseet.Name = Name;
            aseet.Quantity = Quantity;

            _appDbContext.Aseet.Add(aseet);
            _appDbContext.SaveChanges();


            this.Index();

        }
        private void EditAsset()
        {
            Console.Write("Nhập Id Nguyên liệu: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Aseet a = new Aseet();
            a = _appDbContext.Aseet.FirstOrDefault(a => a.Id == id);
            if (a == null)
            {
                Console.WriteLine("Nguyên liệu không tồn tại");
            }
            else
            {
                Console.Write("Nhập tên thay đổi: ");
                string Name = Console.ReadLine() ?? "";
                a.Name = Name;
                _appDbContext.Aseet.Update(a);
                _appDbContext.SaveChanges();
                this.Index();
            }
        }

        private void DeleteAsset()
        {
            Console.Write("Nhập Id Nguyên liệu: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Aseet a = new Aseet();
            a = _appDbContext.Aseet.FirstOrDefault(a => a.Id == id);
            if (a == null)
            {
                Console.WriteLine("Nguyên liệu không tồn tại");
            }
            else
            {
                _appDbContext.Aseet.Remove(a);
                _appDbContext.SaveChanges();
                this.Index();
            }
        }

        private void warehouse()
        {
            Console.Write("Nhập Id Nguyên liệu cần nhập: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Aseet a = new Aseet();
            a = _appDbContext.Aseet.FirstOrDefault(a => a.Id == id);
            if (a == null)
            {
                Console.WriteLine("Nguyên liệu không tồn tại");
            }
            else
            {
                Console.Write("Nhập số lượng: ");
                int Quantity = int.Parse(Console.ReadLine() ?? "0");
                a.Quantity += Quantity;
                _appDbContext.Aseet.Update(a);
                _appDbContext.SaveChanges();
                warehouse warehouse = new warehouse();
                warehouse.AssetId = a.Id;
                warehouse.Quantity = Quantity;
                _appDbContext.warehouse.Add(warehouse);
                _appDbContext.SaveChanges();
                this.Index();
            }
        }
        private void export()
        {
            Console.Write("Nhập Id Nguyên liệu cần Xuất: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Aseet a = new Aseet();
            a = _appDbContext.Aseet.FirstOrDefault(a => a.Id == id);
            if (a == null)
            {
                Console.WriteLine("Nguyên liệu không tồn tại");
            }
            else
            {
                Console.Write("Nhập số lượng: ");
                int Quantity = int.Parse(Console.ReadLine() ?? "0");
                a.Quantity += Quantity;
                _appDbContext.Aseet.Update(a);
                _appDbContext.SaveChanges();
                export export = new export();
                export.AssetId = a.Id;
                export.Quantity = Quantity;
                _appDbContext.export.Add(export);
                _appDbContext.SaveChanges();
                this.Index();
            }
        }
        private void historyexport()
        {
            Console.Clear();
            Console.WriteLine("Lịch sử xuất");
            var ds = from a in _appDbContext.export
                     select new
                     {
                         AsetName = _appDbContext.Aseet.FirstOrDefault(x => x.Id == a.AssetId).Name,
                         Quantity = a.Quantity,
                     };
            Console.WriteLine("{0, -20}{1}", "Nguyên liệu", "Số Lượng");
            foreach (var d in ds.ToList())
            {
                Console.WriteLine("{0, -20}{1}", d.AsetName, d.Quantity);
            }
            Console.ReadKey();
            this.Index();
        }
        private void historywarehouse()
        {
            Console.Clear();
            Console.WriteLine("Lịch sử nhập");
            var ds = from a in _appDbContext.warehouse
                     select new
                     {
                         AsetName = _appDbContext.Aseet.FirstOrDefault(x => x.Id == a.AssetId).Name,
                         Quantity = a.Quantity,
                     };
            Console.WriteLine("{0, -20} {1}", "Nguyên liệu", "Số Lượng");
            foreach (var d in ds.ToList())
            {
                Console.WriteLine("{0, -20} {1}", d.AsetName, d.Quantity);
            }
            Console.ReadKey();
            this.Index();
        }
    }
}
