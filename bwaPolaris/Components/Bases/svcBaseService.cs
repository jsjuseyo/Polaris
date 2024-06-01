using Grpc.Net.Client;
using Grpc.Net.Client.Web;
namespace bwaPolaris.Components;

public class svcBaseService
{
    public List<T0Form> DaftarForm = new();
    public List<T9Privileges> DaftarHakAkses = new();
    public T9User User = new();

    public svcBaseService()
    {
        InitializeGrpcChannel();
    }

    private void InitializeGrpcChannel()
    {
        var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
        ClientChannel = GrpcChannel.ForAddress(AlamatAPI, new GrpcChannelOptions { HttpClient = httpClient });
        
    }


}
