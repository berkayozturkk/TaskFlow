using Microsoft.EntityFrameworkCore;
using TaskFlow.Data.Context;
using TaskFlow.Models.Entities;
using TaskFlow.Models.Enums;

namespace TaskFlow.Data.Seed
{
    public static class SeedData
    {
        public static async System.Threading.Tasks.Task SeedAsync(AppDbContext context)
        {
            // In-Memory DB için
            await context.Database.EnsureCreatedAsync();

            // 1. ROLLERİ EKLE (Employee için gerekli)
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new() { Id=1, Name = "Manager" },
                    new() { Id=2, Name = "Analist" },
                    new() { Id=3, Name = "Developer" }
                };
                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            // 2. EMPLOYEE'LERİ EKLE (Analyst için gerekli)
            if (!context.Employees.Any())
            {
                var roles = await context.Roles.ToDictionaryAsync(r => r.Name, r => r);

                var employees = new List<Employee>
                {
                    // Analistler
                    new() {
                        Id = 1,
                        FirstName = "Ali",
                        LastName = "Yılmaz",
                        Email = "ali.yilmaz@firma.com",
                        RoleId = roles["Analist"].Id,
                        IsActive = true
                    },
                    new() {
                        Id = 2,
                        FirstName = "Ayşe",
                        LastName = "Demir",
                        Email = "ayse.demir@firma.com",
                        RoleId = roles["Analist"].Id,
                        IsActive = true
                    },
                    
                    // Developerlar (8 kişi)
                    new() {
                        Id = 3,
                        FirstName = "Mehmet",
                        LastName = "Kaya",
                        Email = "mehmet.kaya@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    new() {
                        Id = 4,
                        FirstName = "Zeynep",
                        LastName = "Çelik",
                        Email = "zeynep.celik@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    new() {
                        Id = 5,
                        FirstName = "Can",
                        LastName = "Öztürk",
                        Email = "can.ozturk@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    new() {
                        Id = 6,
                        FirstName = "Elif",
                        LastName = "Aksoy",
                        Email = "elif.aksoy@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    new() {
                        Id = 7,
                        FirstName = "Burak",
                        LastName = "Kara",
                        Email = "burak.kara@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    new() {
                         Id = 8,
                        FirstName = "Deniz",
                        LastName = "Arslan",
                        Email = "deniz.arslan@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    new() {
                        Id = 9,
                        FirstName = "Cem",
                        LastName = "Yıldız",
                        Email = "cem.yildiz@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    new() {
                        Id = 10,
                        FirstName = "Seda",
                        LastName = "Güneş",
                        Email = "seda.gunes@firma.com",
                        RoleId = roles["Developer"].Id,
                        IsActive = true
                    },
                    //Manager
                     new() {
                        Id = 11,
                        FirstName = "Yunus",
                        LastName = "Çokgüzel",
                        Email = "berkay.ozturk@firma.com",
                        RoleId = roles["Manager"].Id,
                        IsActive = true
                    }
                };

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }

            // 3. İŞLEM TİPLERİNİ EKLE (8 farklı zorluk)
            if (!context.OperationTypes.Any())
            {
                var operationTypes = new List<OperationType>
                {
                    new() {
                        Id=1,
                        Name = "Basit Raporlama",
                        Description = "Hazır raporların çalıştırılması ve PDF çıktı alınması",
                        DifficultyLevel = TaskDifficulty.VeryEasy
                    },
                    new() {
                        Id=2,
                        Name = "Veri Girişi",
                        Description = "Form verilerinin sisteme girilmesi ve düzenlenmesi",
                        DifficultyLevel = TaskDifficulty.Easy
                    },
                    new() {
                        Id=3,
                        Name = "Form Düzenleme",
                        Description = "Mevcut formlarda alan ekleme/çıkarma ve düzenleme",
                        DifficultyLevel = TaskDifficulty.Normal
                    },
                    new() {
                        Id=4,
                        Name = "Rapor Optimizasyonu",
                        Description = "Yavaş çalışan raporların performans iyileştirmesi",
                        DifficultyLevel = TaskDifficulty.Medium
                    },
                    new() {
                        Id=5,
                        Name = "API Entegrasyonu",
                        Description = "Harici servislerle API bağlantısı kurulması",
                        DifficultyLevel = TaskDifficulty.Hard
                    },
                    new() {
                        Id=6,
                        Name = "Veritabanı İşlemleri",
                        Description = "Kompleks SQL sorguları ve stored procedure yazılması",
                        DifficultyLevel = TaskDifficulty.VeryHard
                    },
                    new() {
                        Id =7,
                        Name = "Kompleks Algoritma",
                        Description = "İş mantığı geliştirme ve optimizasyon",
                        DifficultyLevel = TaskDifficulty.Expert
                    },
                    new() {
                        Id =8,
                        Name = "Sistem Mimarisi",
                        Description = "Yeni modül tasarımı ve mimari kararlar",
                        DifficultyLevel =  TaskDifficulty.Legendary
                    }
                };

                await context.OperationTypes.AddRangeAsync(operationTypes);
                await context.SaveChangesAsync();
            }

            // 4. TASK'LERİ EKLE
            if (!context.Tasks.Any())
            {
                var operationTypes = await context.OperationTypes.ToDictionaryAsync(r => r.Name, r => r);

                var tasks = new List<TaskFlow.Models.Entities.Task>
                {
                    // BEKLEYEN TASKLER (Pending) 
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Acil: Aylık Satış Raporu Hatası",
                        Description = "Satış raporu yanlış hesaplama yapıyor, acil düzeltilmeli",
                        OperationTypeId = 0, 
                        AnalystId = 2,
                        Status = AssignmentStatus.Pending,
                        CreatedDate = DateTime.Now.AddHours(-2)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Yeni Müşteri Kayıt Formu",
                        Description = "Müşteri kayıt formuna 'T.C. Kimlik' alanı eklenecek",
                        OperationTypeId = 0, 
                        AnalystId = 1,
                        Status = AssignmentStatus.Pending,
                        CreatedDate = DateTime.Now.AddHours(-5)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Stok Raporu Performans Sorunu",
                        Description = "Stok raporu 30 saniyede açılıyor, 5 saniyenin altına düşürülmeli",
                        OperationTypeId = 0, 
                        AnalystId = 2,
                        Status = AssignmentStatus.Pending,
                        CreatedDate = DateTime.Now.AddDays(-1)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Excel Export Hatası",
                        Description = "Raporları Excel'e aktarırken tarih formatı bozuluyor",
                        OperationTypeId = 1, 
                        AnalystId = 2,
                        Status = AssignmentStatus.Pending,
                        CreatedDate = DateTime.Now.AddHours(-8)
                    },
    
                    //  ATANMIS TASKLER (Assigned) 
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Yeni API Entegrasyonu - X Bank",
                        Description = "X Bank ödeme sistemi için API entegrasyonu yapılacak",
                        OperationTypeId = 5, 
                        AnalystId = 1,
                        DeveloperId = 3, 
                        Status = AssignmentStatus.Assigned,
                        CreatedDate = DateTime.Now.AddDays(-3),
                        AssignedDate = DateTime.Now.AddDays(-2)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Veritabanı Yedekleme Prosedürü",
                        Description = "Otomatik yedekleme için stored procedure yazılacak",
                        OperationTypeId = 6, 
                        AnalystId = 1,
                        DeveloperId = 4, 
                        Status = AssignmentStatus.Assigned,
                        CreatedDate = DateTime.Now.AddDays(-4),
                        AssignedDate = DateTime.Now.AddDays(-3)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Muhasebe Modülü Hata Düzeltme",
                        Description = "KDV hesaplamasında hata var, düzeltilecek",
                        OperationTypeId = 3, 
                        AnalystId = 2,
                        DeveloperId = 5,
                        Status = AssignmentStatus.Assigned,
                        CreatedDate = DateTime.Now.AddDays(-2),
                        AssignedDate = DateTime.Now.AddDays(-1)
                    },
    
                    // DEVAM EDEN TASKLER (InProgress) 
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Yeni Rapor Tasarımı - Satış Analizi",
                        Description = "Bölgelere göre satış analizi yapan yeni rapor hazırlanacak",
                        OperationTypeId = 1, 
                        AnalystId = 1,
                        DeveloperId = 6, 
                        Status = AssignmentStatus.InProgress,
                        CreatedDate = DateTime.Now.AddDays(-5),
                        AssignedDate = DateTime.Now.AddDays(-4),
                        CompletedDate = null
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Müşteri İlişkileri Modülü Geliştirmesi",
                        Description = "Müşteri takip modülüne yeni özellikler eklenecek",
                        OperationTypeId = 7, 
                        AnalystId = 2,
                        DeveloperId = 7, 
                        Status = AssignmentStatus.InProgress,
                        CreatedDate = DateTime.Now.AddDays(-7),
                        AssignedDate = DateTime.Now.AddDays(-6)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Performans İyileştirmesi - Ana Sayfa",
                        Description = "Ana sayfa açılış süresi optimize edilecek",
                        OperationTypeId = 4, 
                        AnalystId = 1,
                        DeveloperId = 8, 
                        Status = AssignmentStatus.InProgress,
                        CreatedDate = DateTime.Now.AddDays(-3),
                        AssignedDate = DateTime.Now.AddDays(-2)
                    },
    
                    // TAMAMLANMIS TASKLER (Completed) 
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Fatura Yazdırma Düzeltmesi",
                        Description = "Fatura yazdırırken logo görünmüyor, düzeltildi",
                        OperationTypeId = 2, 
                        AnalystId = 1,
                        DeveloperId = 9, 
                        Status = AssignmentStatus.Completed,
                        CreatedDate = DateTime.Now.AddDays(-10),
                        AssignedDate = DateTime.Now.AddDays(-9),
                        CompletedDate = DateTime.Now.AddDays(-8)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Kullanıcı Giriş Loglama",
                        Description = "Kullanıcı girişlerinin loglanması sağlandı",
                        OperationTypeId = 6, 
                        AnalystId = 2,
                        DeveloperId = 10, 
                        Status = AssignmentStatus.Completed,
                        CreatedDate = DateTime.Now.AddDays(-12),
                        AssignedDate = DateTime.Now.AddDays(-11),
                        CompletedDate = DateTime.Now.AddDays(-9)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Şifre Sıfırlama Ekranı",
                        Description = "Şifre sıfırlama ekranına güvenlik kodu eklendi",
                        OperationTypeId = 3, 
                        AnalystId = 1,
                        DeveloperId = 3, 
                        Status = AssignmentStatus.Completed,
                        CreatedDate = DateTime.Now.AddDays(-15),
                        AssignedDate = DateTime.Now.AddDays(-14),
                        CompletedDate = DateTime.Now.AddDays(-12)
                    },
    
                    // ZOR TASKLER (Difficulty 7-8)
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Yapay Zeka Destekli Raporlama",
                        Description = "Satış tahminleri için yapay zeka modülü geliştirilecek",
                        OperationTypeId = 0, 
                        AnalystId = 0,
                        Status = AssignmentStatus.Pending,
                        CreatedDate = DateTime.Now.AddHours(-1)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Mikroservis Mimarisi Geçişi",
                        Description = "Mevcut sistemi mikroservislere bölme planlaması",
                        OperationTypeId = 8, 
                        AnalystId = 1,
                        Status = AssignmentStatus.Pending,
                        CreatedDate = DateTime.Now.AddDays(-2)
                    },
                    new TaskFlow.Models.Entities.Task
                    {
                        Title = "Veri Şifreleme Algoritması",
                        Description = "Hassas veriler için yeni şifreleme algoritması geliştirilecek",
                        OperationTypeId = 7, 
                        AnalystId = 2,
                        DeveloperId = 7, 
                        Status = AssignmentStatus.Assigned,
                        CreatedDate = DateTime.Now.AddDays(-5),
                        AssignedDate = DateTime.Now.AddDays(-4)
                    }
                };

                await context.Tasks.AddRangeAsync(tasks);
                await context.SaveChangesAsync();
            }
        
            
        }
    }
}