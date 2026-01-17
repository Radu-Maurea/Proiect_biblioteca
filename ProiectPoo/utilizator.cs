using System.Text.Json.Serialization; 

namespace ProiectPoo;

[JsonDerivedType(typeof(Bibliotecar), typeDiscriminator: "Bibliotecar")]
[JsonDerivedType(typeof(Client), typeDiscriminator: "Client")]
public abstract class Utilizator
{
    public string Email { get; set; }
    public string Nume { get; set; }
    public string Parola { get; set; }
    
    public Utilizator(string email, string parola)
    {
        Email = email;
        Parola = parola;
    }

    public void SetNume(string nume) => Nume = nume;
    public abstract string Rol();
    
    public override string ToString() => $"{Nume} ({Rol()})";

    public void AfisareMeniuSpecific()
    {
        Console.WriteLine($"Bun venit, {Nume}! Esti logat ca {Rol()}.");
    }
}