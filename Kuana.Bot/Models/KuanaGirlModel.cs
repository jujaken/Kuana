
namespace Kuana.Bot.Models
{
    public class KuanaGirlModel
    {
        public required List<DialogMessageModel> MemmoryMessage { get; set; }
        public int MemmorySize { get; set; } 

        public void AddToMemmoryMessage(string msg)
            => AddToMemmoryMessage(new DialogMessageModel() { Message = msg });

        public void AddToMemmoryMessage(string author, string msg)
            => AddToMemmoryMessage(new DialogMessageModel() { Author = author, Message = msg });

        public void AddToMemmoryMessage(DialogMessageModel msg)
        {
            if (MemmoryMessage.Count == MemmorySize)
                MemmoryMessage.Remove(MemmoryMessage.First());

            MemmoryMessage.Add(msg);
        }
    }
}
