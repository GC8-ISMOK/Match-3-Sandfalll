[System.Serializable]
public class FigurineData
{
    public string shape;
    public string color;
    public string animal;

    public FigurineData(string s, string c, string a)
    {
        shape = s;
        color = c;
        animal = a;
    }

    public bool Equals(FigurineData other)
    {
        return other != null &&
            shape == other.shape &&
            color == other.color &&
            animal == other.animal;
    }
}
