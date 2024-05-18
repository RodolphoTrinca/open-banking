using OpenBanking.Application.Entity;

namespace OpenBanking.API.DTO
{
    [Serializable]
    public class ParticipantsDTO
    {
        public IList<BankData> Participants { get; set; }

        public ParticipantsDTO()
        {
            
        }

        public ParticipantsDTO(IEnumerable<BankData> participants)
        {
            Participants = participants.ToList();
        }
    }
}
