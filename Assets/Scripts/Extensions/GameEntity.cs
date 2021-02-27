public partial class GameEntity
{
    // Переопределяем метод ToString чтобы значение в компоненте NameComponent выводилось в окне иерархии
    // TODO можно развить, чтобы выводить дополнительно инфу о нужных компонентах
    public override string ToString()
    {
        string result = null;

        if (hasName)
        {
            result += name.value + (name.isUnique ? "" : "_" + creationIndex);
        }

        return result ?? base.ToString();
    }

    // Перегружаем метод AddName для красоты и удобства, чтобы по умолчанию isUnique был true
    public void AddName(string newValue)
    {
        AddName(newValue, true);
    }
}