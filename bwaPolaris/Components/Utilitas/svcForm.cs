global using bwaPolaris.Shared;
global using Mapster;
global using static Polaris.Utility.modVariabel;
using static bwaPolaris.Shared.svpMasterForm;

namespace bwaPolaris.Components
{
    public class svcForm
    {
        private svpMasterFormClient _client { get; set; }
        private svcBaseService _bases { get; set; }
        public svcForm(svcBaseService bases)
        {
            _bases = bases;
            _client = new svpMasterFormClient(ClientChannel);
        }

        public async Task<List<T0Form>> GetDataForm()
        {
            var reply = _client.GetForm(new RqsForm(), Headers);
            return reply.ListT0Form.Adapt<List<T0Form>>();
        }

		public async Task<List<T0Form>> GetDataFormByIdUser(Guid? idUser)
		{
            var request = new RqsFormByIdUser { IdUser = idUser.ToString() };
			var reply = _client.GetFormByIdUser(request, Headers);
			return reply.ListT0Form.Adapt<List<T0Form>>();
		}

		public async Task<List<T1AtributForm>> GetDataAtributForm(string idForm)
        {
            var reply = _client.GetT1AtributFormById(new RqsFormById { IdForm = idForm }, Headers);
            return reply.ListT1AtributForm.Adapt<List<T1AtributForm>>();
        }
    }
}
