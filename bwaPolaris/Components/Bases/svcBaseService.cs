using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
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
        ClientChannel = GrpcChannel.ForAddress(AlamatAPI, new GrpcChannelOptions
        {
            HttpClient = httpClient,
            MaxReceiveMessageSize = 100 * 1024 * 1024, // 100 MB
            MaxSendMessageSize = 100 * 1024 * 1024 // 100 MB });

        });
    }

    public byte[] GetGambar(string value)
    {
        byte[] hasil = null;
        if (!string.IsNullOrEmpty(value))
        {
            hasil = Convert.FromBase64String(value);
        }
        return hasil;
    }


}
