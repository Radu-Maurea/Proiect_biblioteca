namespace ProiectPoo;

public class Manager : Utilizator
{
    public Manager(string nume, string parola) : base(nume, parola){}
    
    public override string Rol() => "Manager";
}