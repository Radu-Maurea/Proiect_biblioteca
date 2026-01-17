using System.Text.Json.Serialization;
namespace ProiectPoo;

// [JsonDerivedType(typeof(Bibliotecar), typeDiscriminator: "Bibliotecar")]
// [JsonDerivedType(typeof(Client), typeDiscriminator: "Client")]
public class Client : Utilizator
{
    public Client(string email, string parola) : base(email, parola){}
    public override string Rol() => "Client";
    
}