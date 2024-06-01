using bwaPolaris.Data;
using Grpc.Core;
using Grpc.Net.Client;

namespace Polaris.Utility;
public static class modVariabel
{
    public static string AlamatAPI, TitleApp, Nama, Role;

    public static Guid IdUser, IdJabatan, IdValidator, IdServer;
    public static GrpcChannel ClientChannel;
    public static Metadata Headers;
    public static IDbContext ServerDbContext;
}