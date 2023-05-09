# Prof IT Film Arşivi

Bu proje, Prof IT şirketi bünyesinde 2023 yılında staj yapan öğrenciler tarafından yazılmışır. Bu proje kullanıcıların da aktif rol oynayabileceği bir film arşivi oluşturmayı amaçlamaktadır.

## Gereksinimler

Bu projeyi çalıştırmak için aşağıdaki gereksinimlere ihtiyacınız vardır:
- Visual Studio 2022
- Asp.Net Core 6
- MsSql Veri Tabanı
- MongoDb (Log takibi için)
- Seq (Log takibi için)

## Kurulum

1. Projenin github üzerinden bağlantı adresini kopyalıyıp yerel bilgisayarınızda bir klon oluşturun
2. appsetings dosyasındaki database bağlantı adresininzi düzenleyiniz
3. "update-database komutu ile programınb kullancağı veritabaını yerel bilgisayarınızda oluşturunuz"
4. Visual Studio üzerinden projeyi derleyip çalıştırınız

## Kullanılan NuGet Paketleri

- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation: Bu paket, Razor sayfalarını çalışma zamanında derleyerek geliştirme sürecini hızlandıran bir araçtır.
- Microsoft.EntityFrameworkCore: Bu paket, veritabanı işlemlerini kolaylaştırmak için kullanılır. Entity Framework Core kullanılarak verileri veritabanına eklemek, silmek ve düzenlemek mümkündür.
- Microsoft.EntityFrameworkCore.Design: Bu paket, Entity Framework Core kullanarak veritabanı tasarımı yapmak ve yönetmek için gerekli araçları sağlar.
- Microsoft.EntityFrameworkCore.SqlServer: Bu paket, Entity Framework Core kullanarak SQL Server veritabanıyla çalışmak için gerekli araçları sağlar.
- Microsoft.Extensions.Configuration.Json: Bu paket, uygulamanın yapılandırma dosyalarından (appsettings.json) veri almak için kullanılır.
- Microsoft.Extensions.DependencyInjection: Bu paket, bağımlılık enjeksiyonu yapmak için kullanılır. Uygulamadaki farklı servisler arasındaki bağımlılıkları çözmek için kullanılır.
- Microsoft.VisualStudio.Web.CodeGeneration.Design: Bu paket, .NET CLI üzerinden kod üretimi için kullanılır. Özellikle, MVC uygulamalarında denetleyici, görünüm ve modelleri otomatik olarak oluşturmak için kullanılabilir.

## Kullanılan Servisler

### MovieService
MovieService, IMovieService arayüzünü uygular ve veritabanındaki filmlerin işlemlerini gerçekleştirir. Bu sınıf, IUnitOfWork ve IMapper arayüzlerine sahiptir ve bu arayüzleri kullanarak, veritabanı işlemleri ve nesne eşleme işlemlerini gerçekleştirir.

MovieService sınıfı, AddMovie, UpdateMovie, DeleteMovie, GetAllMovies, GetMovieById, GetMoviesByGenre, GetMoviesByDirector, GetMoviesByActor, SearchMovies gibi birçok yöntemi içerir. Bu yöntemler, filmlerin ekleme, silme, düzenleme, listeleme ve arama işlemlerini gerçekleştirir.

### UserService 
UserService, IUserService arayüzünü uygular ve kullanıcı verilerinin işlemlerini gerçekleştirir. Bu sınıf, IUnitOfWork arayüzüne sahiptir ve bu arayüzü kullanarak, veritabanı işlemlerini gerçekleştirir.

UserService sınıfı, AddUser, UpdateUser, DeleteUser, GetAllUsers, GetUserById, GetUserByEmail, GetUserByUsername, ValidateUser gibi birçok yöntemi içerir. Bu yöntemler, kullanıcıların kayıt olma, giriş yapma, profil bilgilerini güncelleme ve listeleme işlemlerini gerçekleştirir.

### RatingService
RatingService, IRatingService arayüzünü uygular ve filmlerin değerlendirilmesi işlemlerini gerçekleştirir. Bu sınıf, IUnitOfWork arayüzüne sahiptir ve bu arayüzü kullanarak, veritabanı işlemlerini gerçekleştirir.

RatingService sınıfı, AddRating, UpdateRating, DeleteRating, GetAllRatings, GetRatingById, GetRatingsByMovieId, GetAverageRatingByMovieId gibi birçok yöntemi içerir. Bu yöntemler, filmlere puan verme, verilen puanları güncelleme, puanları listeleme ve ortalama puanları hesaplama işlemlerini gerçekleştirir.

### CommentService
CommentService.cs, filmlerle ilgili yorumlarla ilgili işlemleri gerçekleştirmek için tasarlanmış bir servis sınıfıdır. Sınıf, ICommentService arayüzünü uygular ve tüm yorum işlemlerini içeren yöntemleri sağlar. Yöntemler arasında, tüm yorumları getirme, bir film için yapılan yorumları getirme, bir yorum eklemeyi, bir yorumu silmeyi, bir yorumun detaylarını getirmeyi, bir kullanıcının yaptığı tüm yorumları getirmeyi ve bir yorumu güncellemeyi içerir. Bu servis, projedeki diğer servislerle birlikte kullanılabilir


### CategoryService
 CategoryService.cs dosyası, filmlerin kategorileriyle ilgili işlemleri yapmak üzere tasarlanmış bir servis sınıfıdır. Sınıf, ICategoryService arayüzünü uygular ve GetAllCategories, GetCategoryById, AddCategory, DeleteCategory ve UpdateCategory gibi çeşitli yöntemler aracılığıyla kategori işlemlerini gerçekleştirir. Bu yöntemler, projede kategori işlemlerinin kolayca yönetilmesini sağlar ve diğer servislerle birlikte kullanılabilir.

### FavoriteService
 FavoriteService.cs dosyası, kullanıcıların favori filmlerini yönetmek için kullanılan bir servis sınıfıdır. Sınıf, tüm favori filmleri listelemek, belirli bir favori filmi getirmek, yeni bir favori film eklemek ve bir favori filmi silmek gibi yöntemler içerir. Bu sayede, uygulamanın kullanıcılar tarafından sevilen filmleri takip etmek ve yönetmek için kolay bir yöntemi vardır.

### MovieLikeService
MovieLikeService.cs dosyası, kullanıcıların filmleri beğenme işlemlerini yönetmek için kullanılan bir servis sınıfıdır. Sınıf, tüm film beğenilerini listelemek, belirli bir film beğenisini getirmek, yeni bir film beğenisi eklemek ve bir film beğenisini silmek gibi yöntemler içerir. Bu sayede, uygulamanın kullanıcıların film beğenilerini takip etmek ve yönetmek için kolay bir yöntemi vardır.

### CommentLikeService
CommentLikeService.cs dosyası, yorumları beğenme işlemlerini yönetmek için kullanılan bir servis sınıfıdır. Bu sınıf, tüm yorum beğenilerini listeleme, belirli bir yorum beğenisini getirme, yeni bir yorum beğenisi ekleme ve bir yorum beğenisini silme işlemlerini gerçekleştirebilir.
## Roller


- User(Authenticate olmamış)
- User(Authenticate olmuş)
- Admin  


## Kullanım

- Anasayfada, mevcut filmleri görebilirsiniz.
- Anasayfada, admin tarafından öne çıkarılan filmleri slider üzerinde gezebilirsiniz.
- Anasayfada, mevcut filtreleri ve kategorilere göre filmleri listeleyebilirsiniz.
- Anasayfada, mevcut filmlerin detay sayfalarına gidebilirsiniz.
- Detay sayfasından filmi listenize ekleyip çıkarabilirsiniz.
- Detay sayfasıdan filme 5 üzerinden puan verebilirisiniz.
- Detay sayfasıdan filme yorum yazabilirisiniz
- Detay sayfasıdan filme yapılmış olan yorumlara beğeni atabilirsiniz
- Admin uygulamadaki filmler ve kullanıcılar ile ilgili metrikleri metrikler sayfasından görüntüleyebilir
- Admin yeni bir film eklemek için "Add Movie" sayfasınan gerekli alanları doldurarak yeni film ekleyebilir.
- Admin yeni kategori ekleyebilir.
- Admin var olan kategorileri silebilir.
- Admin var olan kategorileri güncelleyebilir.
- Admin filmlere yazılmış olan yorumları onaylayabilir ya da banlayabilir
- Admin üye olan kullanıcıları bilgileriyle listeleyebilir.
- Admin sistemdeki filmleri listeyebilir ve bu filmler üzerinde değişiklik yapabilir

## Katkıda Bulunma

Her türlü katkıya açığız! Projenin Github sayfasında yer alan "Issues" kısmına gidebilir ve açık problemleri inceleyebilirsiniz. Ayrıca yeni özellikler ekleyebilir veya hataları düzeltebilirsiniz. Pull request yaparak katkıda bulunabilirsiniz.

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasına bakabilirsiniz.
