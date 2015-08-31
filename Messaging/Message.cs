namespace Cloudmark.Messaging
{
    public delegate void MessageHandler(Message message);

    public interface Message
    {
        object getPayload();

        MessageHeaders getHeaders();
    }
    /// A generic message representation with headers and body.  
    public interface Message<out T>: Message
    {
        new T getPayload();

        new MessageHeaders getHeaders();
    }
}