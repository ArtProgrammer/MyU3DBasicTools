
namespace SimpleAI.Messaging
{
    public interface ITelegramReceiver
    {
        bool HandleMessage(ref Telegram msg);
    }
}
