
namespace ProiectPoo;

public class Carte
{
    public string Nume { get; set; }
    public string Autor { get; set; }
    public int An { get; set; }
    public int NumarCopii { get; set; }
    public int DurataMaxima { get; set; }
    public string Categorie { get; set; } 

    public Carte(string nume, string autor, int an, int numarCopii, int durataMaxima)
    {
        Nume = nume;
        Autor = autor;
        An = an;
        NumarCopii = numarCopii;
        Categorie = null;
        DurataMaxima = durataMaxima;
    }
    
    public override string ToString()
    {
        string infoCat = string.IsNullOrEmpty(Categorie) ? "Fara Categorie" : Categorie;
        return $"{Nume} de ({Autor}, {An}), Copii: {NumarCopii}, Categorie: {infoCat}";
    }
}

public class Categorie_Literara
{
    public string Nume { get; set; }
    public List<Carte> Carti { get; set; }

    public Categorie_Literara(string nume)
    {
        Nume = nume;
        Carti = new List<Carte>();
    }
}