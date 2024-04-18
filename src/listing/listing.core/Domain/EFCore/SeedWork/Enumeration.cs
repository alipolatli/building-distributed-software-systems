using System.Reflection;

namespace listing.core.Domain.EFCore.SeedWork;

public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }

    public int Id { get; private set; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    ///Eğer CompareTo metodu negatif bir değer döndürürse, bu, mevcut nesnenin karşılaştırılan nesneden daha küçük olduğunu gösterir.
    ///Eğer CompareTo metodu pozitif bir değer döndürürse, bu, mevcut nesnenin karşılaştırılan nesneden daha büyük olduğunu gösterir.
    ///Eğer CompareTo metodu sıfır döndürürse, bu, mevcut nesnenin karşılaştırılan nesneyle eşit olduğunu gösterir.
    /// </summary>
    /// <param name="other">Karşılaştırma yapılacak obje.</param>
    /// <returns></returns>
    public int CompareTo(object other)
    {
        // Diğer nesne null ise, bu nesne diğer nesneden daha büyüktür.
        if (other == null)
        {
            return 1;
        }

        // Diğer nesne bir Enumeration nesnesine dönüştürülür.
        Enumeration otherEnumeration = (Enumeration)other;

        // Id özelliklerini karşılaştırarak sıralama yapılır.
        return Id.CompareTo(otherEnumeration.Id);
    }


    /// <summary>
    /// Belirtilen türden tüm örneklendirme değerlerini elde etmeyi sağlar.
    /// </summary>
    /// <typeparam name="T">Örneklendirme değerlerinin alınacağı tür.</typeparam>
    /// <returns>Tüm örneklendirme değerlerini içeren IEnumerable koleksiyonu.</returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        // Belirtilen türün tür bilgisini alır.
        Type enumType = typeof(T);

        // Public, static ve sadece tanımlanan (DeclaredOnly) alanları alır.
        FieldInfo[] fields = enumType.GetFields(BindingFlags.Public |
                                                BindingFlags.Static |
                                                BindingFlags.DeclaredOnly);

        // Alanlardan değerleri seçer.
        IEnumerable<object> values = fields.Select(f => f.GetValue(null));

        // Seçilen değerleri T türüne dönüştürür.
        IEnumerable<T> enumValues = values.Cast<T>();

        // Tüm örneklendirme değerlerini döndürür.
        return enumValues;
    }

    /// <summary>
    /// İki Enumeration örneğinin Id değerleri arasındaki mutlak farkı hesaplar.
    /// </summary>
    /// <param name="firstValue">İlk Enumeration örneği.</param>
    /// <param name="secondValue">İkinci Enumeration örneği.</param>
    /// <returns>İki örneğin Id değerleri arasındaki mutlak fark.</returns>
    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        // İki örneğin Id değerleri arasındaki farkı hesaplar ve mutlak değerini alır.
        var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);

        // Mutlak farkı döndürür.
        return absoluteDifference;
    }

    /// <summary>
    /// Belirtilen değere sahip olan örneklendirme değerini elde eder.
    /// </summary>
    /// <typeparam name="T">Örneklendirme değerinin alınacağı tür.</typeparam>
    /// <param name="value">Elde edilmek istenen değer.</param>
    /// <returns>Belirtilen değere sahip olan örneklendirme değeri.</returns>
    public static T FromValue<T>(int value) where T : Enumeration
    {
        // Belirtilen değere sahip olan örneklendirme değerini elde etmek için Parse metodu çağrılır.
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);

        // Elde edilen örneklendirme değerini döndürür.
        return matchingItem;
    }

    /// <summary>
    /// Belirtilen görünen adı (display name) olan örneklendirme değerini elde eder.
    /// </summary>
    /// <typeparam name="T">Örneklendirme değerinin alınacağı tür.</typeparam>
    /// <param name="displayName">Elde edilmek istenen görünen ad.</param>
    /// <returns>Belirtilen görünen adı olan örneklendirme değeri.</returns>
    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        // Belirtilen görünen adı olan örneklendirme değerini elde etmek için Parse metodu çağrılır.
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);

        // Elde edilen örneklendirme değerini döndürür.
        return matchingItem;
    }

    /// <summary>
    /// Belirtilen koşulu sağlayan örneklendirme değerini elde eder.
    /// </summary>
    /// <typeparam name="T">Örneklendirme değerinin alınacağı tür.</typeparam>
    /// <typeparam name="K">Koşulun kontrol edileceği değerin türü.</typeparam>
    /// <param name="value">Koşulun kontrol edileceği değer.</param>
    /// <param name="description">Hata durumunda kullanılacak açıklama.</param>
    /// <param name="predicate">Koşul fonksiyonu.</param>
    /// <returns>Koşulu sağlayan örneklendirme değeri.</returns>
    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        // Belirtilen koşulu sağlayan örneklendirme değerini elde etmek için GetAll metodu kullanılır ve FirstOrDefault ile koşula uygun olan ilk öğe bulunur.
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        // Eğer koşula uygun bir örneklendirme değeri bulunamazsa, InvalidOperationException fırlatılır.
        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        // Koşulu sağlayan örneklendirme değeri döndürülür.
        return matchingItem;
    }

    /// <summary>
    /// İki örneklendirme değerini karşılaştırır ve tür ve değer eşleşmesi durumunda true, aksi takdirde false döndürür.
    /// </summary>
    /// <param name="obj">Karşılaştırılacak nesne.</param>
    /// <returns>İki örneklendirme değerinin eşit olup olmadığını gösteren bool değeri.</returns>
    public override bool Equals(object obj)
    {
        // İlk olarak, obj parametresini Enumeration türüne dönüştürerek otherValue değişkenine atarız.
        if (!(obj is Enumeration otherValue))
        {
            // Eğer dönüşüm başarısız ise, false değerini döndürürüz çünkü eşleşme yoktur.
            return false;
        }

        // İki örneklendirme değerinin türünün eşleşip eşleşmediğini kontrol ederiz.
        var typeMatches = GetType().Equals(obj.GetType());

        // İki örneklendirme değerinin Id değerlerini karşılaştırırız.
        var valueMatches = Id.Equals(otherValue.Id);

        // Hem tür hem de değer eşleşirse, true değerini döndürürüz.
        return typeMatches && valueMatches;
    }


    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}