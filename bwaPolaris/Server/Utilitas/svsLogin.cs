using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static bwaPolaris.Server.modAes;

namespace bwaPolaris.Server;

public class svsLogin : svpLogin.svpLoginBase
{
	private IDbContext _db;
	private readonly svsBaseService _bases;
	public svsLogin()
	{
		_db = ServerDbContext;
		_bases = new svsBaseService(_db);
	}

	public override async Task<RplLogin> Login(RqsLogin request, ServerCallContext context)
	{
		var hasil = new RplLogin();
		try
		{
			//var e = _bases.HashPassword(request.Password);
			var data = await _db.Set<T9User>().FirstOrDefaultAsync(x => x.Username == request.Username);
			if (data is null)
			{
				
				Metadata metadata = new Metadata { { "Error", "Username yang anda masukkan salah!" } };
				//throw new RpcException(new Status(StatusCode.NotFound, "Not Found"), metadata);
                hasil.Token = "Username yang anda masukkan salah!";
                hasil.IsOK = false;
				return hasil;
            }

            //data.Password = Encrypt(request.Password);
            //_db.Set<T9User>().Update(data);
            //_db.SaveChangesAsync();

            var decryptPassword = Decrypt(data.Password);

            if (decryptPassword != request.Password) {
                Metadata metadata = new Metadata { { "Error", "Password yang anda masukkan salah!" } };
                hasil.Token = "Password yang anda masukkan salah!";
                hasil.IsOK = false;
                return hasil;
            }

			//var tokenHandler = new JwtSecurityTokenHandler();
			//var key = Encoding.UTF8.GetBytes("123456789absfdjfdhsfq");
			//var tokenDescriptor = new SecurityTokenDescriptor
			//{
			//	Subject = new ClaimsIdentity(new Claim[]
			//	{
			//			new Claim(ClaimTypes.NameIdentifier, Convert.ToString(data.IdUser)),
			//	}),
			//	Expires = DateTime.UtcNow.AddDays(7),
			//	SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			//};
			//var token = tokenHandler.CreateToken(tokenDescriptor);
			hasil = data.Adapt<RplLogin>();
			//hasil.Token = tokenHandler.WriteToken(token);
			hasil.Token = "";
			hasil.IsOK = true;
		}
		catch (Exception e)
		{
			var msg = e.Message;
		}
		return hasil;
	}

	public override async Task<RplUser> GetUser(RqsUser request, ServerCallContext context)
	{
		try
		{
			var data = await _db.Set<T9User>().FirstOrDefaultAsync(x => x.IdUser.ToString() == request.IdUser);
			return data.Adapt<RplUser>();
		}
		catch (Exception e) { throw e; }
	}

}
