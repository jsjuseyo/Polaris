using bwaPolaris.Shared;
using static bwaPolaris.Shared.svpPrivileges;

namespace bwaPolaris.Components
{
    public class svcPrivileges
    {
        private svpPrivilegesClient _client { get; set; }
        private svcBaseService _bases { get; set; }
        public svcPrivileges(svcBaseService bases)
        {
            _bases = bases;
            _client = new svpPrivilegesClient(ClientChannel);
        }

        public async Task<List<T9Privileges>> GetPrivileges(Guid idUser)
        {
            var request = new rqsPrivileges
            {
                IdUser = idUser.ToString(),
            };
            var reply = _client.GetPrivileges(request, Headers);
            var data = reply.DaftarPrivileges.Adapt<List<T9Privileges>>();
            return data;
        }

		public async Task<T9Privileges> GetPrivilegesById(string idUser, string idForm)
		{
			var request = new rqsPrivileges
			{
				IdUser = idUser,
                IdForm = idForm
			};
			var reply = _client.GetPrivilegesById(request, Headers);
			var data = reply.Adapt<T9Privileges>();
			return data;
		}

		public async Task<bool> InsertPrivileges(T9Privileges data)
        {
            var request = data.Adapt<proPrivileges>();
            var reply = await _client.InsertPrivilegesAsync(request, Headers);
            return reply.IsOK;
        }

        public async Task<bool> UpdatePrivileges(T9Privileges data)
        {
            var request = data.Adapt<proPrivileges>();
            var reply = await _client.UpdatePrivilegesAsync(request, Headers);
            return reply.IsOK;
        }

        public async Task<bool> DeletePrivileges(T9Privileges data)
        {
            var request = data.Adapt<proPrivileges>();
            var reply = await _client.DeletePrivilegesAsync(request, Headers);
            return reply.IsOK;
        }

        public async Task<bool> ResetPrivileges()
        {
            var request = new rqsPrivileges { IdUser = "" };
            var reply = await _client.ResetPrivilegesAsync(request, Headers);
            return reply.IsOK;
        }

    }
}
