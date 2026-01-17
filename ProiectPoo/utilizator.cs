namespace ProiectPoo;

public abstract class Utilizator
{
    private string Nume { get; set; }
    private string Parola { get; set; }
    
    public Utilizator(string nume, string parola)
    {
        Nume = nume;
        Parola = parola;
    }

    public abstract string Rol();
    public override string ToString() => $"{Nume} ({Rol()})";
}