namespace ProiectPoo;

public class Carte
{
    public string Nume { get; set; }
    public string Autor { get; set; }
    public int An { get; set; }
    public int NumarCopii { get; set; }
    public Carte(string nume, string autor, int an,int numarCopii)
    {
        Nume = nume;
        Autor = autor;
        An = an;
        NumarCopii = numarCopii;
    }
    
    public override string ToString() => $"{Nume} de ({Autor}, {An}), cu {NumarCopii} copii.";
}